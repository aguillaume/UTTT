using System;

namespace Uttt
{
    public class Player
    {
        public Actor Actor { get; set; }
        public Player NextPlayer()
        {
            return new Player
            {
                Actor = (Actor == Actor.Me) ? Actor.Enemry : Actor.Me
            };
        }
    }
}