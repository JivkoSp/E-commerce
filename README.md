# E-commerce

BooksPlace is E-commerce book store where users can buy books, write reviews and comment on revies of others. 
The store have admin panel where users who have "Staff" and/or "Admin" roles can view different statistics, 
{create, add}=> Products, {create, update, delete}=> Users, {create}=>Roles,Promotions and bann users. 

1.How the app looks:

LogIn page:
![LogInImg](https://user-images.githubusercontent.com/97282923/181300719-9228a0f3-457c-4a22-967e-d3ca0e090a12.PNG)

SignUp page:
![SignUpImg](https://user-images.githubusercontent.com/97282923/181300912-59aa5db9-589c-4be6-b086-dbec72246af4.PNG)

Product pages and categories:
![Products](https://user-images.githubusercontent.com/97282923/181301175-eb1e8756-4c04-4ec5-bbef-24ee457998e0.PNG)

Product with discount price:
![ProductsWithPromotions](https://user-images.githubusercontent.com/97282923/181301695-de88af6e-fe64-43cd-b3f7-f24a7482985f.PNG)

Search products:
![Search](https://user-images.githubusercontent.com/97282923/181301793-63a54ce1-e3cb-450d-94d5-2df7f8405253.PNG)

View product:
![ViewProduct](https://user-images.githubusercontent.com/97282923/181301879-b38862b7-1aae-421d-84b4-085ddb6a3e64.PNG)

Write review about product:
![ViewProduct-Create](https://user-images.githubusercontent.com/97282923/181302004-5cc197d4-6904-42b3-9207-eaa477ec4ebd.PNG)

View product reviews and write/read comments:
![ViewProduct-Reviews](https://user-images.githubusercontent.com/97282923/181302103-be148118-ce9a-4eec-84bc-14a8fb7e6e9a.PNG)

Product cart:
![Cart](https://user-images.githubusercontent.com/97282923/181302269-4b3253a0-1183-48b9-bc16-1b11e2e3525d.PNG)

Admin home page:
![Admin-Index](https://user-images.githubusercontent.com/97282923/181302393-5e33b9f6-23ff-47ec-85b2-cff8edbb548e.PNG)

Admin manage users:
![Admin-Dashboard-ManageUsers](https://user-images.githubusercontent.com/97282923/181302891-54c964d0-d674-4a2d-9ff1-fe3caf38b563.PNG)

Admin edit user:
![Admin-EditUser](https://user-images.githubusercontent.com/97282923/181302508-1f9f39ff-5b12-411f-974f-533ac7450a06.PNG)

Admin create user:
![Admin-CreateUser](https://user-images.githubusercontent.com/97282923/181302664-558b9507-ccad-4527-be41-3d7e30360029.PNG)

Admin create role:
![Admin-CreateRole](https://user-images.githubusercontent.com/97282923/181302721-09456519-8a4c-436a-94f9-08b46e78f0cd.PNG)

Admin manage promotions:
![Admin-ManagePromotions](https://user-images.githubusercontent.com/97282923/181303022-77239334-64dc-4061-98a9-53c84f6b8316.PNG)

Admin dashboard products:
![Admin-Dashboard-Products](https://user-images.githubusercontent.com/97282923/181303146-98b4daae-b8ca-4602-bf2d-6bb1806b75fe.PNG)

Admin dashboard users
![Admin-Dashboard-Users](https://user-images.githubusercontent.com/97282923/181303323-0bf6334c-b23c-463d-a61a-bdf333f17e29.PNG)

2. How the app works

The authentication and authorization functionality is created with Identity api in boundle with enitity framework. 
I have used RabbitMq for the messaging functionality (the ability to have comments), it's highly reliable and have good support. 
For the cart functionality is used session with local storage i.e RAM (i don't really need sql support). 
For the search functionality am using simple jquery autocomplete with the help of small search api on the backend (am using jquery becouse it's simple, 
it have good documentation and i think at least for now i don't really need something more complex).







