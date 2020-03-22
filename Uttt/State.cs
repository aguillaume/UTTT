namespace Uttt
{
    public class State
    {
        public Board Board { get; set; }
        public Player Player { get; internal set; }

        public Play Play { get; set; }
    }
}