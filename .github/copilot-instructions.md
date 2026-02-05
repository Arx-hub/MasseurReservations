# Copilot instructions for MasseurReservations

Purpose: help AI coding agents become productive quickly working on this small, single-project C# console app.

- **Big picture:** single console app (solution: `MasseurReservations.sln`) in `MasseurReservations/` containing a minimal in-memory reservation system. See [MasseurReservations/Program.cs](MasseurReservations/Program.cs) for the main logic.

- **Architecture & data flow:**
  - `Program.cs` contains a simple `Reservation` model and `ReservationSystem` behavior (methods: `MakeReservation()`, `ViewReservations()`, `AskForCustomerName()`, `ReadChoice()`).
  - All reservations are stored in-memory in a static `List<Reservation>`; there is no persistence layer or external service.
  - Input/output is purely console-based; state is lost when the process exits.

- **Key patterns & conventions (project-specific):**
  - Date input uses `dd.MM.yyyy` (exact parse via `DateTime.TryParseExact` with `CultureInfo.InvariantCulture`).
  - Time slots are 30-minute intervals between 08:00 and 15:00; conflict detection uses exact `DateTime` equality (`reservations.Any(r => r.Slot == slot)`).
  - Defensive console handling: code checks for `Console.ReadLine()` returning `null` (input closed) and returns gracefully.

- **Build / run / debug:**
  - Build: `dotnet build MasseurReservations.sln` or `dotnet build MasseurReservations/MasseurReservations.csproj`.
  - Run from project folder: `dotnet run --project MasseurReservations/MasseurReservations.csproj` (or open solution in Visual Studio and launch).
  - Target framework observed in build outputs: .NET 9 (`net9.0`).

- **Testing:**
  - No test project exists. If you add tests, prefer a new `MasseurReservations.Tests` project using xUnit and run via `dotnet test`.

- **When changing behavior:**
  - Keep the console UX flow in `Program.cs` consistent (menu choices 1/2/3). Changes to date/time validation must preserve `dd.MM.yyyy` format unless you update user prompts accordingly.
  - If you add persistence, document migration steps and ensure existing console flows still work when persistence is unavailable (fallback to in-memory).

- **Integration points / external dependencies:**
  - Currently none: no DB, no network, no third-party services. NuGet usage is via the project file if added.

- **Examples to reference while coding:**
  - Add a new validation: update `MakeReservation()` in [MasseurReservations/Program.cs](MasseurReservations/Program.cs).
  - Adjust slot generation: change the `for` loop that builds `times` in `MakeReservation()`.

- **Notes for AI edits:**
  - Make minimal, focused changes; this repo is small and meant to stay simple. Avoid introducing heavy frameworks.
  - Preserve user-facing text and prompts unless explicitly improving clarity; tests (if added) should cover parsing and conflict detection.

If any section is unclear or you want more examples (e.g., adding a persistence adapter or tests), tell me which area to expand.
