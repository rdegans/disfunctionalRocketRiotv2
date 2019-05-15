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
            player = new Player(0, 300, 6, 0, canvas);
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
            if (zappers.containsSpinner())
            {
                zappers.spin();
            }
        }
    }
}
