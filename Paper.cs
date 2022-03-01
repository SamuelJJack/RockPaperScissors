using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Paper : Move
    {
        public Paper()
        {
            MoveName = "Paper";
            ImageSource = new BitmapImage(new Uri("/Images/Paper.png", UriKind.Relative));
            WhichMoveCanWin = "Rock";
        }
    }
}
