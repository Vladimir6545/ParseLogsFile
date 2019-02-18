using ParseLogFile.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParseLogFile.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public JsonResult Upload()
        //{
        //    for (int i = 0; i < Request.Files.Count; i++)
        //    {
        //        HttpPostedFileBase file = Request.Files[i]; 
        //        int fileSize = file.ContentLength;
        //        string fileName = file.FileName;
        //        string mimeType = file.ContentType;
        //        System.IO.Stream fileContent = file.InputStream;
        //        file.SaveAs(Server.MapPath("~/Files/") + fileName);
        //        // SaveFileToDatabase();
        //    }
        //    return Json("Uploaded " + Request.Files.Count + " files");
        //}

        //public void SaveFileToDatabase()
        //{
        //    string path = Server.MapPath("~/Files/");
        //    var filename = new DirectoryInfo(path).GetFiles();
        //    string[] readText = System.IO.File.ReadAllLines(path + filename[0].Name);
        //    string[] words = readText[0].Split(' ');
        //}
    }
}