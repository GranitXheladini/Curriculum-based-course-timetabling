﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLgenerator.Data.Model
{
   public class CourseWithName
    {
        public Course course { get; set; }
        public string name { get; set; }
        public CourseWithName()
        {
            course = new Course();
        }
    }
}
