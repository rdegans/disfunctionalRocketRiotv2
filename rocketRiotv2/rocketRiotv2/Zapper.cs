﻿using System;
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
                zappers[i].Width = 200;
                zappers[i].Height = 200;//change back
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
                if (true)//rand.Next(2) == 1)
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
                        myPointCollection.Add(new Point(0, i2 * (-30)));
                    }
                    hitPoints[i] = new Polyline();
                    hitPoints[i].Points = myPointCollection;
                    hitPoints[i].Stroke = Brushes.Red;
                    hitPoints[i].StrokeThickness = 2;
                    Canvas.SetTop(hitPoints[i], 519 - Canvas.GetBottom(zappers[i]));
                    Canvas.SetLeft(hitPoints[i], 101 + Canvas.GetLeft(zappers[i]));
                    canvas.Children.Add(zappers[i]);
                    canvas.Children.Add(hitPoints[i]);
                    if (rand.Next(11) == 12)//fix
                    {
                        spinner = i;
                    }
                    else
                    {
                        RotateTransform rotate = new RotateTransform(orientation);
                        zappers[i].RenderTransformOrigin = new Point(0.5, 0.5);
                        zappers[i].RenderTransform = rotate;
                        RotateTransform rotate2 = new RotateTransform(orientation);
                        rotate2.CenterX = 0;
                        rotate2.CenterY = -60;
                        hitPoints[i].RenderTransform = rotate2;
                    }
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
            zappers[spinner].RenderTransformOrigin = new Point(0.5, 0.5);
            zappers[spinner].RenderTransform = rotate;
            RotateTransform rotate2 = new RotateTransform(degreeCounter);
            rotate2.CenterY = -60;
            rotate2.CenterX = 0;
            hitPoints[spinner].RenderTransform = rotate2;
        }
        public PointCollection locations()
        {
            PointCollection returnPoints = new PointCollection();
            for (int i = 0; i < hitPoints.Length; i++)
            {
                for (int i2 = 0; i2 < hitPoints[i].Points.Count; i2++)
                {
                    //MessageBox.Show(hitPoints[i].Points[i2] + "");
                    //hitPoints[i].TranslatePoint(hitPoints[i].Points[i2], canvas);
                    //MessageBox.Show(canvas.TransformToAncestor(Window.GetWindow(canvas)).Transform(hitPoints[i].Points[i2]) + "");
                    //MessageBox.Show(canvas.PointFromScreen(hitPoints[i].Points[i2]) + "");
                    //convert to point relative to screen with polyline as ui element, convert to point relative to canvas
                    Point screenPoint = hitPoints[i].PointToScreen(hitPoints[i].Points[i2]);
                    Point canvasPoint = canvas.PointFromScreen(screenPoint);
                    returnPoints.Add(canvasPoint);
                    //canvas.Children.Remove//testing
                }
            }
            //MessageBox.Show(returnPoints.ToString());
            Polyline test = new Polyline();
            test.Points = returnPoints;
            test.Stroke = Brushes.Red;
            test.StrokeThickness = 2;
            Canvas.SetTop(test, 0);
            Canvas.SetLeft(test, 0);
            try
            {
                canvas.Children.Remove(test);
            }
            catch
            {

            }
            //canvas.Children.Add(test);
            return returnPoints;
        }
    }
}
