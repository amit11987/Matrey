﻿using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public interface ISOPRepository : IGenericRepository<SOP>
    {
        SOP GetById(long id);
    }
}
