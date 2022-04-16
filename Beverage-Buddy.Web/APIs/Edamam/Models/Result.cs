﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.APIs.Edamam.Models
{
    public class Result
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Count { get; set; }
        public string Link { get; set; }
        public List<Recipe> Hits { get; set; }
    }
}
