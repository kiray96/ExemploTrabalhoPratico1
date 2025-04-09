using System;
using System.Diagnostics;
using Humanizer;
using Spectre.Console;

namespace SimonSays
{
    public class Game
    {
        /// <summary>
        /// The provider responsible for generating the patterns used in the 
        /// game.
        /// </summary>
        private readonly CommandProvider commandProvider;

        /// <summary>
        /// A list to store the last 5 game results for the game stats board.
        /// </summary>
        private readonly GameResult[] gameStats;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// Sets up the command provider, and initializes the game
        /// stats.
        /// </summary>
        public Game()
        {
            // ////////// => TO IMPLEMENT <= //////////// //
            commandProvider = new CommandProvider();
            gameStats = new GameResult[5];
        }

        /// <summary>
        /// Displays the main menu of the game and prompts the user to choose
        /// an option. The available choices are to start the game, view the 
        /// game stats board, or quit the game.
        /// </summary>
        /// <remarks>
        /// This method uses the <see cref="Spectre.Console"/> library to 
        /// display the main menu and handle user input for choosing different 
        /// options. The game will continue prompting 
        /// the user until they choose to quit.
        /// </remarks>
        public void ShowMenu()
        {
            while (true)
            {
                AnsiConsole.Clear();
                string choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold yellow]Simon Says[/]")
                        .AddChoices("Start Game", "View Game Stats", "Quit"));

                switch (choice)
                {
                    case "Start Game":
                        StartGame(); // ////////// => TO IMPLEMENT <= //////////// //
                        break;
                    case "View Game Stats":
                        ShowGameStats(); // ////////// => TO IMPLEMENT <= //////////// //
                        break;
                    case "Quit":
                        return;
                }
            }
        }

        private void StartGame()
        {
            int round = 1;
            Stopwatch stopwatch = new Stopwatch();

            while (true)
            {
                string pattern = commandProvider.GeneratePattern(round); // ////////// => TO IMPLEMENT <= //////////// //
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[bold green]Simon Says Follow this" + 
                    " Pattern:[/]");
                AnsiConsole.MarkupLine($"[italic yellow]{pattern}[/]");

                stopwatch.Start();
                string userInput = AnsiConsole.Ask<string>("\n[bold cyan]Repeat"
                    + " the Pattern (use W, A, S, D):[/] ");
                stopwatch.Stop();

                bool isCorrect = pattern == userInput; // ////////// => TO IMPLEMENT <= //////////// //

                if (!isCorrect)
                {
                    double timeTaken = stopwatch.Elapsed.TotalSeconds; // ////////// => TO IMPLEMENT <= //////////// //
                    stopwatch.Reset(); // *********** tenho que por reset para voltar a contar o tempo ************

                    AnsiConsole.MarkupLine("\n[bold red]You Lost![/]");
                    AnsiConsole.MarkupLine(
                        $"[bold]Time Taken:[/] {timeTaken:F2} Seconds");
                    AnsiConsole.MarkupLine(
                        $"[bold]You Lost at Round:[/] {round}");

                    // Shift existing entries
                    // *****************do maior para o mais pequeno no array*********************
                    for (int i = gameStats.Length - 1; i > 0; i--)
                    {
                        // ////////// => TO IMPLEMENT <= //////////// //
                        gameStats[i] = gameStats[i - 1];
                    }

                    // Add new result at the beginning
                    gameStats[0] = new GameResult(pattern, timeTaken, round);// ////////// => TO IMPLEMENT <= //////////// //

                    AnsiConsole.Markup("\n[bold green]Press Enter to Return to"
                        + " the Menu...[/]");
                    Console.ReadLine();
                    break;
                }

                round++;
            }
        }

        private void ShowGameStats()
        {
            AnsiConsole.Clear();
            Table table = new Table();
            table.AddColumn("#");
            table.AddColumn("Round");
            table.AddColumn("Time Taken (s)");
            table.AddColumn("Losing Pattern");

            for (int i = 0; i < gameStats.Length; i++)
            {
                if (gameStats[i] != null)
                {

                    if (gameStats[i] == null)
                    {
                        // ////////// => TO IMPLEMENT = RASTEIRA? = //////////// //
                    }

                    // Add row to table
                    // Table.AddRow() only accepts strings

                    // ************* Tenho que mudar para strings **************
                    // ********* cada valor é uma coluna QUE SÓ ACEITA STRINGS *************
                    // ********* POSSO QUEBRAR A LINHA A CADA VIRGULA *************
                    // ////////// => TO IMPLEMENT <= //////////// //
                    table.AddRow((i + 1).ToString(), gameStats[i].Round.ToString(), 
                    $"{gameStats[i].TimeTaken :F2}", gameStats[i].LosingPattern);
                }
            }

            AnsiConsole.Write(table);
            AnsiConsole.Markup(
                "\n[bold green]Press Enter to Return to Menu...[/]");
            Console.ReadLine();
        }
    }
}