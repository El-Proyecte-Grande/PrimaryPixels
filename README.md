# PrimaryPixels

## About The Project

PrimaryPixels is a webshop designed to simplify online shopping. Users can browse products, add them to their cart, and place orders. 
Administrators have access to a secure interface to manage products and inventory.

### Key Features:
- **User Registration and Login**: Securely register and log in to access features.
- **Browsing Products**: Users can filter products by category on the main page for easy navigation.
- **Add to Cart**: Add, remove, or adjust product quantities in the cart.
- **Submit Order**: Provide a delivery address and submit orders seamlessly.
- **Admin Panel**: A dedicated interface for administrators to add and manage products.


## Built With

- **Backend**: [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) (with Identity Framework and Entity Framework)
- **Frontend**: [React.js](https://reactjs.org/)
- **Containerization**: [Docker](https://www.docker.com/) and Docker Compose

---

## Screenshots

### Main Page
![image](https://github.com/user-attachments/assets/9c488554-921b-4fd0-89b3-091bfb2349a7)

### Login Page
![image](https://github.com/user-attachments/assets/8273b48b-1266-4247-84a6-e165d5488f6c)

### Cart Page
![image](https://github.com/user-attachments/assets/f3dad35d-e70b-49d8-95ff-b4df4bc6eadc)

### Product Page
![image](https://github.com/user-attachments/assets/fcdb26e2-aee0-4031-bb38-83012683b12e)

### Admin Interface
![image](https://github.com/user-attachments/assets/22abd890-ef0c-413a-8079-c108874f08c3)


---

## Prerequisites

Make sure you have the following installed:
1. [Docker](https://www.docker.com/)
2. [Node.js](https://nodejs.org/)
3. [.NET 8 SDK](https://dotnet.microsoft.com/)

---

## Start the App

0.: Prepare dotenv file If you're running the app using Docker, go to the main folder. Copy the .env.sample file and rename it to .env. If you're running the app without Docker, go to the PrimaryPixels folder. Copy the .env.sample file and rename it to .env. These .env files contain the necessary environment variables for the app to function properly.


### Using Docker

1. Build docker compose: docker compose build

2. Run DB: docker compose up db

3. Open backend folder, use migrations: cd PrimaryPixels --> dotnet ef database update --context "PrimaryPixelsContext"     2, dotnet ef database update --context "UsersContext"

4. Create Frontend .env file, in frontend/vite-project folder, according to the .env.sample Should contain the backend URL

5. Step back, run docker compose: cd ..  --> docker compose up

6. Access the app: Open your browser, navigate to http://localhost:4000.


### With Terminal

1. Start the database using Docker:
   
    docker-compose up -d db
   
2. Navigate to the backend directory and use migrations:

 cd PrimaryPixels
 dotnet ef database update --context "PrimaryPixelsContext"
 dotnet ef database update --context "UsersContext"
 
3. start the backend server:

 dotnet run
 remember the server URL
 
4. Navigate to the frontend directory and create .env according to the .env.sample

 cd ..
 cd frontend/vite-project
 .ENV file should contain the backend URL.
 
5. Install npm and start the server

 npm install
 npm run dev
 
6. Access the app: Open your browser, navigate to http://localhost:4000.

## Contributors

- **Domokos Marcell**  
  [GitHub Profile](https://github.com/domokosmarcell)

- **Gőgös Dániel**  
  [GitHub Profile](https://github.com/GogosDani)
   
