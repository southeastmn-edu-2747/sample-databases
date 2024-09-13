using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using mswanson2747_2017_ex1c2.Models;

namespace mswanson2747_2017_ex1c2.Data
{
    public class DbInitializer
    {
        public static void Initialize(WideWorldContext context)
        {
            context.Database.EnsureCreated();

            // Check for previously populated Countries table
            if (context.Countries.Any()) return;

            // Insert Countries
            string insertSqlCmd;
            System.IO.StreamReader file =
               new System.IO.StreamReader(".\\Data\\InsertCountries.sql");
            while ((insertSqlCmd = file.ReadLine()) != null)
            {
                context.RawSqlReturn.FromSql("set identity_insert Application.Countries ON; " + insertSqlCmd + "select 1 as Id;").ToList();
            }
            file.Close();
            context.RawSqlReturn.FromSql("set identity_insert Application.Countries OFF; select 1 as Id;").ToList();
            context.SaveChanges();
        }
    }
}
