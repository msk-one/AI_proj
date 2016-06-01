using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AI_proj.Models;

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
        [HttpPost]
        public ActionResult Index(UploadImageModel model)
        {
                Bitmap original = null;
                var name = "newimagefile";
                var errorField = string.Empty;

                errorField = "File";
                name = Path.GetFileNameWithoutExtension(model.File.FileName);
                original = Bitmap.FromStream(model.File.InputStream) as Bitmap;


                if (original != null)
                {
                    var fn = Server.MapPath("~/Content/img/" + name + ".png");
                    var img = CreateImage(original, model.X, model.Y, model.Width, model.Height);
                    img.Save(fn, System.Drawing.Imaging.ImageFormat.Png);
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(errorField, "Your upload did not seem valid. Please try again using only correct images!");
        
            return View(model);
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