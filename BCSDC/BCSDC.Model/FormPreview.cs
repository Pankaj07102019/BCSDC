using System;
using System.Collections.Generic;

namespace BCSDC.Model
{
    public class FormPreview
    {
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
