# Gourmet-Swap

Gourmet Swap is community driven platform for local Chefs and foodies who value quality cuisine from local kitchens without the hassle and expense of dining out. Built off the idea of swapping dishes and recipes with your neighbor, GS offers a simple and straight forward search and discovery tools on a variety platforms including web/responsive featuring Google Maps and Directions implementation, an AI powered Bot built on top Azure's Language Understanding Intelligent Service L.U.I.S and a new Alexa Skill aimed at making ordering more convenient. Registered Chefs are provided an admin dashboard to manage and promote their meals, services and profiles. Site Administrators leverage a custom built CMS tool to promote custom content on the site.


Feature - Created a Contact Us page in which customers can send questions and an Admin Email Inbox where an admin receives questions from customers and replies back to the customers via Email using SendGrid email API. Implemented server-side pagination by sorting and filtering via SQL to optimize the email system. Created a framework for the blog management system that will allow users and cooks to create and read blogs in the future. 

Strategy – Using SQL built data tables and store procedures, a C# service class and REST endpoints were implemented with Web.Api to handle incoming requests and return valid responses. 






Upon message submission after passing the validations on Contact Us page, user's message is sent to an Admin Email Inbox and gets saved in the database. 

Admin is able to select received messages by individual checkboxes or select them all. Unread messages have a default status of 1. Archive button is enabled when a message or messages are checked. The status of a selected message is changed to 2 upon clicking on archive button and gets sent to a archive page. Status of a message is changed to 3 after it is read. Read messages are stored in a read page. Admin is able to search a message by customer's first name, last name, and email address. Reply button is only enabled when a single message is checked and an admin is able to reply to a customer using SendGrid email Api. 
