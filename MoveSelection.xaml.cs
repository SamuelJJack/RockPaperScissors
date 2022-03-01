using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RockPaperScissors
{
    /// <summary>
    /// Interaction logic for MoveSelection.xaml
    /// </summary>
    public partial class MoveSelection : Window
    {
      
        public MoveSelection()
        {
            InitializeComponent();
        }

        private void OnMouseEnterRockImage(object sender, EventArgs e)
        {
            RockImage.Source = new BitmapImage(new Uri("/Images/Rock Border.png", UriKind.Relative));
        }

        private void OnMouseLeaveRockImage(object sender, EventArgs e)
        {
            RockImage.Source = new BitmapImage(new Uri("/Images/Rock.png", UriKind.Relative));           
        }

        private void OnMouseEnterPaperImage(object sender, EventArgs e)
        {
            PaperImage.Source = new BitmapImage(new Uri("/Images/Paper Border.png", UriKind.Relative));
        }

        private void OnMouseLeavePaperImage(object sender, EventArgs e)
        {
            PaperImage.Source = new BitmapImage(new Uri("/Images/Paper.png", UriKind.Relative));
        }

        private void OnMouseEnterScissorsImage(object sender, EventArgs e)
        {
            ScissorsImage.Source = new BitmapImage(new Uri("/Images/Scissors Border.png", UriKind.Relative));
        }

        private void OnMouseLeaveScissorsImage(object sender, EventArgs e)
        {
            ScissorsImage.Source = new BitmapImage(new Uri("/Images/Scissors.png", UriKind.Relative));
        }

        private void RockImageClick(object sender, MouseEventArgs e)
        {
            StartPlayGame(0);
        }

        private void PaperImageClick(object sender, MouseButtonEventArgs e)
        {
            StartPlayGame(1);
        }

        private void ScissorsImageClick(object sender, MouseButtonEventArgs e)
        {
            StartPlayGame(2);
        }

        private void StartPlayGame(int playersMoveIndex)
        {
            //Create move factory
            var moveFactory = new MoveFactory();

            //Choose a move for the computer at random   
            var random = new Random();
            var computersMove = moveFactory.CreateMove(random.Next(0, 2));

            //Choose rock as the players move
            var playersMove = moveFactory.CreateMove(playersMoveIndex);

            //Create new play game window and pass in both the players and computers' move
            var playGameWindow = new PlayGame(playersMove, computersMove);
            playGameWindow.Show();
            this.Close();
        }

    }
}
