using System.Collections.Generic;
using System.IO;
using System.Text;
using FANNCSharp;
using FANNCSharp.Double;

namespace AI_proj.NeuralNetwork
{
    public class DigitNeuralNetwork
    {
        private NeuralNet digitNet;

        //public static NeuralNet fuckDisShit()
        //{
        //    List<uint> layers = new List<uint>();
        //    //layers.Add(841);
        //    //layers.Add(1014);
        //    //layers.Add(1250);
        //    //layers.Add(100);
        //    //layers.Add(10);
        //    layers.Add(28*28);
        //    layers.Add(100);
        //    layers.Add(10);
        //    NeuralNet net = new NeuralNet(NetworkType.LAYER, layers.ToArray());
        //    net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC;
        //    net.ActivationFunctionOutput = ActivationFunction.SIGMOID_SYMMETRIC;
        //    net.LearningRate = 0.01f;
        //    net.LearningMomentum = 0.9f;
        //    TrainingData data = new TrainingData(@"C:\Users\Adam\Documents\Visual Studio 2015\Projects\AI_proj\charset\testfile");
        //    net.TrainOnData(data, 10, 0, 0.0f);
        //    return net;
        //}
        public DigitNeuralNetwork()
        {
            digitNet = new NeuralNet(NetworkType.LAYER, new List<uint> {28*28, 300, 10})
            {
                ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC,
                ActivationFunctionOutput = ActivationFunction.SIGMOID_SYMMETRIC
            };
            digitNet.LearningRate = 0.001f;
            digitNet.LearningMomentum = 0.9f;
        }

        public void CreateTestingFile(string inputImagesPath, string inputLabelsPath, string trainingFilePath, int numLoad)
        {
            var digits = DigitImage.LoadDigitsWithLabelsFromFile(inputImagesPath, inputLabelsPath, numLoad);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(numLoad + " 784 10");
            foreach (var digit in digits)
            {
                builder.AppendLine(digit.GetTrainingSample());
            }
            File.WriteAllText(trainingFilePath ,builder.ToString());
        }

        public NeuralNet GetNetwork()
        {
            return digitNet;
        }

        public void TrainNetwork(string trainingFilePath)
        {
            TrainingData data = new TrainingData(trainingFilePath);
            digitNet.TrainOnData(data, 10, 0, 0);
        }
    }
}