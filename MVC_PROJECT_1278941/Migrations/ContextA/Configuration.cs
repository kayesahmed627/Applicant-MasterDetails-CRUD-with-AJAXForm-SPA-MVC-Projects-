namespace MVC_PROJECT_1278941.Migrations.ContextA
{
    using MVC_PROJECT_1278941.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_PROJECT_1278941.Models.ApplicantDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ContextA";
            ContextKey = "MVC_PROJECT_1278941.Models.ApplicantDbContext";
        }

        protected override void Seed(MVC_PROJECT_1278941.Models.ApplicantDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


//            context.Departments.AddRange(new Department[]
//{
//            new Department{ DepartmentName="Arabi"},
//            new Department{ DepartmentName="Spanish"},

//});
//            context.SaveChanges();
//            Applicant s = new Applicant
//            {
//                ApplicantName = "Mahbubur Rahman",
//                DepartmentId = 17,
//                Phone = "01648493795",
//                BirthDate = new DateTime(2024, 1, 1),
//                PayRate=350.00M,
//                Picture = "2.jpg"
//            };
//            s.Qualifications.Add(new Qualification { Institute = "Dhaka College", PassingYear = 2010, Degree = Degree.HSC });
//            s.Qualifications.Add(new Qualification { Institute = "Bangla College", PassingYear = 2008, Degree = Degree.SSC });
//            context.Applicants.Add(s);
//            context.SaveChanges();

        }
    }
}
