using MVC_PROJECT_1278941.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_PROJECT_1278941.ViewModel
{
    public class QualificationViewModel
    {
        public int QualificationId { get; set; }
        [Required, StringLength(50), Display(Name = "Institute Name")]
        public string Institute { get; set; }
        [Required]
        public int PassingYear { get; set; }
        [Required]
        public Degree Degree { get; set; }
        [Required]
        public int ApplicantId { get; set; }
        [Display(Name = "Applicant Name")]
        public string ApplicantName { get; set; }

    }
}