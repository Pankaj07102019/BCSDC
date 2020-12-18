using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BCSDC.Models
{
    public class CreateForms
    {
        [Required]
        [Display(Name = "Form Name")]
        public string FormName { get; set; }

        [Required]
        [Display(Name = "Field Name")]
        public string FieldName { get; set; }

        [Required]
        [Display(Name = "Field Type")]
        public string FieldType { get; set; }

        [Required]
        [Display(Name = "Field Value")]
        public string FieldValue { get; set; }
    }
}