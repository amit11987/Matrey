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
            _entities = context;
        }

        public Test GetById(long testID)
        {
           
            return _dbset.Where(x => x.TestID == testID).FirstOrDefault();
        }

        public void InsertORUpdateORDelete(long testID)
        {
            var test =  _dbset.Where(x => x.TestID == testID).FirstOrDefault();
            for (int i = 0; i < test.TestParameters.Count; i++)
            {
                if (test.TestParameters[i].TestID > 0)
                {
                 
                }
            }
            _entities.SaveChanges();
        }
    }
}
