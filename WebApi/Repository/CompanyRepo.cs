using System.Collections.Generic;
using IRepository;
using Model;
using DataAccess;

namespace Repository
{
    public class CompanyRepo : ICompanyRepo
    {
        public List<CompanyModel> GetAllComapnies()
        {
            return CompanyDataAccess.GetAllCompanies();
        }

        public CompanyModel GetCompanyByID(int id)
        {
            return CompanyDataAccess.GetCompanyByID(id);
        }

        public CompanyModel GetCompanyByISIN(string isin)
        {
            return CompanyDataAccess.GetCompanyByISIN(isin);
        }

        public bool CheckIsinExists(string isin)
        {
            bool exists = false;

            if (CompanyDataAccess.GetCompanyByISIN(isin) != null)
                exists = true;

            return exists;
        }

        public bool CreateCompany(string name, string exchange, string isin, string ticker, string url)
        {
            return CompanyDataAccess.CreateCompany(name,exchange,isin,ticker,url);
        }

        public bool UpdateCompany(CompanyModel company)
        {
            return CompanyDataAccess.UpdateCompany(company);
        }
    }
}
