# alex-project1
# Store Order App

## Project Description
Web application that simulates order placing platform. Customers can register and place order to different locations with inventories consisting of a variety of products.


## Technologies Used

* ASP.NET Core 3.1
* Entity Framework Core
* SSMS/T-SQL
* Azure CI/CD
* Azure Web Services
* xUnit - version 2.4.0

## Features

* Register a new customer
* Search customers by first, last, or fullname
* Search locations by their name
* See locations inventory and current stock
* See customer details including history of placed orders
* See locations details including history of orders placed at each specific location
* Place orders containing multiple products for customers at specific location 

## To-do List

* Allow edit product amount in cart
* Allow customers to remove product selections in cart
* Set up login page so customer doesn't have to provide registration details every time they place an order
* Add email field to database to distinguish customers by email instead of name

## Getting Started

Current Live Version: https://alex-storeapp.azurewebsites.net

Build:
(Windows)
* git clone https://github.com/2011-nov02-net/alex-project1
* run with dotnet run

## Usage

* Once app is running you should see a Welcome page displaying a list of customers
* You can search and select a customer and click order history to see a their order history
* To see list of stores you can navigate to the stores page by clicking the stores button in the navigation bar
* There you can see a locations order history and inventory
* To add an item to cart you can select the add to cart option on a specific product which will prompt you for an amount
* To place the order you can navigate to the cart page by clicking the shopping cart button in the navigation bar or at the bottom of the inventory page and then clicking the checkout option which will prompt you to enter your registration details

## License

This project uses the following license: [MIT License](<https://opensource.org/licenses/MIT>).
