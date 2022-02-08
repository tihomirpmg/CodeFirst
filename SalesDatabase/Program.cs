using SalesDatabase.Data;
using System;

namespace SalesDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SalesContext();
            context.Database.EnsureCreated();
        }
    }
}
