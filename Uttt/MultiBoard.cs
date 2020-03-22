public class MultiBoard : BaseBaord<Board>
{
    public MultiBoard()
    {
        Board = new Board[,]
        {
                {new Board(0,0), new Board(0,1), new Board(0,2)},
                {new Board(1,0), new Board(1,1), new Board(1,2)},
                {new Board(2,0), new Board(2,1), new Board(2,2)}
        };
    }

    public Board ToBoard()
    {
        var boardGame = new Board(-1, -1);
        foreach (var cell in boardGame.Board)
        {
            var boardValue = Board[cell.Row, cell.Col].Pos.Value;
            //boardValue = (boardValue == Actor.Tie) ? Actor.Enemry : boardValue; // sub Tie for an Enemy for now.
            cell.Value = boardValue;
        }
        return boardGame;
    }
}
