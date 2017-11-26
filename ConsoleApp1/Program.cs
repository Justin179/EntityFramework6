using System;
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

                // 印出sql指令
                db.Database.Log = (sql) => { Console.WriteLine(sql); };

                //GetCourse_Git(db);
                //GetDepartment(db);
                //AddCourse(db);
                //UpdateCourse(db);
                //DeleteCourse(db);


                var one = db.Course.Find(6);
                //// 改狀態
                //db.Entry(one).State = System.Data.Entity.EntityState.Modified;
                //// 依狀態，所以執行update sql
                //db.SaveChanges();

                //var c = new Course() { CourseID = 7 };
                //// 改狀態
                //db.Entry(c).State = System.Data.Entity.EntityState.Deleted;
                //db.Course.Remove(c);
                //// 依狀態，所以執行delete sql
                //db.SaveChanges();

                // 這邊是看狀態的變化
                Console.WriteLine(db.Entry(one).State);
                one.MyCredits += 1;
                Console.WriteLine(db.Entry(one).State);

                ////db.Entry(one).State = System.Data.Entity.EntityState.Deleted;
                //db.SaveChanges();

                //Console.WriteLine(db.Entry(one).State);



                Console.ReadLine(); // 有這一行，console才不會馬上就被關掉
            }
        }

        private static void DeleteCourse(ContosoUniversityEntities db)
        {
            var c = db.Course.Find(9);
            db.Course.Remove(c);
            db.SaveChanges();
        }

        private static void UpdateCourse(ContosoUniversityEntities db)
        {
            var items = db.Course.Where(p => p.Title.Contains("Git"));
            foreach (var item in items)
            {
                item.MyCredits += 1;
            }
            db.SaveChanges(); // (執行此行程式，進行一次交易) 整批commit / rollback to db
        }

        private static void AddCourse(ContosoUniversityEntities db)
        {
            var c = new Course()
            {
                Title = "Entity Framework 6.1",
                MyCredits = 100,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now

        };
            c.Department = db.Department.Find(2);

            db.Course.Add(c);
            db.SaveChanges();
        }

        private static void GetDepartment(ContosoUniversityEntities db)
        {
            var data = (from p in db.Department.Include("Course") select p);
            foreach (var dept in data)
            {
                Console.WriteLine(dept.Name);
                foreach (var course in dept.Course)
                {
                    Console.WriteLine("\t" + course.Title);
                }
            }
        }

        private static void GetCourse_Git(ContosoUniversityEntities db)
        {
            //var data = db.Course.Where(p => p.Title.Contains("Git")).ToList();
            var data = (from p in db.Course
                        where p.Title.Contains("Git")
                        select p).ToList();

            foreach (var item in data)
            {
                Console.WriteLine(item.Title + "\t" + item.Department.Name);
            }
        }


    }
}
