using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Rock : Move
    {
        public Rock()
        {
            MoveName = "Rock";
            ImageSource = new BitmapImage(new Uri("/Images/Rock.png", UriKind.Relative));
            WhichMoveCanWin = "Scissors";
        }
    }
}
