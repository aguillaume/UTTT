using System;
using System.Collections.Generic;
using System.Text;

namespace Uttt
{
    public class Minimax
    {
        Tree tree = new Tree();

        internal Play FindBestPlay(Board gameBaord, Player playerNo)
        {
            tree.Root = new Node
            {
                State = new State
                {
                    Board = gameBaord,
                    // passed in board corresponds to the previous move, so the player for that move should be the opponent
                    Player = playerNo.NextPlayer() 
                }
            };

            var validmoves = gameBaord.GetValidMoves();





            return new Play(0, 0);
        }
    }
}
