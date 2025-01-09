import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import './index.css'
import HomePage from "./Pages/HomePage"
import CartPage from "./Pages/CartPage"
import ProductPage from "./Pages/ProductPage"
import LoginPage from "./Pages/LoginPage";

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
    path: "/product/:id",
    element: <ProductPage />
  },
  {
    path: "/cart/:userId",
    element: <CartPage />
  }
]);

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(

  <RouterProvider router={router} />
);
