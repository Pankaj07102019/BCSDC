using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BCSDC.Models
{
    public class FormPreview
    {
        [UIHint("SignaturePad")]
        public byte[] Signature { get; set; }
        public string FormName { get; set; }

        public List<FormControlsList> lstControls { get; set; }
    }

    public class FormControlsList
    {
        public string FieldName { get; set; }

        public string FieldType { get; set; }

        public string FieldValue { get; set; }
    }
}