using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Media;

namespace rocketRiotv2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        Player player;
        Random random = new Random();
        Zapper zappers;
        public MainWindow()
        {
            InitializeComponent();
            player = new Player(0, 300, 6, 0, playerCanvas);
            zappers = new Zapper(canvas, random);
            zappers.generate();
            SoundPlayer sp = new SoundPlayer("Rocket Man Soundtrack.wav");
            sp.Play();
            ImageBrush spritefill;//Image for the player
            BitmapImage bitmapImage;//Image file to use
            bitmapImage = new BitmapImage(new Uri("spriteFill2.png", UriKind.Relative));
            spritefill = new ImageBrush(bitmapImage);

            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            gameTimer.Start();
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            player.move();
            player.animate();
            if (player.pastScreen())
            {
                zappers.generate();
            }
            /*if (zappers.containsSpinner())
            {
                zappers.spin();
            }
            if (player.intersectWith(zappers.locations()))
            {
                MessageBox.Show("You lose");
            }
            if (player.collision(Mouse.GetPosition(canvas)))
            {
                MessageBox.Show("You lose");
            }*/
            /*Rectangle test = new Rectangle();
            test.Fill = Brushes.Red;
            //Canvas.SetTop(test, zappers.locations()[0].Y);
            //Canvas.SetLeft(test, zappers.locations()[0].X);
            Canvas.SetTop(test, 100);
            Canvas.SetLeft(test, 100);
            test.Height = 10;
            test.Width = 10;
            canvas.Children.Add(test);
            lblOutput.Text = Mouse.GetPosition(canvas) + "\n" + zappers.locations()[0] + "\n" + playerCanvas.InputHitTest(new Point(100, 100));
            if (player.collision(new Point (100, 100)))
            {
                MessageBox.Show("You lose");
            }*/
            if (player.intersectWith(zappers.locations()))
            {
                MessageBox.Show("You lose");
            }
        }
    }
}
