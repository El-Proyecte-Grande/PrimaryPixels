import { useState } from 'react';
import { apiWithAuth } from '../../Axios/api';
import { useNavigate } from 'react-router-dom';
import { useStripe, useElements, PaymentElement } from '@stripe/react-stripe-js';


const CheckoutForm = ({ orderId }) => {
  const stripe = useStripe();
  const elements = useElements();
  const navigate = useNavigate();
  const [errorMessage, setErrorMessage] = useState("");


  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!stripe || !elements) {
      return;
    }
    const result = await stripe.confirmPayment({
      elements,
      redirect: "if_required"
    });

    if (result.error) {
      setErrorMessage(result.error.message);
      console.log(result.error.message);
    } else {
      await apiWithAuth.post("/api/payments/success", { orderId: orderId }, {
        headers: { "Content-Type": "application/json" }
      });
      navigate(`/order/success/${orderId}`);
    }
  }

  return (
    <form onSubmit={handleSubmit} className="flex flex-col items-center gap-4 pt-52">
      <PaymentElement />
      {errorMessage && <div className="error-message">{errorMessage}</div>}
      <button disabled={!stripe} className="px-4 py-2 bg-blue-500 text-white rounded">Submit</button>
    </form>
  )
};

export default CheckoutForm;