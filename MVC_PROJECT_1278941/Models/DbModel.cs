using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_PROJECT_1278941.Models
{
    public enum Degree { SSC = 1, HSC, Honours, Masters }
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required, StringLength(50), Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        //navigation
        public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
    }
    public class Applicant
    {
        public int ApplicantId { get; set; }
        [Required, StringLength(50), Display(Name = "Applicant Name")]
        public string ApplicantName { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
        [Required, Column(TypeName = "money"), Display(Name ="Pay Rate"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal PayRate { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; }
        [Required, ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();
    }
    public class Qualification
    {
        public int QualificationId { get; set; }
        [Required, StringLength(50), Display(Name = "Institute Name")]
        public string Institute { get; set; }
        [Required]
        public int PassingYear { get; set; }
        [Required]
        public Degree Degree { get; set; }

        //fk
        [Required, ForeignKey("Applicant")]
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
    public class ApplicantDbContext : DbContext
    {
        public ApplicantDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
    }
}