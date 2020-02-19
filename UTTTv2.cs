using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateTicTacToe
{
    //https://jeffbradberry.com/posts/2015/09/intro-to-monte-carlo-tree-search/
    //https://www.analyticsvidhya.com/blog/2019/01/monte-carlo-tree-search-introduction-algorithm-deepmind-alphago/

    public class UTTTv2
    {
        static void Main(string[] args)
        {

        }

    }

    public class Board {
        public void Start()
        {
            //# Returns a representation of the starting state of the game.
        }

        public void CurrentPlayer(object state)
        {
            //# Takes the game state and returns the current player's number.
        }

        public void NextState(object state, object play)
        {
            //# Takes the game state, and the move to be applied. Returns the new game state.
        }

        public void LegalPlays(object[] stateHistory)
        {
            //# Takes a sequence of game states representing the full game history, and returns the full list of moves that are legal plays for the current player.
        }

        public void Winner(object[] stateHistory)
        {
            /*
             * # Takes a sequence of game states representing the full
             * # game history.  If the game is now won, return the player
             * # number.  If the game is still ongoing, return zero.  If
             * # the game is tied, return a different distinct value, e.g. -1.
            */
        }
    }

    public class MonteCarloAi
    {
        public MonteCarloAi(Board board, object args)
        {
            //# Takes an instance of a Board and optionally some keyword
            //# arguments.  Initializes the list of game states and the
            //# statistics tables.

            //self.board = board
            //self.states = []
            //seconds = kwargs.get('time', 30)
            //self.calculation_time = datetime.timedelta(seconds = seconds)
            //self.max_moves = kwargs.get('max_moves', 100)
            //self.wins = {}
            //self.plays = { }
        }

        public void Update(object state)
        {
            //# Takes a game state, and appends it to the history.
        }

        public void GetPlay()
        {
            //# Causes the AI to calculate the best move from the
            //# current game state and return it.

            //begin = datetime.datetime.utcnow()
            //while datetime.datetime.utcnow() - begin < self.calculation_time:
            //    self.run_simulation()
        }

        public void RunSimualtion()
        {
            //# Plays out a "random" game from the current position,
            //# then updates the statistics tables with the result.

            //visited_states = set()
            //states_copy = self.states[:]
            //state = states_copy[-1]
            //player = self.board.current_player(state)

            //expand = True
            //for t in xrange(self.max_moves):
            //    legal = self.board.legal_plays(states_copy)

            //    play = choice(legal) //randmon move
            //    state = self.board.next_state(state, play)
            //    states_copy.append(state)

            //    winner = self.board.winner(states_copy)
            //    if winner:
            //        break
        }
    }

}
