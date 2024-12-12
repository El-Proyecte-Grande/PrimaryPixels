import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import HomePage from './Pages/HomePage.jsx';
import './index.css';

const router = createBrowserRouter([
  {
    path: "/",
    element: <HomePage />
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
