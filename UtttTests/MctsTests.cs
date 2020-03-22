using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uttt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uttt.Tests
{
    [TestClass()]
    public class MctsTests
    {
        [TestMethod()]
        public void FindBestPlay_NextMoveWin()
        {
            var gameBaord = new Board(0, 0);

            gameBaord.Board[0, 0] = new Cell { Row = 0, Col = 0, Value = Actor.Me };
            gameBaord.Board[0, 2] = new Cell { Row = 0, Col = 2, Value = Actor.Me };
            gameBaord.Board[1, 1] = new Cell { Row = 1, Col = 1, Value = Actor.Me };

            gameBaord.Board[0, 1] = new Cell { Row = 0, Col = 1, Value = Actor.Enemry };
            gameBaord.Board[1, 0] = new Cell { Row = 1, Col = 0, Value = Actor.Enemry };
            gameBaord.Board[2, 0] = new Cell { Row = 2, Col = 0, Value = Actor.Enemry };

            Console.WriteLine(gameBaord.ToString());

            var algo = new Mcts();
            var player = new Player
            {
                Actor = Actor.Me
            };

            
            var play = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play.Row, play.Col, player.Actor);

            Console.WriteLine(gameBaord.ToString());

            var expectedPlay = new Play(2,2);
            Assert.AreEqual(expectedPlay.Row, play.Row);
            Assert.AreEqual(expectedPlay.Col, play.Col);
            Assert.IsTrue(gameBaord.IsFull);
            Assert.AreEqual(Actor.Me, gameBaord.Pos.Value);
        }

        //O|X|X
        //-|X|-
        //-|O|-

        //O|X|X
        //O|X|-
        //-|O|-

        //O|X|X
        //O|X|-
        //X|O|-

        [TestMethod()]
        public void FindBestPlay_NextMoveBlock2()
        {
            var gameBaord = new Board(0, 0);

            gameBaord.Board[0, 1] = new Cell { Row = 0, Col = 1, Value = Actor.Me };
            gameBaord.Board[0, 2] = new Cell { Row = 0, Col = 2, Value = Actor.Me };
            gameBaord.Board[1, 1] = new Cell { Row = 1, Col = 1, Value = Actor.Me };

            gameBaord.Board[0, 0] = new Cell { Row = 0, Col = 0, Value = Actor.Enemry };
            gameBaord.Board[2, 1] = new Cell { Row = 2, Col = 1, Value = Actor.Enemry };

            Console.WriteLine(gameBaord.ToString());

            var algo = new Mcts();
            var player = new Player { Actor = Actor.Enemry };


            var play = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play.Row, play.Col, player.Actor);

            Console.WriteLine(gameBaord.ToString());

            var expectedPlay = new Play(2, 0);
            Assert.AreEqual(expectedPlay.Row, play.Row);
            Assert.AreEqual(expectedPlay.Col, play.Col);
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);
        }

        [TestMethod()]
        public void FindBestPlay_NextMoveBlock()
        {
            var gameBaord = new Board(0, 0);

            gameBaord.Board[0, 0] = new Cell { Row = 0, Col = 0, Value = Actor.Me };
            gameBaord.Board[1, 1] = new Cell { Row = 1, Col = 1, Value = Actor.Me };

            gameBaord.Board[0, 2] = new Cell { Row = 0, Col = 2, Value = Actor.Enemry };
            gameBaord.Board[2, 2] = new Cell { Row = 2, Col = 2, Value = Actor.Enemry };

            Console.WriteLine(gameBaord.ToString());

            var algo = new Mcts();
            var player = new Player
            {
                Actor = Actor.Me
            };

            var play = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play.Row, play.Col, player.Actor);

            Console.WriteLine(gameBaord.ToString());

            var expectedPlay = new Play(1, 2);
            Assert.AreEqual(expectedPlay.Row, play.Row);
            Assert.AreEqual(expectedPlay.Col, play.Col);
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);
        }

        [TestMethod()]
        public void FindBestPlay_NextTwoMoveBlock()
        {
            var gameBaord = new Board(0, 0);

            gameBaord.Board[0, 0] = new Cell { Row = 0, Col = 0, Value = Actor.Me };
            gameBaord.Board[1, 1] = new Cell { Row = 1, Col = 1, Value = Actor.Me };

            gameBaord.Board[0, 2] = new Cell { Row = 0, Col = 2, Value = Actor.Enemry };
            gameBaord.Board[2, 2] = new Cell { Row = 2, Col = 2, Value = Actor.Enemry };

            Console.WriteLine(gameBaord.ToString());

            var algo = new Mcts();
            var player = new Player
            {
                Actor = Actor.Me
            };

            var play1 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play1.Row, play1.Col, player.Actor);

            Console.WriteLine(gameBaord.ToString());

            var expectedPlay1 = new Play(1, 2);
            Assert.AreEqual(expectedPlay1.Row, play1.Row);
            Assert.AreEqual(expectedPlay1.Col, play1.Col);
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);

            player = player.NextPlayer();
            var play2 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play2.Row, play2.Col, player.Actor);

            Console.WriteLine(gameBaord.ToString());

            var expectedPlay2 = new Play(1, 0);
            Assert.AreEqual(expectedPlay2.Row, play2.Row);
            Assert.AreEqual(expectedPlay2.Col, play2.Col);
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);
        }

        [TestMethod()]
        public void FindBestPlay_NextMovePreventFork()
        {
            var gameBaord = new Board(0, 0);

            gameBaord.Board[1, 1] = new Cell { Row = 1, Col = 1, Value = Actor.Me };
            gameBaord.Board[1, 2] = new Cell { Row = 1, Col = 2, Value = Actor.Me };

            gameBaord.Board[1, 0] = new Cell { Row = 1, Col = 0, Value = Actor.Enemry };
            gameBaord.Board[2, 2] = new Cell { Row = 2, Col = 2, Value = Actor.Enemry };

            Console.WriteLine(gameBaord.ToString());

            var algo = new Mcts();
            var player = new Player { Actor = Actor.Me };

            var play1 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play1.Row, play1.Col, player.Actor);

            Console.WriteLine(gameBaord.ToString());
            
            var safePlays = new List<Play>
            {
                new Play(0,0),
                new Play(0,1),
                new Play(2,0),
                new Play(2,1),
            };

            Assert.IsTrue(safePlays.Contains(play1));
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);

        }

        [TestMethod()]
        public void FindBestPlay_FullGameTie()
        {
            var gameBaord = new Board(0, 0);

            Console.WriteLine(gameBaord.ToString());

            var algo = new Mcts();
            var player = new Player { Actor = Actor.Me };

            var play1 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play1.Row, play1.Col, player.Actor);
            Console.WriteLine(gameBaord.ToString());
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);

            player = player.NextPlayer();
            var play2 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play2.Row, play2.Col, player.Actor);
            Console.WriteLine(gameBaord.ToString());
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);

            player = player.NextPlayer();
            var play3 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play3.Row, play3.Col, player.Actor);
            Console.WriteLine(gameBaord.ToString());
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);

            player = player.NextPlayer();
            var play4 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play4.Row, play4.Col, player.Actor);
            Console.WriteLine(gameBaord.ToString());
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);

            player = player.NextPlayer();
            var play5 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play5.Row, play5.Col, player.Actor);
            Console.WriteLine(gameBaord.ToString());
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);

            player = player.NextPlayer();
            var play6 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play6.Row, play6.Col, player.Actor);
            Console.WriteLine(gameBaord.ToString());
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);

            player = player.NextPlayer();
            var play7 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play7.Row, play7.Col, player.Actor);
            Console.WriteLine(gameBaord.ToString());
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);

            player = player.NextPlayer();
            var play8 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play8.Row, play8.Col, player.Actor);
            Console.WriteLine(gameBaord.ToString());
            Assert.IsFalse(gameBaord.IsFull);
            Assert.AreEqual(Actor.None, gameBaord.Pos.Value);

            player = player.NextPlayer();
            var play9 = algo.FindBestPlay(gameBaord, player);
            gameBaord.UpdateCell(play9.Row, play9.Col, player.Actor);
            Console.WriteLine(gameBaord.ToString());
            Assert.IsTrue(gameBaord.IsFull);
            Assert.AreEqual(Actor.Tie, gameBaord.Pos.Value);
        }
    }
}