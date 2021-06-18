using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zip_Files_MVC.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsSelected { get; set; }
    }
}