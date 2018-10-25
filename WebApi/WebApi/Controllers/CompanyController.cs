using System;
using System.Collections.Generic;
using System.Web.Http;
using Model;
using IRepository;
using System.Net.Http;
using System.Net;

namespace WebApi
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyRepo companyRepo;
        public CompanyController(ICompanyRepo _companyRepo)
        {
            companyRepo = _companyRepo;
        }

        [Route("api/GetAllCompanies")]
        public IEnumerable<CompanyModel> GetAllCompanies()
        {
            return (companyRepo.GetAllComapnies());
        }

        [Route("api/Company/GetCompanyByID/{id}")]
        public CompanyModel GetCompanyByID(int id)
        {
            return (companyRepo.GetCompanyByID(id));
        }

        [Route("api/Company/GetCompanyByIsin/{isin}")]
        public CompanyModel GetCompanyByIsin(string isin)
        {
            return (companyRepo.GetCompanyByISIN(isin));
        }

        [Route("api/Company/CreateCompany")]
        public HttpResponseMessage CreateCompany(string name, string exchange, string isin, string ticker, string url)
        {
            var message = string.Empty;

            if (companyRepo.CheckIsinExists(isin))
            {
                message = string.Format("Company with ISIN {0} already exists.", isin);
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(message) };
            }
            else
            {
                if (companyRepo.CreateCompany(name, exchange, isin, ticker, url))
                {
                    message = string.Format("Company {0} created successfully.", name);
                    return new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent(message) };
                }
                else
                {
                    message = string.Format("Error occured in createing Company.");
                    return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(message) };
                }
            }
        }

        [Route("api/Company/UpdateCompany")]
        public HttpResponseMessage UpdateCompany(CompanyModel company)
        {
            var message = string.Empty;

            if (companyRepo.CheckIsinExists(company.ISIN))
            {
                message = string.Format("Company with ISIN {0} already exists.", company.ISIN);
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(message) };
            }
            else
            {
                if (companyRepo.UpdateCompany(company))
                {
                    message = string.Format("Company {0} updated successfully.", company.Name);
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(message) };
                }
                else
                {
                    message = string.Format("Error occured in updating Company.");
                    return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(message) };
                }
            }
        }
             
    }
}
