
using System.Collections.Generic;
using System.Linq;

public class BaseBaord<T>
{
    public T[,] Board { get; set; }
    public Cell Pos { get; set; }

    public bool IsFull => Pos.Value != Actor.None;
    public IEnumerable<T[]> Lines => Rows.Concat(Cols).Concat(Diagonals);

    public List<T[]> Diagonals
    {
        get
        {
            return new List<T[]>
                {
                    new T[3] { Board[0, 0], Board[1, 1], Board[2, 2] },
                    new T[3] { Board[0, 2], Board[1, 1], Board[2, 0] }
                };
        }
    }

    public List<T[]> Rows
    {
        get
        {
            return new List<T[]>
                {
                    new T[3] { Board[0, 0], Board[0, 1], Board[0, 2] },
                    new T[3] { Board[1, 0], Board[1, 1], Board[1, 2] },
                    new T[3] { Board[2, 0], Board[2, 1], Board[2, 2] }
                };
        }
    }

    public List<T[]> Cols
    {
        get
        {
            return new List<T[]> {
                    new T[3] { Board[0, 0], Board[1, 0], Board[2, 0] },
                    new T[3] { Board[0, 1], Board[1, 1], Board[2, 1] },
                    new T[3] { Board[0, 2], Board[1, 2], Board[2, 2] }
                };
        }
    }
}
