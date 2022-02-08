﻿using HospitalDatabase.Data;
using System;

namespace HospitalDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new HospitalContext();
            context.Database.EnsureCreated();
        }
    }
}
