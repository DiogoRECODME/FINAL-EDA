using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Rd.EDA.BusinessLayer.BO.JobInfoBO;
using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Recodme.Rd.EDA.ApplicationTests.NormalTests.JobInfoTests
{
    [TestClass]
    public class ProposalTests
    {
        [TestMethod]
        public void TestCreateAndListProposal()
        {
            ApplicationSeeder.Seed();

            var bo = new ProposalBO();
            var foreignBO = new JobBO();

            var proposal = new Proposal("User Teste", "Mensagem de teste", foreignBO.ListUndeleted().Result.First().Id);

            var resCreate = bo.Create(proposal);
            var resGet = bo.Read(proposal.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListProposal()
        {
            ApplicationSeeder.Seed();

            var bo = new ProposalBO();

            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateProposal()
        {
            ApplicationSeeder.Seed();

            var bo = new ProposalBO();

            var resList = bo.List();

            var item = resList.Result.FirstOrDefault();
            item.Message = "Oi";

            var resUpdate = bo.Update(item);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().Message == "Oi");
        }

        [TestMethod]
        public void TestDeleteProposal()
        {
            ApplicationSeeder.Seed();

            var bo = new ProposalBO();

            var resList = bo.List();
            var total = resList.Result.Count;

            var resDelete = bo.Delete(resList.Result.First().Id);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}