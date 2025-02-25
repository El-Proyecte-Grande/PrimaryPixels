import {useStripe, useElements, PaymentElement} from '@stripe/react-stripe-js';


const CheckoutForm = ({orderInfo}) => {
  const stripe = useStripe();
  const elements = useElements();

  const handleSubmit = async (event) => {
    // We don't want to let default form submission happen here,
    // which would refresh the page.
    event.preventDefault();

    if (!stripe || !elements) {
      // Stripe.js hasn't yet loaded.
      // Make sure to disable form submission until Stripe.js has loaded.
      return;
    }

    const getOrderId = async () => {
        
        const response = await apiWithAuth.post("/api/order",
            JSON.stringify(orderInfo),
            {
                headers: {
                    "Content-Type": "application/json"
                },
            })
        if (response.status !== 200) {
            console.error("Failed to submit order");
            return;
        }
        // Delete items from users shoppingcart when the order submit was successful.
        const deleteRequest = apiWithAuth.delete("/api/shoppingCartItem/user");
        if (response.status !== 200) {
            console.error("Failed to delete products from cart!");
            return;
        }
        const orderId = response.data;
        return orderId;
    }

    const result = await stripe.confirmPayment({
      //`Elements` instance that was used to create the Payment Element
      elements,
      confirmParams: {
        return_url: `/order/success/${getOrderId()}`
      },
    });


    if (result.error) {
      // Show error to your customer (for example, payment details incomplete)
      console.log(result.error.message);
    } else {
      // Your customer will be redirected to your `return_url`. For some payment
      // methods like iDEAL, your customer will be redirected to an intermediate
      // site first to authorize the payment, then redirected to the `return_url`.
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <PaymentElement />
      <button disabled={!stripe}>Submit</button>
    </form>
  )
};

export default CheckoutForm;