﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace SearchFabric.API
{
    public interface IOwinAppBuilder
    {
        void Configuration(IAppBuilder appBuilder);

    }
}