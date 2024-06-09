using System;

namespace Observer10
{
    using System;
    using System.Collections.Generic;


    public interface IObserver
    {
        void Update(Dictionary<string, int> scores);
    }


    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }

 
    public class Game : ISubject
    {
        private List<IObserver> observers;
        private Dictionary<string, int> scores;

        public Game()
        {
            observers = new List<IObserver>();
            scores = new Dictionary<string, int>();
        }

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update(scores);
            }
        }

        public void UpdateScore(string playerName, int score)
        {
            if (scores.ContainsKey(playerName))
            {
                scores[playerName] = score;
            }
            else
            {
                scores.Add(playerName, score);
            }
            NotifyObservers();
        }

        public Dictionary<string, int> GetScores()
        {
            return new Dictionary<string, int>(scores);
        }
    }

    public class Player : IObserver
    {
        private string playerName;
        private ISubject game;

        public Player(string name, ISubject game)
        {
            this.playerName = name;
            this.game = game;
            game.RegisterObserver(this);
        }

        public void Update(Dictionary<string, int> scores)
        {
            Console.WriteLine($"Aktualizacja wyniku {playerName}:");
            foreach (var score in scores)
            {
                Console.WriteLine($"Gracz {score.Key}: {score.Value} punktów");
            }
            Console.WriteLine();
        }

        public void Unsubscribe()
        {
            game.RemoveObserver(this);
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            Game game = new Game();

            Player player1 = new Player("Gracz1", game);
            Player player2 = new Player("Gracz2", game);
            Player player3 = new Player("Gracz3", game);

            game.UpdateScore("Gracz1", 10);
            game.UpdateScore("Gracz2", 15);
            game.UpdateScore("Gracz3", 20);

            player3.Unsubscribe();

            game.UpdateScore("Gracz1", 25);
            game.UpdateScore("Gracz2", 30);
        }
    }

}
