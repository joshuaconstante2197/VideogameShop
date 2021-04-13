# Videogame Shop

This app is capable of receiving inventory and sales from a CSV file or add them manualy. It also has login capabilities with support for two roles, Manager and Employee.

## Table of Contents
1. [General Info](#general-info)
2. [Technologies](#technologies)
3. [Features](#features)
4. [How to Use](#how-to-use)
5. [Images](#images)
6. [Status](#status)
## General Info:

The app was thought out as if it were to be used by employees at a physical store to manage sells and inventory, as such sales can be logged as credit card purchases or cash purchases either trough uploading a CSV formatted file or by adding them manually.


## Technologies

Project is created with:
* ASP.NET
* MVC
* SQL Server
* JavaScript
* HTML
* CSS
* jQuery

## Features:
* Upload a CSV file that gets parsed and save into a SQL server database.
* Validates credit card purchases using Luhn's algorithm.
* Encrypts credit cards after they get validated.
* Notifies user when inventory is running low trough color coding.
* Supports two users. Manager and Employee.
* Passwords are encrypted using a hash salt.
* Only the manager can upload the CSV file and save data manually while the employee can only log data manually.
* Manager can manage users by changing their roles, adding or deleting users.

### To-do list:
* Improve UI.
* Filter sales by date trough sql query.
* Add or remove users from roles.

## How to Use:
1. In [FileLibrary/Data](https://github.com/joshuaconstante2197/VideogameShop/tree/master/VideogameShop.Library/Data) you will find the SQL queries named *SQL SQLQuery1.sql*. Run that first.
2. In the **Home** page you will see a button to populate the DB with users, this was only done for demostration purposes. Press that button to be able to acces the manager role.
3. Login as a manager: username is *admin* and password is *admin1*.
4. In the same [FileLibrary/Data](https://github.com/joshuaconstante2197/VideogameShop/tree/master/VideogameShop.Library/Data) folder you will find the CSV files named *Videogame shop - Inventory.csv* and *Videogame shop - Sales1.csv*. You can upload them from the **Orders** and **Product** pages respectively. 
5. You will see all the information displayed there, you can also just add more product manually or sales manually.
6. In the **Orders** page for credit card sales you will only see the last 4 digits of the credit card since the rest is encrypted.
7. In the **Manage Roles** page, if logged in as a manager you can add, delete or edit roles.

## Images:
1. Home Page ![alt text](https://github.com/joshuaconstante2197/VideogameShop/blob/master/VideogameShop.Library/Data/img/videogame-shop-main.PNG)
2. Product Page ![alt text](https://github.com/joshuaconstante2197/VideogameShop/blob/master/VideogameShop.Library/Data/img/videogame-shop-product.PNG)
3. Sales Page ![alt text](https://github.com/joshuaconstante2197/VideogameShop/blob/master/VideogameShop.Library/Data/img/videogame-shop-sales.PNG)
4. Roles Page ![alt text](https://github.com/joshuaconstante2197/VideogameShop/blob/master/VideogameShop.Library/Data/img/videogame-shop-roles.PNG)

## Status:

Project is: *in progress*




Created by [@joshua.co.dev](https://www.https://portfolio-website-4l9ay.ondigitalocean.app/projects/portfolio-item-piano.html.pl/) 
