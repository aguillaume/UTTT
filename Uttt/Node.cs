using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Uttt
{
    public class Node
    {
        public Node Parent{ get; set; }
        public List<Node> Children{ get; set; }
        public State State { get; set; }

        public double Playouts { get; set; }

        public double Wins { get; set; }

        public double Score
        {
            get
            {
                return (Playouts == 0) ? 
                    double.MaxValue : 
                   (Wins / Playouts) + (Math.Sqrt(2) * Math.Sqrt(Math.Log(Parent.Playouts)/Playouts));
            }
        }
    }
}
