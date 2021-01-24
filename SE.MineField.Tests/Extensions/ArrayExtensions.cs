using System.Collections.Generic;
using SE.MineField.Enums;

namespace SE.MineField.Tests
{
    public static class SquareTypeArrayExtensions
    {
        public static IList<SquareType> ToList(this SquareType[,] array)
        {
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            List<SquareType> ret = new List<SquareType>(width * height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    ret.Add(array[i, j]);
                }
            }
            return ret;
        }

    }
}