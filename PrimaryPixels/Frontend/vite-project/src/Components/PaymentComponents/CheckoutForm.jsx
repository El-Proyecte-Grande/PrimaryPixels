import { useEffect, useState } from 'react';
import { apiWithAuth } from '../../Axios/api';
import { useNavigate } from 'react-router-dom';
const stripeKey = import.meta.env.VITE_STRIPE_KEY;


const CheckoutForm = ({ orderId, clientSecret }) => {
  const navigate = useNavigate();
  const stripe = Stripe(stripeKey);
  const elements = stripe.elements();
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    const cardElement = elements.create('card');
    cardElement.mount('#card-element');
  }, [])

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!stripe || !elements) {
      return;
    }
    try {
      const { paymentIntent, error } = await stripe.confirmCardPayment(clientSecret, {
        payment_method: {
          card: elements.getElement('card'),
        }
      });

      if (error) {
        setErrorMessage(error.message || "Payment failed. Please try again.");
      } else if (paymentIntent && paymentIntent.status === 'succeeded') {
        await apiWithAuth.post("/api/payments/success", { orderId: orderId }, {
          headers: { "Content-Type": "application/json" }
        });
        navigate(`/order/success/${orderId}`);
      }

    } catch (err) {
      setErrorMessage("An error occurred during payment processing. Please try again.");
      console.error(err);
    };
  }


  return (
    <form onSubmit={handleSubmit} className="flex flex-col items-center gap-4 pt-52">
      <div id="card-element" className="w-full max-w-md p-2 border border-gray-300 rounded bg-white">

      </div>
      {errorMessage && <div className="error-message">{errorMessage}</div>}
      <button disabled={!stripe} className="px-4 py-2 bg-blue-500 text-white rounded">Submit</button>
    </form>
  )
};

export default CheckoutForm;