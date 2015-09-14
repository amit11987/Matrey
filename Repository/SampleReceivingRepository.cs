using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Repository
{
    public class SampleReceivingRepository : GenericRepository<SampleReceive>, ISampleReceivingRepository
    {
        public SampleReceivingRepository(DbContext context)
            : base(context)
        {

        }

        public SampleReceive GetById(long SampleReceiveID)
        {
            return _dbset.Where(x => x.SampleReceiveID == SampleReceiveID).FirstOrDefault(); 
        }

        
    }
}
