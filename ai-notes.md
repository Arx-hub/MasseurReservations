Vaihe 1

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
Vaihe 2 

Promptti, jossa pyydetään käyttäjän nimeä: 

I want the system to ask for the customer’s/user’s name after choosing the available time slot. Make the method and explain it. Save the name into the same list as the reserved time. Dont change the original code. 

 

AI’s generated code: 

Added a CustomerName property to Reservation and updated ToString() to show the name when present. 
Added a new method: AskForCustomerName() — it prompts until a non-empty name is entered and returns null if input was closed. 
Updated MakeReservation() to call AskForCustomerName() after the user confirms the slot, and save the name together with the slot: 
reservations.Add(new Reservation { Slot = slot, CustomerName = customerName }); 

 

Arvio: promptti toimi, koska pyydetty muutos oli pieni. 
-----------------------------------------------
Vaihe 3 

Promptti, joka estää päällekkäisten ajanvarauksien tekoa, ja lisäksi poistaa ne näkyvistä vapaissa ajoissa: 

Haluamme päivittää C#:lla toteutetun konsolipohjaisen ajanvarausjärjestelmämme siten, että jokainen varaus kestää tasan yhden tunnin. Esimerkiksi, jos asiakas varaa ajan klo 13:00, varaus päättyy automaattisesti klo 14:00. Kun yhden tunnin aikavaraus on tehty, järjestelmän tulee poistaa koko aikaväli (klo 13:00–14:00) vapaiden aikojen listalta. Käyttäjä ei saa nähdä eikä valita aloitusaikoja, jotka menevät päällekkäin jo olemassa olevan varauksen kanssa. Pyydämme suunnittelemaan tähän tarvittavan logiikan ja metodit, jotka estävät päällekkäiset varaukset. 

 

AI:n generoitu koodi: 

Updated: Program.cs — added 1-hour appointment behavior. 

Added: AppointmentDuration constant and helper methods: 
GeneratePotentialStartTimes() — builds 30-min start points where a 1-hour appointment fits (08:00–14:00). 
GetAvailableStartTimesForDate(DateTime) — filters starts that would overlap existing reservations. 
IsSlotAvailable(DateTime) — conflict check using 1-hour intervals. 
IntervalsOverlap(...) — interval overlap helper. 
 

Saatavilla olevat aloitusajat sisältävät nyt vain ne ajat, joihin kokonainen yhden tunnin varaus mahtuu eikä päällekkäisyyksiä olemassa olevien varausten kanssa synny. 
Varauksen yhteydessä järjestelmä tarkistaa yhden tunnin varausten päällekkäisyydet ja estää ristiriitaiset varaukset. 
Mitään olemassa olevia käyttäjälle näkyviä toimintoja ei poistettu; muutokset ovat vähäisiä ja rajattuja. 

Arvio: Unohdettiin kirjoittaa prompttiin ensin, ettei tekoäly muokkaisi jo olemassa olevaa koodia, jolloin se teki liikaa ja sekoitti ohjelman. Yritimme uudelleen ja sit meni ensimmäisellä oikein. 

----------------------------------------------
Vaihe 4 

Prompt that will be used to enable the program to delete reservations:  

In the main menu of the program I want to have an option to delete specific reservations. The process to delete reservations should go as follows, first you select delete reservations from the initial menu, then you get prompted choose whether you want to delete by date or name. If you choose date, then the date must be inputted in the previously used format. The program will return all the appointments under the selected date. Each of them will have a number assigned to it so when asked to delete one writing the number that is assigned to it will suffice. Example: 

3. Delete reservations 

3 

Would you like to find the reservation you wish to delete by [1] date or [2] name? 
1 

Please write the date in the following format dd.MM.yyyy 
12.03.2026 

These are the reservations for that day, please select which one you would like to delete: 

[1] 8:30 Maria 
[2] 12:30 Juana 
[3] 13:00 Paca 
[4] 14:00 Rosita 

3 

Do you wish to delete Paca’s reservation at 13:00 (Y/N) 
Y 

Paca’s reservation at 13:00 has been delete. Press enter to continue 

After this, the program should take you back to the main menu. Alternatively the program should work similarly when choosing to delete by name instead of asking the date the program should ask for the name. Results will be returned regardless of if the capital letters match or not. Of course, include the usual safeguards and making sure that that for example if it asks for the date, writing a letter will not be valid so the user will be prompted to write a valid time format. Do not change the existing code. And make sure you include a brief summary of what you changed and did.  


AI:n generoitu koodi: 

Updated Program.cs main menu: 
Added option 3. Delete reservations and shifted Exit to 4. 
Added call to DeleteReservation() in the main switch. 

Added new methods: 
DeleteReservation() — asks whether to delete by date or name and dispatches. 
DeleteByDate() — prompts for dd.MM.yyyy, lists that day's reservations, allows selection and deletion with confirmation. 
DeleteByName() — prompts for a name (case-insensitive), lists matches, allows selection and deletion with confirmation. 

Input validation and safeguards: 
Date input uses DateTime.TryParseExact and re-prompts on invalid format. 
All flows handle input closed (null) gracefully and return to the main menu. 
Name matching is case-insensitive using StringComparison.OrdinalIgnoreCase. 
Kept changes local and minimal; preserved existing flows and the in-memory reservations list. 

 
Arvio: Promptti toimi, koska se oli hyvin yksityiskohtainen. 

-------------------------------------------------
Vaihe 5 

Skenaario 8 

Promptti onnistuneiden varauksen hakuun: 

I want a similar search function that the delete function has. So when you select View reservations, it gives you the option to choose based on date or name. Make sure it ignores capital letters. Dont make changes to the original code. Explain the changes you made. 

 

AI:n generoitu koodi: 

Modified Program.cs: 
ViewReservations() now prompts: view by date, by name, or view all. 
Added ViewReservationsByDate() and ViewReservationsByName() that: 
Use dd.MM.yyyy parsing for date input. 
Perform case-insensitive name matching (StringComparison.OrdinalIgnoreCase). 
Display matched reservations in sorted order and wait for Enter to return. 
Existing behavior to list all reservations preserved as the “all” option. 
No existing data structures or deletion/booking behavior changed; all additions are new methods and small flow changes that reuse the in-memory reservations list. 

Arvio: promptti toimi hyvn, sillä se perustui edelliseen prompttiin. 