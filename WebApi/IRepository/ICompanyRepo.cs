using System.Collections.Generic;
using Model;

namespace IRepository
{
    public interface ICompanyRepo
    {
        CompanyModel GetCompanyByID(int id);
        CompanyModel GetCompanyByISIN(string isin);
        List<CompanyModel> GetAllComapnies();
        bool CheckIsinExists(string isin);
        bool CreateCompany(string name, string exchange, string isin, string ticker, string url);
        bool UpdateCompany(CompanyModel company);
    }
}
