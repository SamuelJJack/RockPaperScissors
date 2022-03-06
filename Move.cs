using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{ 
    public abstract class Move    
    {
        public string MoveName { get; set; }
        public ImageSource ImageSource { get; set; }   
        public string WhichMoveCanWin { get; set; } 
       
            


       
    }



}
