using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Repository
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(DbContext context)
            : base(context)
        {

        }

        public Test GetById(long testID)
        {
            return _dbset.Where(x => x.TestID == testID).FirstOrDefault();
        }
    }
}
