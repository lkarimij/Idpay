using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeiliBeauty.Models
{
    public class ErrorModel
    {

        [Display(Name = "Error Message")]
        public string ErrorMessage { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }
    }

    public class UpdateModel
    {
        public string Controller { get; set; }

        public string Action { get; set; }
    }
}