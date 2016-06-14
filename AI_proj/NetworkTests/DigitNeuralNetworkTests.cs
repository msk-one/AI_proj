using System;
using System.Collections.Generic;
using System.Linq;
using AI_proj.NeuralNetwork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FANNCSharp.Double;

namespace NetworkTests
{
    [TestClass]
    public class DigitNeuralNetworkTests
    {
        private IList<DigitImage> digits;
        [TestMethod]
        public void LoadDigits()
        {
            digits= 
                DigitImage.LoadDigitsWithLabelsFromFile(
                    @"C:\Users\Adam\Documents\Visual Studio 2015\Projects\AI_proj\charset\train-images.idx3-ubyte",
                    @"C:\Users\Adam\Documents\Visual Studio 2015\Projects\AI_proj\charset\train-labels.idx1-ubyte", 100);
            Console.WriteLine(digits[20]);
        }
        [TestMethod]
        public void RecognizeDigit()
        {
            var testDigits = DigitImage.LoadDigitsWithLabelsFromFile(
                    @"C:\Users\Piotr\Source\t10k-images.idx3-ubyte",
                    @"C:\Users\Piotr\Source\t10k-labels.idx1-ubyte", 30);
            var inputDigit1 = testDigits[0];
            var inputDigit2 = testDigits[1];
            var inputDigit3 = testDigits[2];
            var inputDigit4 = testDigits[3];
            var digitNetwork = new DigitNeuralNetwork();
            digitNetwork.CreateTestingFile(@"C:\Users\Piotr\Source\train-images.idx3-ubyte",
                    @"C:\Users\Piotr\Source\train-labels.idx1-ubyte",
                    @"C:\Users\Piotr\Source\testfile", 1200);

            var net = digitNetwork.GetNetwork();

            var inputNumberImage = inputDigit1.GetInputData();
            net.TrainOnFile(@"C:\Users\Piotr\Source\testfile", 1200, 0, 0.0001f);
            var n = net.Run(inputNumberImage);
            var n2 = net.Run(inputDigit2.GetInputData());
            var n3 = net.Run(inputDigit3.GetInputData());
            var n4 = net.Run(inputDigit4.GetInputData());
           
            Console.WriteLine("Actual: "+inputDigit1.label +" "+ inputDigit2.label +" "+ inputDigit3.label+" " + inputDigit4.label);
            /*for (int i = 0; i < n.Length; i++)
            {
                n[i] = Math.Abs(n[i]);
                n2[i] = Math.Abs(n2[i]);
                n3[i] = Math.Abs(n3[i]);
                n4[i] = Math.Abs(n4[i]);
            }*/
            Console.WriteLine("For 1st result:");
            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] > 0.9)
                {
                    Console.WriteLine(i + "; " + n[i] + "  <----");
                }
                else
                {
                    Console.WriteLine(i + "; " + n[i]);
                }
                
            }
            Console.WriteLine("For 2nd result:");
            for (int i = 0; i < n2.Length; i++)
            {
                if (n2[i] == n2.Min())
                {
                    Console.WriteLine(i + "; " + n2[i] + "  <----");
                }
                else
                {
                    Console.WriteLine(i + "; " + n2[i]);
                }

            }
            Console.WriteLine("For 3rd result:");
            for (int i = 0; i < n3.Length; i++)
            {
                if (n3[i] == n3.Min())
                {
                    Console.WriteLine(i + "; " + n3[i] + "  <----");
                }
                else
                {
                    Console.WriteLine(i + "; " + n3[i]);
                }

            }

            Console.WriteLine("For 4th result:");
            for (int i = 0; i < n4.Length; i++)
            {
                if (n4[i] == n4.Min())
                {
                    Console.WriteLine(i + "; " + n4[i] + "  <----");
                }
                else
                {
                    Console.WriteLine(i + "; " + n4[i]);
                }

            }
            //Console.WriteLine(res1);
            //Console.WriteLine(n2);
            //Console.WriteLine(n3);
            //Console.WriteLine(n4);
        }
    }
}
