using System;
using System.Collections.Generic;
using System.Linq;
using Uttt;

public class Board : BaseBaord<Cell>
{
    public Board(int row, int col)
    {
        Pos = new Cell(row, col);
        Board = new Cell[,]
        {
                {new Cell(0,0), new Cell(0,1), new Cell(0,2)},
                {new Cell(1,0), new Cell(1,1), new Cell(1,2)},
                {new Cell(2,0), new Cell(2,1), new Cell(2,2)}
        };
        Pos.Value = Actor.None;
    }

    public Board(Board board) : this(board.Pos.Row, board.Pos.Col)
    {
        Pos.Value = board.Pos.Value;
        foreach (var cell in board.Board)
        {
            Board[cell.Row, cell.Col] = new Cell(cell);
        }
    }

    public void UpdateCell(int row, int col, Actor actor)
    {
        Board[row, col].Value = actor;
        UpdateState();
        //Err(ToString());
        //Err($"The Board State has been updated. {actor} placed at {row} {col}. The board state is: {Pos.Value}. Board is full: {IsFull}");
    }

    internal List<Play> GetValidMoves()
    {
        var validMoves = new List<Play>();
        foreach (var cell in Board)
        {
            if (cell.Value == Actor.None)
            {
                validMoves.Add(new Play(cell.Row, cell.Col));
            }
        }

        return validMoves;
    }

    private void Err(string v)
    {
        Console.Error.WriteLine(v);
    }

    private void UpdateState()
    {
        //if (IsFull) return; DO NOT ESCAPE BECAUSE IN SIMULATION STATE NEEDS TO BE RE-EVALUATED 

        // Is board a Tie?
        if ((Rows.Sum(r => r.Count(c => c.Value != Actor.None)) == 9))
        {
            Pos.Value = Actor.Tie;
            return;
        }

        // Any line has 3 of the same?
        foreach (var line in Lines)
        {
            var val1 = line.First().Value;
            if (val1 != Actor.None && line.All(c => c.Value == val1))
            {
                Pos.Value = val1;
                return;
            }
        }

        Pos.Value = Actor.None; // SET TO NONE IN CASE SIMULATION UPDATED INITIAL VALUE
    }

    public override string ToString()
    {
        var result = "";
        result += $"{ToXO(Board[0, 0].Value)}|{ToXO(Board[0, 1].Value)}|{ToXO(Board[0, 2].Value)}\n";
        result += $"{ToXO(Board[1, 0].Value)}|{ToXO(Board[1, 1].Value)}|{ToXO(Board[1, 2].Value)}\n";
        result += $"{ToXO(Board[2, 0].Value)}|{ToXO(Board[2, 1].Value)}|{ToXO(Board[2, 2].Value)}\n";
        return result;
    }

    private string ToXO(Actor str)
    {
        switch (str)
        {
            case Actor.None:
                return "-";
            case Actor.Me:
                return "X";
            case Actor.Enemry:
                return "O";
            default:
                return "E";
        }
    }
}
