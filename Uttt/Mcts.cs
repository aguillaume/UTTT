using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uttt
{
    public class Mcts
    {
        Player _player;
        readonly Random _rand = new Random();
        public Play FindBestPlay(Board board, Player player)
        {
            _player = player;

            var tree = new Tree
            {
                Root = new Node
                {
                    Parent = null,
                    Children = null,
                    State = new State
                    {
                        Board = board,
                        Player = _player.NextPlayer()
                    }
                }
            };

            Play bestMove = null;
            int simCount = 0;
            var timeStart = DateTime.Now;
            while ((DateTime.Now - timeStart).TotalMilliseconds < 90)
            {
                var selectedNode = Selection(tree) ?? tree.Root;

                //if (selectedNode.State.Board.IsFull)
                //{
                //    Err("selectedNode isFull exit");
                //    bestMove = selectedNode.State.Play;
                //    break;
                //}

                if (!selectedNode.State.Board.IsFull)
                {
                    Expansion(selectedNode);
                    //var endGame = selectedNode.Children.FirstOrDefault(c => c.State.Board.IsFull);

                    //if (endGame != null)
                    //{
                    //    bestMove = BackTrackMove(endGame).State.Play;
                    //    Err("EndGame exit");
                    //    Err("selectedNode");
                    //    Err(selectedNode.State.Board.ToString());
                    //    Err("endGame Child");
                    //    Err(endGame.State.Board.ToString());
                    //    Err("-------------------");
                    //    break;
                    //}

                    selectedNode = selectedNode.Children.First();
                }

                var playout = Simulation(selectedNode);
                BackPropagation(selectedNode, playout);
                simCount++;
            }

            if (bestMove == null)
            {
                bestMove = tree.Root.Children.Aggregate((c1, c2) => (c1.Score > c2.Score) ? c1 : c2).State.Play;
            }
            foreach (var item in tree.Root.Children)
            {
                Err($"Play: {item.State.Play} Score: {item.Score}  Plays: {item.Playouts}.");
            }
            Err($"Playouts: {tree.Root.Playouts}. Best Move: {bestMove}");
            return bestMove;
            
        }

        private Node Selection(Tree tree)
        {
            // select best move for the player
            if (tree.Root.Children == null) return null;

            var go = true;
            var start = tree.Root;
            Node bestChild = null;
            while(go)
            {
                foreach (var child in start.Children)
                {
                    if (bestChild == null)
                    {
                        bestChild = child;
                        continue;
                    }

                    if (child.Score > bestChild.Score)
                    {
                        bestChild = child;
                    }
                }

                if (bestChild.Children != null && bestChild.Children.Any())
                {
                    start = bestChild;
                    bestChild = null;
                }
                else
                {
                    go = false;
                }
            }

            return bestChild;
        }

        private void Expansion(Node node)
        {
            var newNodes = new List<Node>();
            var validMoves = node.State.Board.GetValidMoves();
            foreach (var move in validMoves)
            {
                var newNode = new Node
                {
                    Parent = node
                };

                var newState = new State
                {
                    Player = node.State.Player.NextPlayer(),
                    Board = new Board(node.State.Board),
                    Play = move
                };
                newState.Board.UpdateCell(move.Row, move.Col, newState.Player.Actor);

                newNode.State = newState;
                newNodes.Add(newNode);
            }

            node.Children = newNodes;
        }

        private Actor Simulation(Node node)
        {
            var boardCopy = new Board(node.State.Board);
            var player = node.State.Player.NextPlayer();

            while(!boardCopy.IsFull)
            {
                var validMoves = boardCopy.GetValidMoves();
                var randmonMove = validMoves[_rand.Next(0, validMoves.Count)];
                boardCopy.UpdateCell(randmonMove.Row, randmonMove.Col, player.Actor);

                if (boardCopy.IsFull)
                {
                    break;
                }

                player = player.NextPlayer();
            }

            return boardCopy.Pos.Value;
        }

        private void BackPropagation(Node node, Actor playout)
        {
            var propagationNode = node;

            while(propagationNode != null)
            {
                propagationNode.Playouts++;
                if (propagationNode.State.Player.Actor == playout)
                {
                    propagationNode.Wins += 1;
                }
                else if (playout == Actor.Tie)
                {
                    propagationNode.Wins += 0.9;
                }

                propagationNode = propagationNode.Parent;
            }
        }

        private void Err(string v)
        {
            Console.Error.WriteLine(v);
        }

    }
}
