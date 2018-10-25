using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Model;
using IRepository;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TestCompanyController
    {
        private readonly ICompanyRepo MockCompanyRepository;
        public TestCompanyController()
        {
            List<CompanyModel> companies = new List<CompanyModel>
                {
                    new CompanyModel {
                        ID = 1,
                        Name = "Apple Inc.",
                        ISIN = "US0378331005",
                        Exchange = "NADAQ",
                        Ticker="AAPL",
                        Website ="http://www.apple.com"},
                    new CompanyModel {
                        ID = 2,
                        Name = "British Airways PLC",
                        ISIN = "US1104193065",
                        Exchange = "Pink sheets",
                        Ticker="BAIRY",
                        Website =""},
                    
                };

            // Mock the Company Repository using Moq
            Mock<ICompanyRepo> mockCompanyRepository = new Mock<ICompanyRepo>();

            // Return all the Companies
            mockCompanyRepository.Setup(mr => mr.GetAllComapnies()).Returns(companies);

            // return a company by Id
            mockCompanyRepository.Setup(mr => mr.GetCompanyByID(
                         It.IsAny<int>())).Returns((int i) => companies.Where(
                           x => x.ID == i).Single());

            // return by ISIN
            mockCompanyRepository.Setup(mr => mr.GetCompanyByISIN(
                It.IsAny<string>())).Returns((string s) => companies.Where(
                x => x.ISIN == s).Single());
            
            this.MockCompanyRepository = mockCompanyRepository.Object;
        }


        [TestMethod]
        public void TestGetCompanyByID()
        {
            CompanyModel testCompany = this.MockCompanyRepository.GetCompanyByID(2);

            Assert.IsNotNull(testCompany); // Test if null
            Assert.IsInstanceOfType(testCompany, typeof(CompanyModel)); // Test type
            Assert.AreEqual("British Airways PLC", testCompany.Name); // Verify 
        }

        [TestMethod]
        public void TestCompanyByISIN()
        {
            CompanyModel testCompany = this.MockCompanyRepository.GetCompanyByISIN("US0378331005");

            Assert.IsNotNull(testCompany); 
            Assert.IsInstanceOfType(testCompany, typeof(CompanyModel));
            Assert.AreEqual(1, testCompany.ID); 
        }

        [TestMethod]
        public void TestGetAllCompanies()
        {
            List<CompanyModel> testCompanies = this.MockCompanyRepository.GetAllComapnies();

            Assert.IsNotNull(testCompanies); 
            Assert.AreEqual(2, testCompanies.Count); 
        }
    }
}


