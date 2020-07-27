using Recodme.Rd.EDA.BusinessLayer.BO.JobInfoBO;
using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.DataAccess.Context;
using Recodme.Rd.EDA.DataAccess.DAO.JobInfoDAO;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Linq.Expressions;

namespace Recodme.Rd.EDA.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var database = new ApplicationContext();
            database.Database.EnsureDeleted();
            database.Database.EnsureCreated();
        }
    }
}
