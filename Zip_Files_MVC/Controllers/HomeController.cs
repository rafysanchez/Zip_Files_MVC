using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zip_Files_MVC.Models;

using System.IO;
using Ionic.Zip;
using System.IO.Compression;

namespace Zip_Files_MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/"));
            List<FileModel> files = new List<FileModel>();
            foreach (string filePath in filePaths)
            {
                files.Add(new FileModel()
                {
                    FileName = Path.GetFileName(filePath),
                    FilePath = filePath
                });
            }

            return View(files);
        }

        [HttpPost]
        public ActionResult Index(List<FileModel> files)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");
                foreach (FileModel file in files)
                {
                    if (file.IsSelected)
                    {
                        zip.AddFile(file.FilePath, "Files");
                    }
                }
                string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    zip.Save(memoryStream);
                    return File(memoryStream.ToArray(), "application/zip", zipName);
                }
            }
        }

       


    }
}
        //        @using(Html.BeginForm("Index", "Home", FormMethod.Post, new { enctype = "multipart/form-data" }))
        //{
        //    <input type = "file" name="zip"  /> 
        //    <div>
        //        <button class="btn btn-default">Submit</button>
        //    </div>
        //}

        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase zip)
        //{
        //    var uploads = Server.MapPath("~/uploads");
        //    using (ZipArchive archive = new ZipArchive(zip.InputStream))
        //    {
        //        foreach (ZipArchiveEntry entry in archive.Entries)
        //        {
        //            if (!string.IsNullOrEmpty(Path.GetExtension(entry.FullName))) //make sure it's not a folder
        //            {
        //                entry.ExtractToFile(Path.Combine(uploads, entry.FullName));
        //            }
        //            else
        //            {
        //                Directory.CreateDirectory(Path.Combine(uploads, entry.FullName));
        //            }
        //        }
        //    }
        //    ViewBag.Files = Directory.EnumerateFiles(uploads);
        //    return View();
        //}