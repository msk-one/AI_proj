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
           
            NeuralNet net = new NeuralNet(3, 2, 3, 1); // layers, input, hidden, output
            //Activation function layers
            /*net.SetActivationFunctionLayer(ActivationFunction.SIGMOID_SYMMETRIC, 2);
            net.SetActivationFunctionLayer(ActivationFunction.SIGMOID_SYMMETRIC, (int)net.LayerCount-1);*/

            net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC;
            net.ActivationFunctionOutput = ActivationFunction.SIGMOID_SYMMETRIC;

            create_training_file();

            TrainingData data = new TrainingData("C:/Users/Jedrek/Documents/Studies/AI/AI_proj/AI_proj/AI_proj/NeuralNetwork/xor.data");

            net.TrainOnData(data, 500000, 1000, (float)0.001);
            
            //net.Test(new double[] { 0, 0 }, new double[] { 0 });
            //net.Test(new double[] { 0, 1 }, new double[] { 1 });
            //net.Test(new double[] { 1, 0 }, new double[] { 1 });
            //net.Test(new double[] { 1, 1 }, new double[] { 0 });

            net.Save("C:/Users/Jedrek/Documents/Studies/AI/AI_proj/AI_proj/AI_proj/NeuralNetwork/xor_learned.net");

            return net;
        }

        private static void create_training_file()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("4 2 1");
            sb.AppendLine("0 0");
            sb.AppendLine("1");
            sb.AppendLine("0 1");
            sb.AppendLine("0");
            sb.AppendLine("1 0");
            sb.AppendLine("0");
            sb.AppendLine("1 1");
            sb.AppendLine("0");
            File.WriteAllText("C:/Users/Jedrek/Documents/Studies/AI/AI_proj/AI_proj/AI_proj/NeuralNetwork/xor.data", sb.ToString());
        }

        public static void Test(NeuralNet net, double a, double b)
        {
            double r = net.Run(new double[] { a, b })[0];
            r = Math.Round(r, 1);
            Debug.WriteLine("{0} xor {1} = {2}", a, b, r);
        }
    }
}