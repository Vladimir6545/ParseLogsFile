using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ParseLogFile.Models.ViewsModels;
using ParseLogFile.Repositories;
using PagedList;
using System;
using System.Linq;

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
        public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IPSortParam = sortOrder == "IP" ? "IPDesc" : "IP";
            ViewBag.DateSortParam = sortOrder == "Date" ? "DateDesc" : "Date";
            ViewBag.TimeSortParam = sortOrder == "Time" ? "TimeDesc" : "Time";
            ViewBag.NetworkSortParam = sortOrder == "Network" ? "NetworkDesc" : "Network";
            ViewBag.SizeSortParam = sortOrder == "Size" ? "SizeDesc" : "Size";
            ViewBag.RezultSortParam = sortOrder == "Rezult" ? "RezultDesc" : "Rezult";

            List<LogsViewModel> dataView = _dataLogRepo.GetDataLogs(sortOrder);

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            if (!String.IsNullOrEmpty(search))
            {
                dataView = dataView.Where(d => d.ip.IP == search || d.descriptionFile.NominationFile == search).ToList();
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);

            if (dataView != null)
            {
                return View(dataView.ToPagedList(pageNumber, pageSize));
            }
            return View();
        }

        public ActionResult IpAddress(string sortOrder, string currentFilter, string search, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IPSortParam = sortOrder == "IP" ? "IPDesc" : "IP";
            ViewBag.CompanySortParam = sortOrder == "Company" ? "CompanyDesc" : "Company";
            ViewBag.NetworkSortParam = sortOrder == "Network" ? "NetworkDesc" : "Network";

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            List<ListIP> ipView = _ipRepo.GetIP(sortOrder);

            if (!String.IsNullOrEmpty(search))
            {
                ipView = ipView.Where(d => d.IP == search
                               || d.CompanyName == search
                               || d.NominationNetwork == search).ToList();
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);

            if (ipView != null)
            {
                return View(ipView.ToPagedList(pageNumber, pageSize));
            }
            return View();
        }

        public ActionResult Files(string sortOrder, string currentFilter, string search, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = sortOrder == "Name" ? "NameDesc" : "Name";
            ViewBag.PageSortParam = sortOrder == "Page" ? "PageDesc" : "Page";

            if (search != null)
              {
                  page = 1;
              }
            else
              {
                 search = currentFilter;
              }

            ViewBag.CurrentFilter = search;

            List<DescriptionFile> files = _fileRepo.GetFiles(sortOrder);

            if (!String.IsNullOrEmpty(search))
            {
                files = files.Where(d => d.Name == search
                               || d.NominationPage == search).ToList();
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);

            if (files != null)
            {
                return View(files.ToPagedList(pageNumber, pageSize));
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

        [HttpGet]
        public ActionResult ClearData()
        {
            _dataLogRepo.ClearData();
            return Redirect("Index");
        }
    }
}
