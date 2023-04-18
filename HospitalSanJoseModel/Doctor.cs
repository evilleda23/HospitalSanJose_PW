﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSanJoseModel
{
    public class Doctor
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Specialty { get; set; } = null!;

        public int YearsOfExperience { get; set; }

        public int Qualification { get; set; }
        public User User { get; set; } = null!;
        public Response? Response { get; set; }
    }
}
