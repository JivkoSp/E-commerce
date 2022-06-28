# E-commerce

BooksPlace is E-commerce book store where users can buy books, write reviews and comment on revies of others.
The store have admin panel where users who have "Staff" and/or "Admin" roles can view different statistics, {create, add}=> Products,
{create, update, delete}=> Users, {create}=>Roles,Promotions and bann users. The authentication and authorization functionality is created 
with Identity api in boundle with enitity framework. I have used RabbitMq for the messaging functionality (the ability to have comments), 
it's highly reliable and have good support. For the cart functionality is used session with local storage i.e RAM (i don't really need sql support).
For the search functionality am using simple jquery autocomplete with the help of small search api on the backend (am using jquery becouse it's simple,
 it have good documentation and i think at least for now i don't really need something more complex).
