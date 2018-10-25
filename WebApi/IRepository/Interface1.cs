using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface ICompanyADORepo
    {
        CompanyModel CreateCompany(CompanyModel company);
        CompanyModel GetCompanyByID(int id);
        CompanyModel GetCompanyByISIN(string isin);
        List<CompanyModel> GetComapnies();
        CompanyModel UpdateCompany(CompanyModel company);
    }
}
