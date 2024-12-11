using System.Collections.Concurrent;

namespace Hilos_PiedraPapelTijera;

using System;
using System.Threading;

class HilosPiedraPapelTijera
{
    static string[] choices = { "Piedra", "Papel", "Tijera" };
    static Random random = new Random();

    static Dictionary<string, int> playerWins = new Dictionary<string, int>();

    static volatile bool countingLocked = false;
    static volatile bool addingPlayer = false;
    static volatile bool finishedRound = false;


    static Dictionary<string, string> scores = new Dictionary<string, string>();

    static void Main()
    {
        int rounds = 3;
        int players = 8;
        List<Thread> threads = new List<Thread>();
        var names = GenerateRandomName(players);
        for (int j = 0; j < players; j++)
        {
            var name = names[j];
            var player = new Thread(() => Play(name, players));
            threads.Add(player);
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
        // Thread player1 = new Thread(() => Play("Lucas", 2));
        // Thread player2 = new Thread(() => Play("Pepe", 2));
        //
        // player1.Start();
        // player2.Start();
        //
        // player1.Join();
        // player2.Join();
    }

    static void Play(string player, int numberPlayers)
    {
        var finished = false;
        var finishedCouting = false;
        var finishedAdding = false;
        
        var eleccion = choices[random.Next(choices.Length)];
        Thread.Sleep(random.Next(100, 500));

        while (finishedAdding == false || scores.ContainsKey(player) == false)
        {
            if (addingPlayer == false)
            {
                addingPlayer = true;
                Thread.Sleep(random.Next(100, 200));
                scores[player] = eleccion;
                addingPlayer = false;
                finishedAdding = true;
            }
        }

        Console.WriteLine($"{player} elige {eleccion}");
        
        while (finished == false)
        {
            Thread.Sleep(random.Next(100, 500));
            if (countingLocked)
            {
                finished = true;
                finishedCouting = true;
            }

            
            while (finishedCouting == false || countingLocked == false)
            {
                if (finishedRound) return;
                
                countingLocked = true;
                
                if (scores.Count != numberPlayers) continue;
                CheckWinner();
                finished = true;
                finishedCouting = true;
                scores.Clear();
                finishedRound = true;
            }
        }
    }

    static void CheckWinner()
    {
        Console.WriteLine("\nResultados de la ronda:");

        List<string> winners = new List<string>();
        foreach (var score in scores)
        {
            string playerChoice = score.Value;
            bool isWinner = true;

            foreach (var opponent in scores)
            {
                if (opponent.Key != score.Key)
                {
                    string opponentChoice = opponent.Value;
                    if (playerChoice == opponentChoice)
                    {
                        isWinner = false;
                        break;
                    }

                    switch (playerChoice)
                    {
                        case "Piedra":
                            if (opponentChoice == "Papel") isWinner = false;
                            break;
                        case "Papel":
                            if (opponentChoice == "Tijera") isWinner = false;
                            break;
                        case "Tijera":
                            if (opponentChoice == "Piedra") isWinner = false;
                            break;
                    }
                }

                if (isWinner)
                {
                    winners.Add(score.Key);
                    break;
                }
            }
        }

        if (winners.Count == 0)
        {
            Console.WriteLine("Empate");
        }

        foreach (var winner in winners)
        {
            Console.WriteLine($"El ganador es {winner} con {scores[winner]}!");
            if (!playerWins.ContainsKey(winner))
            {
                playerWins[winner] = 0;
            }

            playerWins[winner]++;
        }
    }

    static List<string> GenerateRandomName(int numberPlayers)
    {
        List<string> listNames = new List<string> {
            "Carlos", "Ana", "Luis", "Marta", "Pedro", "Sofia", "Juan", "Laura", "David", "Eva",
            "Elena", "Raul", "Isabel", "Santiago", "Paula", "Antonio", "María", "José", "Lucía", "Felipe",
            "Natalia", "Javier", "Carmen", "Miguel", "Beatriz", "Fernando", "Pablo", "Clara", "Ricardo", "Raquel",
            "Ángel", "Victoria", "Manuel", "Teresa"
        };
        var names = new List<string>();
        while (names.Count != numberPlayers)
        {
            var randomName = random.Next(0, listNames.Count - 1);
            names.Add(listNames[randomName]);
            listNames.RemoveAt(randomName);
        }
        return names;
    }
}