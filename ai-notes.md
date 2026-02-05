Tekoälylle promptattu intro:
I want to create a program. First we would like to build the basic logic of the program. The logic should be contained entirely within the terminal, no visual or UI and the code should be written in C#. We are building a reservation system for a masseur. The program should initially have options to exit, create a reservation, or view all the reservations. The user should be able to make a basic reservation under their own name at a specific time.  

Example of how the program displays reservations: 

Press a number to select an option:  
1.Kjasfkjhaksjf 
2.Laskdhbglahsbdg 
3.Aghahdgkah 
4.Make a reservation 
5.Exit  

Please provide a year  
2026 

Please provide a number for the month (in numbers) 
2 

Please provide a day 
6 

These are the available times that day:  

[1] 12.00 
[2] 13.00  
[3] 14.00  
Please select an available time. 
-----------------------------------------------
AI: “Make a reservation: 

Shows 3 date options (today, tomorrow, day after). 
Shows 4 fixed time slots (09:00, 11:00, 13:00, 15:00). 
Validates numeric input and confirmation. 
Checks for existing reservation at that exact DateTime. 
Adds to an in-memory list if free.” 
 

Already NOT what we asked for. So we wrote this: 

Change the date format to hh:mm dd.mm.yyyy. When making a reservation its supposed to ask the user for a date, not give the 3 date options that you wrote right now. it should allow dates that range 2 years in advance, and also not allow past dates. it should also take into consideration that some months only have 30 days and also the month should be written in double digits and the year in 4 digits. The time span should be displayed in 30 minute intervals from 8 am to 3 pm.

Note: Maybe the presantation wasn't to the AI's liking and that's why the AI started doing it differently with the dates and hours.
------------------------------------------------
