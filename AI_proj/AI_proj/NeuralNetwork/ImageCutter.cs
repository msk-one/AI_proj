using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AI_proj.NeuralNetwork
{
    public class ImageCutter
    {
        public static byte[][] CutImage(byte[][] pixels)
        {
           //remove empty rows
           List<byte[]> allNotEmptyRows = new List<byte[]>();
            int x = 0;
            for (int i = 0; i < pixels.Length; i++)
            {
                byte[] row = new byte[pixels[0].Length];
                bool isEmpty = true;
                for (int j = 0; j < pixels[0].Length; j++)
                {
                    if (pixels[i][j] != 0)
                    {
                        isEmpty = false;
                    }
                    row[j] = pixels[i][j];
                }
                if (!isEmpty)
                {
                    allNotEmptyRows.Add(row);
                }
            }

            //remove empty columns
            int firstNotEmptyIdx = -1;
            int lastNotEmptyIdx = -1;
            for (int i = 0; i < allNotEmptyRows[0].Length; i++)
            {
                bool isEmpty = firstNotEmptyIdx != -1;
                for (int j = 0; j < allNotEmptyRows.Count; j++)
                {
                    if (allNotEmptyRows[j][i] != 0 && firstNotEmptyIdx == -1)
                    {
                        firstNotEmptyIdx = i;
                    }
                    else if(allNotEmptyRows[j][i] != 0 && lastNotEmptyIdx == -1 && firstNotEmptyIdx!= -1)
                    {
                        isEmpty = false;
                    }
                }
                if (isEmpty)
                {
                    lastNotEmptyIdx = i;
                    break;
                }
            }
            byte[][] ret = new byte[allNotEmptyRows.Count][];
            for (int i = 0; i < allNotEmptyRows.Count; i++)
            {
                ret[i] = new byte[lastNotEmptyIdx-firstNotEmptyIdx];
                for (int j = firstNotEmptyIdx; j < lastNotEmptyIdx; j++)
                {
                    ret[i][j- firstNotEmptyIdx] = allNotEmptyRows[i][j];
                }
            }
            return ret;
        }

        public static byte[][] MakeSquare(byte[][] pixels)
        {
            if (pixels.Length == pixels[0].Length)
            {
                return pixels;
            }

            List<List<byte>> list = new List<List<byte>>();
            for (int i = 0; i < pixels.Length; i++)
            {
                list.Add(new List<byte>());
                for (int j = 0; j < pixels[0].Length; j++)
                {
                    list[i].Add(pixels[i][j]);   
                }
            }
            int diff;
            if (pixels.Length > pixels[0].Length) //add columns
            {
                //list = new List<List<byte>>();
                diff = pixels.Length - pixels[0].Length;

                for (int i = 0; i < diff/2; i++)
                {
                    foreach (var row in list)
                    {
                        row.Insert(0,0); //add empty at begining
                        row.Add(0); //add empty at end
                    }
                }
                if (diff%2 == 1)
                {
                    foreach (var row in list)
                    {
                        row.Add(0);
                    }
                }
            }
            else
            {
                //add rows
                diff = pixels[0].Length - pixels.Length;
                for (int i = 0; i < diff / 2; i++)
                {
                    list.Add(new List<byte>());
                    list.Insert(0, new List<byte>());
                    for (int j = 0; j < pixels[0].Length; j++)
                    {
                        list[0].Add(0);
                        list[list.Count-1].Add(0);
                    }
                }
                if (diff % 2 == 1)
                {
                    list.Add(new List<byte>(pixels[0].Length));
                    list.Insert(0, new List<byte>(pixels[0].Length));
                }
            }

            byte[][] ret = new byte[list.Count][];
            for (int i = 0; i < list.Count; i++)
            {
                ret[i] = new byte[list[0].Count];
                for (int j = 0; j < list[0].Count; j++)
                {
                    ret[i][j] = list[i][j];
                }
            }
            return ret;
        }
    }
}