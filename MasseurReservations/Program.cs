using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace MasseurReservations
{
    // Simple reservation model holding the reserved DateTime
    class Reservation
    {
        public DateTime Slot { get; set; }
        public override string ToString() => Slot.ToString("HH:mm dd.MM.yyyy");
    }

    class ReservationSystem
    {
        // In-memory storage for reservations
        static List<Reservation> reservations = new List<Reservation>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Press a number to select an option:");
                Console.WriteLine("1. Make a reservation");
                Console.WriteLine("2. View all reservations");
                Console.WriteLine("3. Exit");
                Console.Write("> ");
                string choice = Console.ReadLine();
                if (choice == null)
                {
                    Console.WriteLine("Input closed. Exiting.");
                    return;
                }

                switch (choice)
                {
                    case "1":
                        MakeReservation();
                        break;
                    case "2":
                        ViewReservations();
                        break;
                    case "3":
                        Console.WriteLine("Exiting the program.");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // Step: Prompt user to enter a date (dd.MM.yyyy), validate, choose a 30-min time slot between 08:00 and 15:00, check conflicts, and store reservation
        static void MakeReservation()
        {
            Console.Clear();
            Console.WriteLine("=== Make a Reservation ===");
            Console.WriteLine("Enter a date in the format dd.MM.yyyy (day and month must be two digits, year four digits).");

            DateTime today = DateTime.Today;
            DateTime maxDate = today.AddYears(2);
            DateTime selectedDate;

            while (true)
            {
                Console.Write("Date (dd.MM.yyyy): ");
                string dateInput = Console.ReadLine();
                if (dateInput == null)
                {
                    Console.WriteLine("Input closed. Returning to main menu.");
                    return;
                }
                dateInput = dateInput.Trim();
                if (DateTime.TryParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedDate))
                {
                    if (selectedDate < today)
                    {
                        Console.WriteLine("Date is in the past. Please enter today or a future date.");
                        continue;
                    }
                    if (selectedDate > maxDate)
                    {
                        Console.WriteLine("Date is more than 2 years in the future. Please select an earlier date.");
                        continue;
                    }
                    break;
                }

                Console.WriteLine("Invalid date format or non-existent date. Please use dd.MM.yyyy and ensure the date exists (e.g., April has 30 days).");
            }

            // Generate 30-minute time slots from 08:00 to 15:00
            TimeSpan start = new TimeSpan(8, 0, 0);
            TimeSpan end = new TimeSpan(15, 0, 0);
            List<TimeSpan> times = new List<TimeSpan>();
            for (TimeSpan t = start; t <= end; t = t.Add(TimeSpan.FromMinutes(30)))
                times.Add(t);

            Console.WriteLine("Available times:");
            for (int i = 0; i < times.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {times[i]:hh\\:mm}");
            }

            int timeChoice = ReadChoice(1, times.Count, "Choose a time: ");
            if (timeChoice == -1)
            {
                Console.WriteLine("Input closed. Returning to main menu.");
                return;
            }
            TimeSpan selectedTime = times[timeChoice - 1];

            DateTime slot = selectedDate.Date + selectedTime;

            // Make sure combined date and time isn't in the past
            if (slot < DateTime.Now)
            {
                Console.WriteLine("That date/time is in the past. Please pick a future date/time. Press Enter to continue.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"You selected {slot:HH:mm dd.MM.yyyy}. Confirm? (Y/N)");
            string confirm = Console.ReadLine();
            if (confirm == null)
            {
                Console.WriteLine("Input closed. Returning to main menu.");
                return;
            }
            confirm = confirm.Trim().ToUpper();
            if (confirm != "Y")
            {
                Console.WriteLine("Reservation cancelled. Press Enter to continue.");
                Console.ReadLine();
                return;
            }

            // Check for conflicts
            if (reservations.Any(r => r.Slot == slot))
            {
                Console.WriteLine("Sorry, that time slot is already taken. Press Enter to continue.");
                Console.ReadLine();
                return;
            }

            // Add reservation
            reservations.Add(new Reservation { Slot = slot });
            Console.WriteLine("Reservation created successfully! Press Enter to continue.");
            Console.ReadLine();
        }

        // Step: Display all reservations (sorted)
        static void ViewReservations()
        {
            Console.Clear();
            Console.WriteLine("=== All Reservations ===");

            if (!reservations.Any())
            {
                Console.WriteLine("No reservations yet.");
            }
            else
            {
                var ordered = reservations.OrderBy(r => r.Slot);
                int i = 1;
                foreach (var r in ordered)
                {
                    Console.WriteLine($"{i}. {r}");
                    i++;
                }
            }

            Console.WriteLine("\nPress Enter to return to the main menu.");
            Console.ReadLine();
        }

        // Helper: read an integer choice within a range with validation
        static int ReadChoice(int min, int max, string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (input == null)
                    return -1;
                input = input.Trim();
                if (int.TryParse(input, out int choice) && choice >= min && choice <= max)
                    return choice;

                Console.WriteLine($"Please enter a number between {min} and {max}.");
            }
        }
    }
}
