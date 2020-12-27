using BCSDC.Model;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BCSDC.DAL
{
    public static class SearchDAL
    {
        public static List<SearchModel> SearchForms(string FromName, string FieldName, string FieldType, string FieldValue)
        {
            List<SearchModel> lstForms = new List<SearchModel>();
            DataSet ds = new DataSet();
            try
            {
                DataAccess da = new DataAccess();
                SqlParameter[] prm = new SqlParameter[4];
                prm[0] = new SqlParameter("@Form_Name", FromName == null ? "" : FromName);
                prm[1] = new SqlParameter("@Field_Name", FieldName == null ? "" : FieldName);
                prm[2] = new SqlParameter("@Field_Type", FieldType == "0" ? "" : FieldType);
                prm[3] = new SqlParameter("@Field_Value", FieldValue == null ? "" : FieldValue);

                ds = da.GetDataSet("Search_Forms", prm);

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lstForms.Add(new SearchModel
                        {
                            Form_Name = dr["Form_Name"].ToString(),
                            Field_Name = dr["Field_Name"].ToString(),
                            Field_Type = dr["Field_Type"].ToString(),
                            Field_Value = dr["Field_Value"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return lstForms;
        }
    }
}
