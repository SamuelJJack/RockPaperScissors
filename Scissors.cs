using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Scissors : Move
    {
        public Scissors()
        {
            MoveName = "Scissors";
            ImageSource = new BitmapImage(new Uri("/Images/Scissors.png", UriKind.Relative));
            WhichMoveCanWin = "Paper";
        }
    }
}
