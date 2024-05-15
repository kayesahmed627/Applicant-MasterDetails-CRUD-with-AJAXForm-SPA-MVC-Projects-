using MVC_PROJECT_1278941.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_PROJECT_1278941.ViewModel.InputModel
{
    public class ApplicantInputModel
    {
        public int ApplicantId { get; set; }
        [Required, StringLength(50), Display(Name = "Applicant Name")]
        public string ApplicantName { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
        [Required, Column(TypeName = "money"), Display(Name = "Pay Rate"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal PayRate { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public virtual List<Qualification> Qualifications { get; set; } = new List<Qualification>();

    }
}