import { Elements } from '@stripe/react-stripe-js';
import { loadStripe } from '@stripe/stripe-js';
import CheckoutForm from '../Components/PaymentComponents/CheckoutForm';
import { useEffect, useState } from 'react';
import { apiWithAuth } from '../Axios/api';
import Loading from "../Components/LoadingComponent/Loading";
const stripeKey = import.meta.env.VITE_STRIPE_KEY;
const stripePromise = loadStripe(stripeKey);

export default function PaymentPage({ orderInfo }) {
  const [isLoading, setIsLoading] = useState(true);


  useEffect(() => {

    async function getPaymentIntent() {
      try {
        sessionStorage.setItem("orderId", await submitOrder());
        const intentResponse = await apiWithAuth.post(
          "/api/Create-Payment-Intent",
          { OrderId: sessionStorage.getItem("orderId") },
          { headers: { "Content-Type": "application/json" } }
        );
        const clientSecretFromResponse = await intentResponse.data;
        console.log(clientSecretFromResponse);
        sessionStorage.setItem("clientSecret", clientSecretFromResponse);
        setIsLoading(false);
      } catch (error) {
        console.error(error.message);
      }
    }
    // Check if the user already has a client secret in session storage in case of a page refresh
    if(!sessionStorage.getItem("clientSecret")) {
      getPaymentIntent();
    } else {
      setIsLoading(false);
    }
  }, []);

  async function submitOrder() {
    const response = await apiWithAuth.post("/api/order",
      JSON.stringify(orderInfo),
      {
        headers: {
          "Content-Type": "application/json"
        },
      });

    if (response.status !== 200) {
      console.error("Failed to submit order");
      return;
    }
    // Delete items from the user's shopping cart when the order submission is successful.
    const deleteRequest = await apiWithAuth.delete("/api/shoppingCartItem/user");
    if (deleteRequest.status !== 200) {
      console.error("Failed to delete products from cart!");
      return;
    }
    return response.data;
  }

  return (
    <>
      {
        isLoading ? (
          <Loading />
        ) : (
          <Elements stripe={stripePromise} options={{ clientSecret: `${sessionStorage.getItem("clientSecret")}` }}>
            <CheckoutForm orderId={sessionStorage.getItem("orderId")} />
          </Elements>
        )
      }
    </>
  );
};