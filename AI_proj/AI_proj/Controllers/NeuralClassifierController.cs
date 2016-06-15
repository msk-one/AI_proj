using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AI_proj.Models;
using AI_proj.NeuralNetwork;
using FANNCSharp.Double;

namespace AI_proj.Controllers
{
    public class NeuralClassifierController : Controller
    {
        // GET: NeuralClassifier
        public ActionResult Index()
        {
            return View();
        }
        // POST: NeuralClassifier
        //[HttpPost]
        //public ActionResult Index(UploadImageModel model)
        //{
        //        Bitmap original = null;
        //        var name = "newimagefile";
        //        var errorField = string.Empty;

        //        errorField = "File";
        //        name = Path.GetFileNameWithoutExtension(model.File.FileName);
        //        original = Bitmap.FromStream(model.File.InputStream) as Bitmap;


        //        if (original != null)
        //        {
        //            var fn = Server.MapPath("~/Content/img/" + name + ".png");
        //            var img = CreateImage(original, model.X, model.Y, model.Width, model.Height);
        //            img.Save(fn, System.Drawing.Imaging.ImageFormat.Png);
        //            return RedirectToAction("Index");
        //        }
        //        else
        //            ModelState.AddModelError(errorField, "Your upload did not seem valid. Please try again using only correct images!");
        
        //    return View(model);
        //}
        [HttpPost]
        public ActionResult Index(string imageData)
        {
            if(imageData == null)
                return Json(null);

            byte[] data = Convert.FromBase64String(imageData);

            Image image;
            using (MemoryStream ms = new MemoryStream(data))
            {
                image = Image.FromStream(ms);
            }

            Bitmap bitmap = new Bitmap(image);
            
            byte[][] pixels = new byte[image.Width][];
            for (int i = 0; i < image.Width; i++)
            {
                pixels[i] = new byte[image.Height];
                for (int j = 0; j < image.Height; j++)
                {
                    var pixel = bitmap.GetPixel(j, i);
                    pixels[i][j] = pixel.A;
                }
            }
            var newPixels = ImageCutter.CutImage(pixels);
            newPixels = ImageCutter.MakeSquare(newPixels);
   

            //OD TAD JEST NIEUDANE SKALOWANIE
            bitmap = new Bitmap(newPixels.Length, newPixels.Length, PixelFormat.Format24bppRgb);
            for (int i = 0; i < newPixels.Length; i++)
            {
                for (int j = 0; j < newPixels.Length; j++)
                {
                    byte val =newPixels[i][j];
                    var c = Color.FromArgb(val, val, val);
                    bitmap.SetPixel(i,j, c);
                }
            }
            Bitmap scaledBitmap = new Bitmap(bitmap, 28,28);
            byte[][] scaledPixels = new byte[28][];
            for (int i = 0; i < 28; i++)
            {
                scaledPixels[i] = new byte[28];
                for (int j = 0; j < 28; j++)
                {
                    var p = scaledBitmap.GetPixel(i, j);
                    scaledPixels[i][j] = p.R;
                }
            }
            //StringBuilder builder = new StringBuilder();
            //builder.AppendLine(scaledPixels.Length + " " + scaledPixels[0].Length);
            //for (int i = 0; i < scaledPixels.GetLength(0); i++)
            //{
            //    for (int j = 0; j < scaledPixels[0].Length; j++)
            //    {
            //        if (scaledPixels[i][j] == 0)
            //        {
            //            builder.Append('_');
            //        }
            //        else
            //        {
            //            builder.Append('O');
            //        }
            //    }
            //    builder.Append("\n");
            //}
          NeuralNet network = new NeuralNet(Server.MapPath(@"~/App_Data/digit_neuralnet"));
           DigitImage digit = new DigitImage(scaledPixels, 255);
            Debug.Write('{');
            for (int i = 0; i < digit.pixels.Length; i++)
            {
                Debug.Write('{');
                for (int j = 0; j < digit.pixels.Length; j++)
                {
                    Debug.Write(digit.pixels[i][j] + ", ");
                }
                Debug.Write("},");
            }
            Debug.Write('}');
            var output = network.Run(digit.GetInputData());
            List<int> ret = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                if (output[i] > 0.9d)
                {
                    ret.Add(i);
                }
            }
            Debug.WriteLine(digit.ToString());
            return Json(ret.ToArray());
        }

        Bitmap CreateImage(Bitmap original, int x, int y, int width, int height)
        {
            var img = new Bitmap(width, height);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            }

            return img;
        }


    }
}