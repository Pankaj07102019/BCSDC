using BCSDC.Model;
using Common;
using System;
using System.Data.SqlClient;

namespace BCSDC.DAL
{
    public static class FormsDAL
    {

        public static int SaveForm(FormPreview FromDetails, string Mode)
        {
            int rtval = 0;
            foreach (var Items in FromDetails.lstControls)
            {
                DataAccess da = new DataAccess();
                SqlParameter[] prm = new SqlParameter[5];
                prm[0] = new SqlParameter("@Form_Name", FromDetails.FormName);
                prm[1] = new SqlParameter("@Field_Name", Items.FieldName);
                prm[2] = new SqlParameter("@Field_Type", Items.FieldType);
                prm[3] = new SqlParameter("@Field_Value", Items.FieldValue == null ? "" : Items.FieldValue);
                prm[4] = new SqlParameter("@Mode", Mode);
                rtval = da.executeDMLQuery("Insert_Edit_Forms", prm);
            }
            return rtval;
        }
    }
}
