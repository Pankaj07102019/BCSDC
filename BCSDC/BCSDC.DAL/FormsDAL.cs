using BCSDC.Model;
using Common;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

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

        public static int SaveFormControlList(FormPreview FormDetails)
        {
            int rtval = 0;
            DataTable dt = ToDataTable(FormDetails.lstControls);
            DataAccess da = new DataAccess();
            SqlParameter[] prm = new SqlParameter[2];
            prm[0] = new SqlParameter("@Control_list", dt);
            prm[0].SqlDbType = SqlDbType.Structured;
            prm[0].TypeName = "dbo.FormControlsList";

            prm[1] = new SqlParameter("@Form_Name", FormDetails.FormName);
            rtval = da.executeDMLQuery("INSERT_FORM_CONTROLS", prm);
            return rtval;
        }
        public static int SaveFormFillingDetails(FillingFormsDetails FormFillingDetails)
        {
            int rtval = 0;
            DataTable dt = ToDataTable(FormFillingDetails.lstControls);
            DataAccess da = new DataAccess();
            SqlParameter[] prm = new SqlParameter[2];
            prm[0] = new SqlParameter("@Form_Filling_Details", dt);
            prm[0].SqlDbType = SqlDbType.Structured;
            prm[0].TypeName = "dbo.FormFillingDetails";

            prm[1] = new SqlParameter("@Form_Name", FormFillingDetails.FormName);
            rtval = da.executeDMLQuery("Form_Filling_Details", prm);
            return rtval;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null) == null ? "" : Props[i].GetValue(item, null);
                    //values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public static int DeleteForm(string Form_Name)
        {
            int rtval = 0;
            DataAccess da = new DataAccess();
            SqlParameter[] prm = new SqlParameter[1];
            prm[0] = new SqlParameter("@FormName", Form_Name);
            rtval = da.executeDMLQuery("DELETE_FORM", prm);
            return rtval;
        }
    }
}
