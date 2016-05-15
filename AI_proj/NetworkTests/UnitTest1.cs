using System;
using AI_proj.NeuralNetwork;
using FANNCSharp.Double;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetworkTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("XOR");
            //NeuralNet xornet = XOR.get_xor_net();
            //XOR.Test(xornet, -1, -1);
            //XOR.Test(xornet, -1, 1);
            //XOR.Test(xornet, -1, 0);
            //Console.WriteLine();
            //XOR.Test(xornet, 1, -1);
            //XOR.Test(xornet, 1, 1);
            //XOR.Test(xornet, 1, 0);
            //Console.WriteLine();
            //XOR.Test(xornet, 0, -1);
            //XOR.Test(xornet, 0, 1);
            //XOR.Test(xornet, 0, 0);

            NeuralNet xornet = XOR.get_xor_net();
            XOR.Test(xornet, -1, -1);
            XOR.Test(xornet, -1, 1);
            XOR.Test(xornet, -1, 0);
            Console.WriteLine();
            XOR.Test(xornet, 1, -1);
            XOR.Test(xornet, 1, 1);
            XOR.Test(xornet, 1, 0);
            Console.WriteLine();
            XOR.Test(xornet, 0, -1);
            XOR.Test(xornet, 0, 1);
            XOR.Test(xornet, 0, 0);

            Console.WriteLine();
            XOR.Test(xornet, 100, 2);
            XOR.Test(xornet, -12, 3);
            XOR.Test(xornet, 22, 1010101);

            Console.WriteLine();
            XOR.Test(xornet, 12.5, 12);
            XOR.Test(xornet, 10, -10);
            XOR.Test(xornet, -19.5, -19);
            XOR.Test(xornet, 1234.0001, 1234);
            XOR.Test(xornet, 5, 5+2*Double.Epsilon);

            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                double x = rand.NextDouble(), y = rand.NextDouble();
                int xOfsset = rand.Next(-1000, 1000), yOffset = rand.Next(-1000, 1000);
                XOR.Test(xornet, x + xOfsset, y + yOffset);
            }
        }
    }
}
