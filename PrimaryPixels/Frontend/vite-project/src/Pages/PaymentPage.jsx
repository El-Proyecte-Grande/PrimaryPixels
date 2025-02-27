import { Elements } from '@stripe/react-stripe-js';
import { loadStripe } from '@stripe/stripe-js';
import CheckoutForm from '../Components/PaymentComponents/CheckoutForm';
import { useEffect, useState } from 'react';
import { apiWithAuth } from '../Axios/api';
import Loading from "../Components/LoadingComponent/Loading";
const stripeKey = import.meta.env.VITE_STRIPE_KEY;
const stripePromise = loadStripe(stripeKey);

export default function PaymentPage({ orderInfo }) {
  const [clientSecret, setClientSecret] = useState("");
  const [isLoading, setIsLoading] = useState(true);


  useEffect(() => {
    async function getPaymentIntent() {
      try {
        const response = await apiWithAuth.post("/api/Create-Payment-Intent", {
          Amount: 175000
        },

        );
        const clientSecretFromResponse = await response.data;
        setClientSecret(clientSecretFromResponse)
        setIsLoading(false);
      } catch (error) {
        console.error(error.message);
      }
    }
    getPaymentIntent();
  }, []);



  return (
    <>
      {
        isLoading ? (
          <Loading />
        ) : (
          <Elements stripe={stripePromise} options={{ clientSecret: `${clientSecret}` }}>
            <CheckoutForm orderInfo={orderInfo} />
          </Elements>
        )
      }
    </>
  );
};