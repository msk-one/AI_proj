using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AI_proj.NeuralNetwork
{
    public class DigitImage
    {
        public byte[][] pixels;
        public byte label;
        
        public DigitImage(byte[][] pixels, byte label)
        {
            this.pixels = new byte[28][];
            for (int i = 0; i < this.pixels.Length; ++i)
                this.pixels[i] = new byte[28];

            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                    this.pixels[i][j] = pixels[i][j];
                //this.pixels[i][28] = 0;
            }

            this.label = label;
        }

        public int[] GetValuesHistogram()
        {
            int[] ret = new int[256];
            for (int i = 0; i < 28; i++)
                for (int j = 0; j < 28; j++)
                    ret[pixels[i][j]]++;
            return ret;
        }

        public List<int> GetSizeOfEmptyBorders()
        {
            var ret = new int[4];
            for (int i = 0; i < 28; i++)
            {
                bool empty = true;
                for (int j = 0; j < 28; j++)
                    if (pixels[i][j] != 0)
                        empty = false;
                if (!empty)
                {
                    ret[0] = i;
                    break;
                }
            }
            for (int i = 0; i < 28; i++)
            {
                bool empty = true;
                for (int j = 0; j < 28; j++)
                    if (pixels[j][i] != 0)
                        empty = false;
                if (!empty)
                {
                    ret[1] = i;
                    break;
                }
            }
            for (int i = 27; i >= 0; i--)
            {
                bool empty = true;
                for (int j = 0; j < 28; j++)
                    if (pixels[i][j] != 0)
                        empty = false;
                if (!empty)
                {
                    ret[2] = 27-i;
                    break;
                }
            }
            for (int i = 27; i >= 0; i--)
            {
                bool empty = true;
                for (int j = 0; j < 28; j++)
                    if (pixels[j][i] != 0)
                        empty = false;
                if (!empty)
                {
                    ret[3] = 27 - i;
                    break;
                }
            }
            return ret.ToList();
        }

        public double[] GetInputData()
        {
            double[] ret = new double[28*28];
            for (int i = 0; i < pixels.Length; i++)
            {
                for (int j = 0; j < pixels.Length; j++)
                {
                    //ret[i*28 + j] = Math.Round((double)pixels[i][j]/255-0.5, 2);
                    ret[i * 28 + j] = pixels[i][j];
                }
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagesPath"></param>
        /// <param name="labelsPath"></param>
        /// <param name="numLoad">number of images to load</param>
        /// <returns></returns>
        public static IList<DigitImage> LoadDigitsWithLabelsFromFile(string imagesPath, string labelsPath, int numLoad)
        {
            FileStream ifsLabels =new FileStream(labelsPath, FileMode.Open); 
            FileStream ifsImages = new FileStream(imagesPath, FileMode.Open);


            BinaryReader brLabels = new BinaryReader(ifsLabels);
            BinaryReader brImages = new BinaryReader(ifsImages);

            int magic1 = brImages.ReadInt32(); // discard
            int numImages = brImages.ReadInt32();
            int numRows = brImages.ReadInt32();
            int numCols = brImages.ReadInt32();

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();

            List<DigitImage> digits = new List<DigitImage>();
            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];

            // each  image
            for (int di = 0; di < numLoad; ++di)
            {
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();
                        pixels[i][j] = b;
                    }
                }

                byte lbl = brLabels.ReadByte();
                DigitImage dImage = new DigitImage(pixels, lbl);
                digits.Add(dImage);
            } // each image

            ifsImages.Close();
            brImages.Close();
            ifsLabels.Close();
            brLabels.Close();

            return digits;
        }

        public string GetTrainingSample()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            var builder = new StringBuilder();

            for (int i = 0; i < pixels.Length; i++)
            {
                for (int j = 0; j < pixels.Length; j++)
                {
                    //double ret = Math.Round((double) pixels[i][j]/255 - 0.5d, 2); //normalize to range -0.5,0.5
                    //builder.Append(ret.ToString("f2") + " ");
                    //builder.Append(((pixels[i][j]/128f) - 1f).ToString() + " ");
                    builder.Append(pixels[i][j] + " ");
                }
            }
            builder.Append("\n");
            for(int i=0; i<10; i++)
            {
                if (this.label == i)
                    builder.Append("1");
                else
                    builder.Append("-1");
                if (i != 9)
                    builder.Append(" ");
            }
            return builder.ToString();
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    if (this.pixels[i][j] == 0)
                        s += " "; // white
                    else if (this.pixels[i][j] == 255)
                        s += "O"; // black
                    else
                        s += "."; // gray
                }
                s += "\n";
            }
            s += this.label.ToString();
            return s;
        } // ToString
    }
}