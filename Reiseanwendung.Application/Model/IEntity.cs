﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{
    public interface IEntity<Tkey> where Tkey : struct
    {
        Tkey Id { get; } 
        Guid Guid { get;  }

        bool Equals(object obj);
    }
}
