import {Elements} from '@stripe/react-stripe-js';
import {loadStripe} from '@stripe/stripe-js';
import CheckoutForm from '../Components/PaymentComponents/CheckoutForm';
import { useEffect, useState } from 'react';
import { apiWithAuth } from '../Axios/api';

const stripePromise = loadStripe('pk_test_51QwKtIAcsuZw0JH85rQ3Hc2IffiQbNaWlQVEXyvS0eKqOzSSavNsLla0ZFzZsJ52TVts8SQBMUWvm2Eymerobnbs00j8OO6b0L');

export default function PaymentPage( {orderInfo} ) {
    const [clientSecret, setClientSecret] = useState("");


    useEffect(() => {
        async function getPaymentIntent() {
            try {
                const response = await apiWithAuth.post("/api/Create-Payment-Intent");
                const clientSecretFromResponse = await response.data;
                setClientSecret(clientSecretFromResponse)
            } catch (error) {
                console.error(error.message);
            }
        }
        getPaymentIntent();
    }, []);

  const options = {
    // passing the client secret obtained from the server
    clientSecret: `${clientSecret}`,
  };

  return (
    <Elements stripe={stripePromise} options={options}>
      <CheckoutForm orderInfo={orderInfo}/>
    </Elements>
  );
};