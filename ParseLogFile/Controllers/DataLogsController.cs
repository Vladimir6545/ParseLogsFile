using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParseLogFile.Helpers;
using ParseLogFile.Models;
using ParseLogFile.Models.ViewsModels;
using Whois.NET;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;
using ParseLogFile.Repositories;
using System.Threading.Tasks;

namespace ParseLogFile.Controllers
{
    public class DataLogsController : Controller
    {
        private DataLogRepository _dataLogRepo;
        private IpAddressRepository _ipRepo;
        private FilesRepository _fileRepo;

        public DataLogsController()
        {
            _dataLogRepo = new DataLogRepository();
            _ipRepo = new IpAddressRepository();
            _fileRepo = new FilesRepository();
        }
        // GET: DataLogs
        public ActionResult Index()
        {
            List<LogsViewModel> dataView = _dataLogRepo.GetDataLogs();
            if (dataView != null)
            {
                return View(dataView);
            }
            return View();
        }

        public ActionResult IpAddress()
        {
            List<ListIP> ipView = _ipRepo.GetIP();
            if (ipView != null)
            {
                return View(ipView);
            }
            return View();
        }

        public ActionResult Files()
        {
            List<DescriptionFile> files = _fileRepo.GetFiles();
            if (files != null)
            {
                return View(files);
            }
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                file.SaveAs(Server.MapPath("~/Files/") + fileName);
                _dataLogRepo.SaveToDatabase();
            }
            return Json("Uploaded " + Request.Files.Count + " files");
        }
    }
}
