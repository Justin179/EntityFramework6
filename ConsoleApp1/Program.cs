﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var db = new ContosoUniversityEntities())
            {

                db.Database.Log = (sql) => { Console.WriteLine(sql); };

                //var data = db.Course.Where(p => p.Title.Contains("Git")).ToList();
                var data = (from p in db.Course
                            where p.Title.Contains("Git")
                            select p).ToList();

                foreach (var item in data)
                {
                    Console.WriteLine(item.Title);
                }


                //請比較以下兩種取得資料的 T-SQL 差別
                // db.Department.ToList()
                // db.Department.Include("Course").ToList()


            }
        }




    }
}