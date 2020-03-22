using System;
using System.Reflection.Metadata.Ecma335;

namespace Uttt
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameBaord = new Board(0,0);
            var algo = new Mcts();
            var player = new Player
            {
                Actor = Actor.Me
            };

            while(!gameBaord.IsFull)
            {
                var play = algo.FindBestPlay(gameBaord, player);
                gameBaord.UpdateCell(play.Row, play.Col, player.Actor);
                player = player.NextPlayer();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
