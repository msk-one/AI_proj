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
    }
}