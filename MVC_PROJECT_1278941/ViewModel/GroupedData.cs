using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVC_PROJECT_1278941.Models;

namespace MVC_PROJECT_1278941.ViewModel
{
    public class GroupedData
    {
        public string Key { get; set; }
        public IEnumerable<Qualification> Data { get; set; } = new List<Qualification>();
    }
}