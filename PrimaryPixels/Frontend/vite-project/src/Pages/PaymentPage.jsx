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
  const [orderId, setOrderId] = useState("");


  useEffect(() => {

    async function getPaymentIntent() {
      try {
        const orderid = await submitOrder();
        setOrderId(orderid);
        const intentResponse = await apiWithAuth.post(
          "/api/Create-Payment-Intent",
          { OrderId: orderid },
          { headers: { "Content-Type": "application/json" } }
        );
        const clientSecretFromResponse = await intentResponse.data;
        setClientSecret(clientSecretFromResponse)
        setIsLoading(false);
      } catch (error) {
        console.error(error.message);
      }
    }
    getPaymentIntent();
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
          <Elements stripe={stripePromise} options={{ clientSecret: `${clientSecret}` }}>
            <CheckoutForm orderId={orderId} />
          </Elements>
        )
      }
    </>
  );
};