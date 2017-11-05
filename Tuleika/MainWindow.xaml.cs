
using System;

namespace Tuleika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            sealLengthTextBox.IsEnabled = false;
            sealSizeTextBox.IsEnabled = false;
            foodCountTextBox.IsEnabled = false;
            foodSizeTextBox.IsEnabled = false;
            speedTextBox.IsEnabled = false;
            startButton.IsEnabled = false;

            fieldCanvas.Children.RemoveRange(0, fieldCanvas.Children.Count);
            var game = new Game(fieldCanvas, Convert.ToInt32(speedTextBox.Text), Convert.ToInt32(sealSizeTextBox.Text),
                Convert.ToInt32(sealLengthTextBox.Text), Convert.ToInt32(foodSizeTextBox.Text),
                Convert.ToInt32(foodCountTextBox.Text), scoreLabel);
            KeyDown += game.OnKeyPressed;
            game.Over += OnGameOver;
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            sealLengthTextBox.IsEnabled = true;
            sealSizeTextBox.IsEnabled = true;
            foodCountTextBox.IsEnabled = true;
            foodSizeTextBox.IsEnabled = true;
            speedTextBox.IsEnabled = true;
            startButton.IsEnabled = true;
        }
    }
}
