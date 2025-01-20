import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import './index.css'
import HomePage from "./Pages/HomePage"
import CartPage from "./Pages/CartPage"
import ProductPage from "./Pages/ProductPage"
import LoginPage from "./Pages/LoginPage";
import OrderedPage from "./Pages/OrderedPage"
import RegistrationPage from "./Pages/RegistrationPage";


const router = createBrowserRouter([
  {
    path: "/",
    element: <HomePage />
  },
  {
    path: "/login",
    element: <LoginPage />
  },
  {
    path: "/register",
    element: <RegistrationPage />
  },
  {
    path: "/product/:id",
    element: <ProductPage />
  },
  {
    path: "/cart/:userId",
    element: <CartPage />
  },
  {
    path: "/order/success/:orderId",
    element: <OrderedPage />
  },
]);

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(

  <RouterProvider router={router} />
);
