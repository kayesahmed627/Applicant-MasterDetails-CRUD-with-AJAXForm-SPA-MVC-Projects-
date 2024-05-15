﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_PROJECT_1278941.ViewModel
{
    public class RegisterModel
    {
        [Required, StringLength(20)]
        public string UserName { get; set; }
        [Required, StringLength(20), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, StringLength(20), DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}