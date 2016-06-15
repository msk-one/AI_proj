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
                    @"C:\Users\Adam\Documents\Visual Studio 2015\Projects\AI_proj\charset\t10k-images.idx3-ubyte",
                    @"C:\Users\Adam\Documents\Visual Studio 2015\Projects\AI_proj\charset\t10k-labels.idx1-ubyte", 100);
            Console.WriteLine(digits[20]);
        }

        [TestMethod]
        public void GetTestDataStats()
        {
            var testDigits = DigitImage.LoadDigitsWithLabelsFromFile(
                   @"C:\Users\Piotr\Source\train-images.idx3-ubyte",
                    @"C:\Users\Piotr\Source\train-labels.idx1-ubyte", 60000);
            testDigits = testDigits.Where(x => x.label == 9).ToList();
            var counts = new int[4];
            var squareCounts = new int[4];
            foreach(var item in testDigits)
            {
                var res = item.GetSizeOfEmptyBorders();
                for (int i = 0; i < 4; i++)
                {
                    counts[i] += res[i];
                    squareCounts[i] += res[i] * res[i];
                }
            }
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(counts[i] / (double)testDigits.Count);
                Console.WriteLine(Math.Sqrt(squareCounts[i]/ (double)testDigits.Count - (counts[i] / (double)testDigits.Count) *(counts[i] / (double)testDigits.Count)));
            }
        }

        [TestMethod]
        public void GetHistogram()
        {
            var testDigits = DigitImage.LoadDigitsWithLabelsFromFile(
                   @"C:\Users\Piotr\Source\train-images.idx3-ubyte",
                    @"C:\Users\Piotr\Source\train-labels.idx1-ubyte", 60000);
            var counts = new int[256];
            foreach (var item in testDigits)
            {
                var res = item.GetValuesHistogram();
                for (int i = 0; i < 256; i++)
                {
                    counts[i] += res[i];
                }
            }
            for(int i=0; i< 256; i++)
            {
                Console.WriteLine(i.ToString() + " : " + counts[i]);
            }
            int sum = 0;
            for(int i=1;i<256;i++)
            {
                sum += counts[i];
            }
            Console.WriteLine(counts[0] / sum);
        }

        [TestMethod]
        public void RecognizeDigit()
        {
            var testDigits = DigitImage.LoadDigitsWithLabelsFromFile(
                    @"C:\Users\Piotr\Source\t10k-images.idx3-ubyte",
                    @"C:\Users\Piotr\Source\t10k-labels.idx1-ubyte", 1000);
            var inputDigit1 = testDigits[0];
            var inputDigit2 = testDigits[1];
            var inputDigit3 = testDigits[2];
            var inputDigit4 = testDigits[3];
            var digitNetwork = new DigitNeuralNetwork();
            digitNetwork.CreateTestingFile(@"C:\Users\Piotr\Source\train-images.idx3-ubyte",
                    @"C:\Users\Piotr\Source\train-labels.idx1-ubyte",
                    @"C:\Users\Piotr\Source\testfile", 2000);

            var net = digitNetwork.GetNetwork();

            var inputNumberImage = inputDigit1.GetInputData();
            net.TrainOnFile(@"C:\Users\Piotr\Source\testfile", 2000, 0, 0.0001f);
            var n = net.Run(inputNumberImage);
            var n2 = net.Run(inputDigit2.GetInputData());
            var n3 = net.Run(inputDigit3.GetInputData());
            var n4 = net.Run(inputDigit4.GetInputData());
           
            net.Save(@"C:\Users\Adam\Source\Repos\AI_proj\AI_proj\mnist\digit_neuralnet");
            Console.WriteLine("Actual: "+inputDigit1.label +" "+ inputDigit2.label +" "+ inputDigit3.label+" " + inputDigit4.label);

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
        [TestMethod]
        public void TestTrainedNet()
        {
            var testDigits = DigitImage.LoadDigitsWithLabelsFromFile(
               @"C:\Users\Adam\Source\Repos\AI_proj\AI_proj\mnist\t10k-images.idx3-ubyte",
                @"C:\Users\Adam\Source\Repos\AI_proj\AI_proj\mnist\t10k-labels.idx1-ubyte", 30);
            var inputDigit1 = testDigits[0];
            var inputDigit2 = testDigits[1];
            var inputDigit3 = testDigits[2];
            var inputDigit4 = testDigits[3];
            Console.WriteLine("Actual: " + inputDigit1.label + " " + inputDigit2.label + " " + inputDigit3.label + " " + inputDigit4.label);
            var net = new NeuralNet(@"C:\Users\Adam\Source\Repos\AI_proj\AI_proj\mnist\digit_neuralnet");

            var n = net.Run(inputDigit1.GetInputData());
            var n2 = net.Run(inputDigit2.GetInputData());
            var n3 = net.Run(inputDigit3.GetInputData());
            var n4 = net.Run(inputDigit4.GetInputData());

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

        }
    }
}
