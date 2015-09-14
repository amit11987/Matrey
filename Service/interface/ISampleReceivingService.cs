using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{

    public interface ISampleReceivingService : IEntityService<SampleReceive>
    {
        SampleReceive GetById(long SampleReceiveID);
    }
}
