namespace Hilos_PiedraPapelTijera;
class PiedraPapelTijera
{
    static readonly Random Random = new Random();

    static void Main()
    {
        int playersCount = 16;
        var playerNames = GenerateRandomName(playersCount);
        var gameManager = new Thread(() => ManageGame(playerNames));

        gameManager.Start();
        gameManager.Join();
    }

    static void ManageGame(List<string> playerNames)
    {
        while (playerNames.Count > 1)
        {
            Console.WriteLine($"\nRonda con {playerNames.Count} players");

            List<string> winners = new List<string>();
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < playerNames.Count; i += 2)
            {
                string player1 = playerNames[i];
                string player2 = playerNames[i + 1];

                var match = new Thread(() =>
                {
                    string winner = PlayMatch(player1, player2);
                    lock (winners)
                    {
                        winners.Add(winner);
                    }
                });

                threads.Add(match);
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            playerNames = winners;
        }
        Console.WriteLine($"\nGanador del torneo: {playerNames[0]}");
    }

    static string PlayMatch(string player1, string player2)
    {
        string[] choices = ["Piedra", "Papel", "Tijera"];
        string choice1 = choices[Random.Next(choices.Length)];
        string choice2 = choices[Random.Next(choices.Length)];

        Console.WriteLine($"\t{player1} elige {choice1} vs {player2} elige {choice2}");

        if (choice1 == choice2)
        {
            return PlayMatch(player1, player2);
        }

        if ((choice1 == "Piedra" && choice2 == "Tijera") ||
            (choice1 == "Papel" && choice2 == "Piedra") ||
            (choice1 == "Tijera" && choice2 == "Papel"))
        {
            Console.WriteLine($"\t\tGana {player1}");
            return player1;
        }
        else
        {
            Console.WriteLine($"\t\tGana {player2}");
            return player2;
        }
    }

    static List<string> GenerateRandomName(int numberPlayers)
    {
        List<string> listNames =
        [
            "Carlos", "Ana", "Luis", "Marta", "Pedro", "Sofia", "Juan", "Laura", "David", "Eva",
            "Elena", "Raul", "Isabel", "Santiago", "Paula", "Antonio", "María", "José", "Lucía", "Felipe",
            "Natalia", "Javier", "Carmen", "Miguel", "Beatriz", "Fernando", "Pablo", "Clara", "Ricardo", "Raquel",
            "Ángel", "Victoria", "Manuel", "Teresa"
        ];
        var names = new List<string>();
        while (names.Count != numberPlayers)
        {
            var randomName = Random.Next(0, listNames.Count);
            names.Add(listNames[randomName]);
            listNames.RemoveAt(randomName);
        }
        return names;
    }
}
