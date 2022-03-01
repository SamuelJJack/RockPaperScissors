using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RockPaperScissors.MoveFactory;

namespace RockPaperScissors
{
    class MoveFactory : IMoveFactory
    {
        public Move CreateMove(int moveType)
        {
            switch (moveType)
            {
                case 0:
                    return new Rock();
                case 1:
                    return new Paper();
                case 2:
                    return new Scissors();
                default:
                    return null;
            }
        }

        public interface IMoveFactory
        {
            public Move CreateMove(int move);  
        }
    }
}
