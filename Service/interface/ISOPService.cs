﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{

    public interface ISOPService : IEntityService<SOP>
    {
        SOP GetById(long Id);
    }
}
