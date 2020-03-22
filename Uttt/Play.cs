using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Uttt
{
    public class Play : IEquatable<Play>
    {
        public Play (int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }

        public bool Equals([AllowNull] Play other)
        {
            return this.Row == other.Row &&
                this.Col == other.Col;
        }

        public override string ToString()
        {
            return $"{Row} {Col}";
        }
    }
}