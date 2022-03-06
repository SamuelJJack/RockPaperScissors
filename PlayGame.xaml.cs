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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RockPaperScissors
{
    /// <summary>
    /// Interaction logic for PlayGame.xaml
    /// </summary>
    public partial class PlayGame : Window
    {
        private readonly Move _playersMove, _computersMove;
        private Storyboard? _storyboard;
        private int _animationIterations = 0;

        public PlayGame(Move playersMove, Move computersMove)
        {
            InitializeComponent();
            _playersMove = playersMove;
            _computersMove = computersMove;
            Trace.WriteLine("Players move: " + _playersMove.MoveName);
            Trace.WriteLine("Computers move: " + _computersMove.MoveName);
            AnimateFists();
        }

        private void AnimateFists()
        {
            // Create a NameScope for the page so that we can use Storyboards
            NameScope.SetNameScope(this, new NameScope());

           // Create a transform. This transform will be used to move the image
            var animatedTranslateTransform = new TranslateTransform();
            this.RegisterName("AnimatedTranslateTransform", animatedTranslateTransform);
            LeftFist.RenderTransform = animatedTranslateTransform;
            RightFist.RenderTransform = animatedTranslateTransform;

            // Create the animation path.
            var animationPath = new PathGeometry();
            var pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(0, 0);


            var quadraticBezierSegment = new QuadraticBezierSegment();
            quadraticBezierSegment.Point1 = (new Point(0, 60));
            quadraticBezierSegment.Point2 = (new Point(0, 0));

            pathFigure.Segments.Add(quadraticBezierSegment);
            animationPath.Figures.Add(pathFigure);

            // Freeze the PathGeometry for performance benefits.
            animationPath.Freeze();

            //Create a DoubleAnimationUsingPath to move the
            // rectangle horizontally along the path by animating
            // its TranslateTransform.
            var translateYAnimation = new DoubleAnimationUsingPath()
            {
                PathGeometry = animationPath,
                Duration = TimeSpan.FromMilliseconds(800),

                // Set the Source property to Y. This makes the animation generate vertical offset values from the path information.
                Source = PathAnimationSource.Y
            };

            // Set the animation to target the X property
            // of the TranslateTransform named "AnimatedTranslateTransform".
            Storyboard.SetTargetName(translateYAnimation, "AnimatedTranslateTransform");
            Storyboard.SetTargetProperty(translateYAnimation, new PropertyPath(TranslateTransform.YProperty));

            // Create a Storyboard to contain and apply the animations.
            _storyboard = new Storyboard();
            _storyboard.Children.Add(translateYAnimation);

            _storyboard.Completed += UpdateTitle;
            

            // Start the storyboard.
            _storyboard.Begin(this);
        }

        private void UpdateTitle(object? sender, EventArgs e)
        {
            if (_animationIterations == 0)
            {
                RockPaperScissors.Content = "Rock, ";
                _storyboard.Begin(this);
            }
            else if (_animationIterations == 1)
            {
                RockPaperScissors.Content += "Paper, ";
                _storyboard.Begin(this);
            }
            else if (_animationIterations == 2)
            {
                RockPaperScissors.Content += "Scissors!";
                Trace.WriteLine("Finished shaking fists baby");

                var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(750) };
                timer.Start();
                timer.Tick += (sender, args) =>
                {
                    timer.Stop();
                    LeftFist.Visibility = Visibility.Hidden;
                    RightFist.Visibility = Visibility.Hidden;
                    PlayerLabel.Visibility = Visibility.Visible;
                    CPULabel.Visibility = Visibility.Visible;
                    RevealMoves();
                };
            }

            _animationIterations++;
        }

        private void RevealMoves()
        {
            LeftFist.Source = _playersMove.ImageSource;
            RightFist.Source = _computersMove.ImageSource;
            LeftFist.Visibility = Visibility.Visible;
            RightFist.Visibility = Visibility.Visible;
            FadeInMoves();
            
        }

        private void DetermineWinner()
        {
            if (_playersMove.MoveName == _computersMove.MoveName)
            {
                Trace.WriteLine("Draw");
                RockPaperScissors.Content = "Draw!";
                return;
            }
            if (_playersMove.WhichMoveCanWin == _computersMove.MoveName)
            {
                Trace.WriteLine("You won");
                RockPaperScissors.Content = "You win!";
                return;
            }
            else 
            {
                Trace.WriteLine("You lost");
                RockPaperScissors.Content = "Shiii you lose";
                return;
            }
        }

        private void FadeInMoves()
        {
            var fadeInPlayerMoveAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromMilliseconds(900)),
                RepeatBehavior = new RepeatBehavior(1)
            };

            var fadeInCPUMoveAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromMilliseconds(900)),
                RepeatBehavior = new RepeatBehavior(1)
            };

            _storyboard = new Storyboard();
            _storyboard.Children.Add(fadeInPlayerMoveAnimation);
            _storyboard.Children.Add(fadeInCPUMoveAnimation);

            Storyboard.SetTarget(fadeInPlayerMoveAnimation, LeftFist);
            Storyboard.SetTarget(fadeInCPUMoveAnimation, RightFist);

            Storyboard.SetTargetProperty(fadeInPlayerMoveAnimation, new PropertyPath(Image.OpacityProperty));

            Storyboard.SetTargetProperty(fadeInCPUMoveAnimation, new PropertyPath(Image.OpacityProperty));


            _storyboard.Completed += MoveRevealCompleted;
            _storyboard.Begin(this);
        }

        private void MoveRevealCompleted(object? sender, EventArgs e)
        {
            AnimateNewGameButton();
            DetermineWinner();
        }

        private void NewGameButtonClick(object sender, RoutedEventArgs e)
        { 
            var moveSelectionWindow = new MoveSelection();
            moveSelectionWindow.Show();
            this.Close();
        }

        private void AnimateNewGameButton()
        {
            NewGameButton.Visibility = Visibility.Visible;

            var opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromMilliseconds(2000)),
                RepeatBehavior = new RepeatBehavior(1)
            };

            var widthAnimation = new DoubleAnimation
            {
                From = 0,
                To = 135,
                Duration = new Duration(TimeSpan.FromMilliseconds(2000)),
                RepeatBehavior = new RepeatBehavior(1)
            };

            var heightAnimation = new DoubleAnimation
            {
                From = 0,
                To = 40,
                Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                RepeatBehavior = new RepeatBehavior(1)
            };




            _storyboard = new Storyboard();
            _storyboard.Children.Add(opacityAnimation);
            _storyboard.Children.Add(widthAnimation);
            _storyboard.Children.Add(heightAnimation);


            Storyboard.SetTarget(opacityAnimation, NewGameButton);
            Storyboard.SetTarget(widthAnimation, NewGameButton);
            Storyboard.SetTarget(heightAnimation, NewGameButton);

            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(Button.OpacityProperty));
            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Button.WidthProperty));
            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(Button.HeightProperty));

            _storyboard.Completed += SetNewGameText;
            _storyboard.Begin(this);
        }

        private void SetNewGameText(object? sender, EventArgs e)
        {
            NewGameButton.Content = "New Game";
        }
    }
}
