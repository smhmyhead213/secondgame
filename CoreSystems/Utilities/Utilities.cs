using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace secondgame.Utilities
{
    public static class Utilities
    {
        public static Vector2 ScreenCentre() => new Vector2(Width / 2, Height / 2);
        public static int[] InsertionSort(int[] inputArray)
        {
            for (int i = 0; i < inputArray.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    // Swap if the element at j - 1 position is greater than the element at j position
                    if (inputArray[j - 1] > inputArray[j])
                    {
                        int temp = inputArray[j - 1];
                        inputArray[j - 1] = inputArray[j];
                        inputArray[j] = temp;
                    }
                }
            }
            return inputArray; // Return the sorted array
        }

        /// <summary>
        /// Returns an appropriate offset to be used for SpriteBatch.Draw()'s origin parameter.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Vector2 CalculateOffset(float width, float height)
        {
            return new Vector2(width / 2f, height / 2f);
        }

        public static bool Contains(this RectangleF rectangle, Vector2 point)
        {
            Point newPoint = new Point((int)point.X, (int)point.Y);
            return rectangle.Contains(newPoint);
        }
        public static Microsoft.Xna.Framework.Rectangle ToRectangleXNA(this RectangleF rectangle)
        {
            return new Microsoft.Xna.Framework.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }
    }
}
