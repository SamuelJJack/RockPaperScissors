

namespace RockPaperScissors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void QuitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            var moveSelectionWindow = new MoveSelection();
            moveSelectionWindow.Show();
            this.Close();
        }

       
    }
}
