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
import AdminPage from "./Pages/AdminPage";
import ProfilePage from "./Pages/ProfilePage";
import OrderDetails from "./Pages/OrderDetails"
import ResetPasswordRequest from "./Pages/ResetPasswordRequest";
import ResetPassword from "./Pages/ResetPassword";

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
  {
    path: "/admin",
    element: <AdminPage />
  },
  {
    path: "/profile/:userId",
    element: <ProfilePage />
  },
  {
    path: "/orderdetails/:orderId",
    element: <OrderDetails />
  },
  {
    path: "/reset/password",
    element: <ResetPasswordRequest />
  },
  {
    path: "/reset/password/new",
    element: <ResetPassword />
  }
]);

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(

  <RouterProvider router={router} />
);
