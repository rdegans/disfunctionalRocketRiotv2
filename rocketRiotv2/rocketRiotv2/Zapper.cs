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

namespace rocketRiotv2
{
    public class Zapper
    {
        Random rand;
        Canvas canvas;
        ImageBrush spritefill;//Image for the player
        BitmapImage bitmapImage;//Image file to use
        Rectangle[] zappers = new Rectangle[4];
        Polyline[] hitPoints = new Polyline[4];
        int spinner;
        int degreeCounter = 0;
        int orientation;
        public Zapper(Canvas c, Random r)
        {
            canvas = c;
            rand = r;
            for (int i = 0; i < 4; i++)
            {
                zappers[i] = new Rectangle();
                zappers[i].Width = 300;
                zappers[i].Height = 300;
            }
        }
        public void generate()
        {
            spinner = -1;
            degreeCounter = 0;
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    canvas.Children.Remove(zappers[i]);
                    canvas.Children.Remove(hitPoints[i]);
                }
                catch
                {

                }
                if (rand.Next(2) == 1)
                {
                    bitmapImage = new BitmapImage(new Uri("zapper.png", UriKind.Relative));
                    spritefill = new ImageBrush(bitmapImage);
                    zappers[i].Fill = spritefill;
                    orientation = rand.Next(361);
                    if (i == 0)
                    {
                        Canvas.SetBottom(zappers[i], 300);
                        Canvas.SetLeft(zappers[i], 100);
                    }
                    else if (i == 1)
                    {
                        Canvas.SetBottom(zappers[i], 300);
                        Canvas.SetLeft(zappers[i], 500);
                    }
                    else if (i == 2)
                    {
                        Canvas.SetBottom(zappers[i], 0);
                        Canvas.SetLeft(zappers[i], 100);
                    }
                    else
                    {
                        Canvas.SetBottom(zappers[i], 0);
                        Canvas.SetLeft(zappers[i], 500);
                    }
                    PointCollection myPointCollection = new PointCollection();
                    for (int i2 = 0; i2 < 5; i2++)
                    {
                        myPointCollection.Add(new Point(Canvas.GetLeft(zappers[i]), Canvas.GetBottom(zappers[i]) + i2 * 5));
                    }
                    hitPoints[i] = new Polyline();
                    hitPoints[i].Points = myPointCollection;
                    hitPoints[i].Stroke = Brushes.Red;
                    hitPoints[i].StrokeThickness = 2;
                    canvas.Children.Add(hitPoints[i]);
                    if (rand.Next(11) == 1)
                    {
                        spinner = i;
                    }
                    else
                    {
                        RotateTransform rotate = new RotateTransform(orientation);
                        zappers[i].RenderTransformOrigin = new Point(0.5, 0.5);
                        zappers[i].RenderTransform = rotate;
                        RotateTransform rotate2 = new RotateTransform(orientation);
                        hitPoints[i].RenderTransformOrigin = new Point(0.5, 0.5);
                        hitPoints[i].RenderTransform = rotate2;
                    }
                    canvas.Children.Add(zappers[i]);
                }
            }
        }
        public bool containsSpinner()
        {
            if (spinner > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void spin()
        {
            degreeCounter += 9;
            RotateTransform rotate = new RotateTransform(degreeCounter);
            //zappers[spinner].RenderTransformOrigin = new Point(0.5, 0.5);
            zappers[spinner].RenderTransform = rotate;
            RotateTransform rotate2 = new RotateTransform(degreeCounter);
            //hitPoints[spinner].RenderTransformOrigin = new Point(0.5, 0.5);
            hitPoints[spinner].RenderTransform = rotate2;
        }
    }
}
