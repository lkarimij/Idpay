using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeiliBeauty.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }

        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }

        [Required]
        public string Message { get; set; }

        public HttpPostedFileBase Upload { get; set; }

        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
    }
}