using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Model;
using System.Linq;
using System.Data.SqlClient;

namespace DataAccess
{       /// <summary>
        /// Performs data access operations for Company api controller
        /// </summary>
    public class CompanyDataAccess : BaseDataAccess
    {
        public static List<CompanyModel> GetAllCompanies()
        {
            Database db = DatabaseFactory.CreateDatabase("CON");

            using (DbConnection conn = db.CreateConnection())
            {
                DbCommand cmd = GetDBStoredProcedure("[Company.GetAllCompanies]", db, conn);
                DataSet ds = db.ExecuteDataSet(cmd);

                List<CompanyModel> companies = ds.Tables[0].Rows.Cast<DataRow>().Select(row =>
                      new CompanyModel
                      {
                          ID = row.Get<int>("ID") ?? 0,
                          Name = row.GetString("Name"),
                          Exchange = row.GetString("Exchange"),
                          ISIN = row.GetString("ISIN"),
                          Ticker= row.GetString("Ticker"),
                          Website= row.GetString("Website")
                      }).ToList();

                return companies;
            }
        }

        public static CompanyModel GetCompanyByID(int id)
        {
            Database db = DatabaseFactory.CreateDatabase("CON");

            using (DbConnection conn = db.CreateConnection())
            {
                DbCommand cmd = GetDBStoredProcedure("[Company.GetCompanyByID]", db, conn);
                db.AddInParameter(cmd,"ID", DbType.Int32, id);

                DataSet ds = db.ExecuteDataSet(cmd);

                CompanyModel company = ds.Tables[0].Rows.Cast<DataRow>().Select(row =>
                      new CompanyModel
                      {
                          ID = row.Get<int>("ID") ?? 0,
                          Name = row.GetString("Name"),
                          Exchange = row.GetString("Exchange"),
                          ISIN = row.GetString("ISIN"),
                          Ticker = row.GetString("Ticker"),
                          Website = row.GetString("Website")
                      }).FirstOrDefault();

                return company;
            }
        }

        public static CompanyModel GetCompanyByISIN(string isin)
        {
            Database db = DatabaseFactory.CreateDatabase("CON");

            using (DbConnection conn = db.CreateConnection())
            {
                DbCommand cmd = GetDBStoredProcedure("[Company.GetCompanyByISIN]", db, conn);
                db.AddInParameter(cmd, "ISIN", DbType.String, isin);

                DataSet ds = db.ExecuteDataSet(cmd);

                CompanyModel company = ds.Tables[0].Rows.Cast<DataRow>().Select(row =>
                      new CompanyModel
                      {
                          ID = row.Get<int>("ID") ?? 0,
                          Name = row.GetString("Name"),
                          Exchange = row.GetString("Exchange"),
                          ISIN = row.GetString("ISIN"),
                          Ticker = row.GetString("Ticker"),
                          Website = row.GetString("Website")
                      }).FirstOrDefault();

                return company;
            }
        }
        
        public static bool CreateCompany(string name, string exchange, string isin, string ticker, string url)
        {
            Database db = DatabaseFactory.CreateDatabase("CON");

            using (DbConnection conn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = GetDBStoredProcedure("[Company.InsertCompany]", db, conn);

                    db.AddInParameter(cmd, "Name", DbType.String, name);
                    db.AddInParameter(cmd, "Exchange", DbType.String, exchange);
                    db.AddInParameter(cmd, "ISIN", DbType.String, isin);
                    db.AddInParameter(cmd, "Ticker", DbType.String, ticker);
                    db.AddInParameter(cmd, "Website", DbType.String, url);

                    DataSet ds = db.ExecuteDataSet(cmd);

                    return true;

                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
        }

        public static bool UpdateCompany(CompanyModel company)
        {
            Database db = DatabaseFactory.CreateDatabase("CON");

            using (DbConnection conn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = GetDBStoredProcedure("[Company.UpdateCompany]", db, conn);

                    db.AddInParameter(cmd, "ID", DbType.Int32, company.ID);
                    db.AddInParameter(cmd, "Name", DbType.String, company.Name);
                    db.AddInParameter(cmd, "Exchange", DbType.String, company.Exchange);
                    db.AddInParameter(cmd, "ISIN", DbType.String, company.ISIN);
                    db.AddInParameter(cmd, "Ticker", DbType.String, company.Ticker);
                    db.AddInParameter(cmd, "Website", DbType.String, company.Website);

                    DataSet ds = db.ExecuteDataSet(cmd);

                    return true;

                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
        }
    }
    /// <summary>
    ///  Extension methods DataRow column type
    /// </summary>
    public static class DataRowExtensions
    {
        public static T? Get<T>(this DataRow row, string columnName) where T : struct
        {
            if (row.IsNull(columnName))
                return null;

            return row[columnName] as T?;
        }

        public static string GetString(this DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
                return string.Empty;

            return row[columnName] as string ?? string.Empty;
        }

        public static bool? GetBool(this DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return null;
            }

            int intBool;
            if (int.TryParse(row[columnName].ToString(), out intBool))
            {
                return intBool == 1;
            }

            return null;
        }
    }
}
