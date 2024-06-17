# E-commerce

BooksPlace is an E-commerce book store where users can buy books, write reviews, and comment on reviews of others. 
The store has an admin panel where users with "Staff" and/or "Admin" roles can view different statistics, create and manage products, users, roles, promotions, and ban users.

## 1. How the app looks

### LogIn page:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181300719-9228a0f3-457c-4a22-967e-d3ca0e090a12.PNG" alt="LogIn page" width="450">
</div>

### SignUp page:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181300912-59aa5db9-589c-4be6-b086-dbec72246af4.PNG" alt="SignUp page" width="450">
</div>

### Product pages and categories:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181301175-eb1e8756-4c04-4ec5-bbef-24ee457998e0.PNG" alt="Product pages and categories" width="800">
</div>

### Product with discount price:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181301695-de88af6e-fe64-43cd-b3f7-f24a7482985f.PNG" alt="Product with discount price" width="800">
</div>

### Search products:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181301793-63a54ce1-e3cb-450d-94d5-2df7f8405253.PNG" alt="Search products" width="800">
</div>

### View product:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181301879-b38862b7-1aae-421d-84b4-085ddb6a3e64.PNG" alt="View product" width="800">
</div>

### Write review about product:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181302004-5cc197d4-6904-42b3-9207-eaa477ec4ebd.PNG" alt="Write review about product" width="800">
</div>

### View product reviews and write/read comments:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181302103-be148118-ce9a-4eec-84bc-14a8fb7e6e9a.PNG" alt="View product reviews and write/read comments" width="800">
</div>

### Product cart:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181302269-4b3253a0-1183-48b9-bc16-1b11e2e3525d.PNG" alt="Product cart" width="800">
</div>

### Admin home page:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181302393-5e33b9f6-23ff-47ec-85b2-cff8edbb548e.PNG" alt="Admin home page" width="800">
</div>

### Admin manage users:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181302891-54c964d0-d674-4a2d-9ff1-fe3caf38b563.PNG" alt="Admin manage users" width="800">
</div>

### Admin edit user:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181302508-1f9f39ff-5b12-411f-974f-533ac7450a06.PNG" alt="Admin edit user" width="800">
</div>

### Admin create user:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181302664-558b9507-ccad-4527-be41-3d7e30360029.PNG" alt="Admin create user" width="800">
</div>

### Admin create role:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181302721-09456519-8a4c-436a-94f9-08b46e78f0cd.PNG" alt="Admin create role" width="800">
</div>

### Admin manage promotions:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181303022-77239334-64dc-4061-98a9-53c84f6b8316.PNG" alt="Admin manage promotions" width="800">
</div>

### Admin dashboard products:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181303146-98b4daae-b8ca-4602-bf2d-6bb1806b75fe.PNG" alt="Admin dashboard products" width="800">
</div>

### Admin dashboard users:
<div style="text-align: center;">
  <img src="https://user-images.githubusercontent.com/97282923/181303323-0bf6334c-b23c-463d-a61a-bdf333f17e29.PNG" alt="Admin dashboard users" width="800">
</div>

## 2. How the app works

The authentication and authorization functionality is created using Identity API integrated with Entity Framework. 
RabbitMQ is used for messaging functionality, particularly for comments, due to its reliability and robust support. Session with local storage is utilized for cart functionality, avoiding the need for SQL support. For search functionality, simple jQuery autocomplete is implemented with a lightweight search API on the backend, leveraging jQuery for its simplicity and extensive documentation.
