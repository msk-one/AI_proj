using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Diagnostics;
using System.EnterpriseServices.CompensatingResourceManager;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using FANNCSharp;
using FANNCSharp.Double;

namespace AI_proj.NeuralNetwork
{
    public class XOR
    {
        public static NeuralNet get_xor_net()
        {
           
            //NeuralNet net = new NeuralNet(3, 2, 3, 1); // layers, input, hidden, output
            NeuralNet net = new NeuralNet(NetworkType.LAYER,new List<uint>() {2,10,1});
         //   NeuralNet n = new NeuralNet();
            
            //Activation function layers
            /*net.SetActivationFunctionLayer(ActivationFunction.SIGMOID_SYMMETRIC, 2);
            net.SetActivationFunctionLayer(ActivationFunction.SIGMOID_SYMMETRIC, (int)net.LayerCount-1);*/

            net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC;
            net.ActivationFunctionOutput = ActivationFunction.SIGMOID_SYMMETRIC;

            create_training_file();

            TrainingData data = new TrainingData(@"C:\Users\Adam\Documents\AIProj\xor.data");

            net.TrainOnData(data, 500000, 1000, 0.0f);
            
            //net.Test(new double[] { 0, 0 }, new double[] { 0 });
            //net.Test(new double[] { 0, 1 }, new double[] { 1 });
            //net.Test(new double[] { 1, 0 }, new double[] { 1 });
            //net.Test(new double[] { 1, 1 }, new double[] { 0 });

            //net.Save(@"C:\Users\Adam\Documents\AIProj\xor_learned.net");

            return net;
        }

        //private static void create_training_file()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("8 2 1");

        //    sb.AppendLine("0 1");
        //    sb.AppendLine("1");
        //    sb.AppendLine("0 0");
        //    sb.AppendLine("0");
        //    sb.AppendLine("0 -1");
        //    sb.AppendLine("0");

        //    sb.AppendLine("1 1");
        //    sb.AppendLine("1");
        //    sb.AppendLine("1 -1");
        //    sb.AppendLine("1");
        //    sb.AppendLine("1 0");
        //    sb.AppendLine("1");

        //    sb.AppendLine("-1 -1");
        //    sb.AppendLine("-1");
        //    sb.AppendLine("-1 0");
        //    sb.AppendLine("0");
        //    //sb.AppendLine("-1 1");
        //    //sb.AppendLine("1");
        //    File.WriteAllText(@"C:\Users\Adam\Documents\AIProj\xor.data", sb.ToString());
        //}
        private static void create_training_file()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("10 2 1");

            sb.AppendLine("0 1");
            sb.AppendLine("-1");

            sb.AppendLine("0 0");
            sb.AppendLine("0");

            sb.AppendLine("0 -1");
            sb.AppendLine("1");

            sb.AppendLine("5 1");
            sb.AppendLine("1");

            sb.AppendLine("100 20");
            sb.AppendLine("1");

            sb.AppendLine("39 -21");
            sb.AppendLine("1");

            sb.AppendLine("-23 -21");
            sb.AppendLine("-1");
            sb.AppendLine("-10 21");
            sb.AppendLine("-1");

            sb.AppendLine("-10 -12");
            sb.AppendLine("1");
            sb.AppendLine("-20 30");
            sb.AppendLine("-1");
            //sb.AppendLine("-1 1");
            //sb.AppendLine("1");
            //File.WriteAllText(@"..\..\xor.data", sb.ToString());
        }
        public static void Test(NeuralNet net, double a, double b)
        {
            int res = a > b ? 1 : -1;
            if (Math.Abs(a - b) < Double.Epsilon)
            {
                res = 0;
            }
            double r = net.Run(new double[] { a, b })[0];
            r = Math.Round(r, 8);
            //Console.WriteLine("{0} xor {1} = {2}", a, b, r);

            Console.WriteLine(a +" > "+b+"; "+r + "  ;" + (Math.Abs(res - r) < 0.1));
        }
    }
}