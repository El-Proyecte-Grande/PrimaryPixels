import { useStripe, useElements, PaymentElement } from '@stripe/react-stripe-js';
import { apiWithAuth } from '../../Axios/api';

const CheckoutForm = ({ orderInfo }) => {
  const stripe = useStripe();
  const elements = useElements();

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!stripe || !elements) {
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
        return_url: `${window.location.origin}/order/success/${await getOrderId()}`,
      },
    });


    if (result.error) {
      console.log(result.error.message);
    } else {
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