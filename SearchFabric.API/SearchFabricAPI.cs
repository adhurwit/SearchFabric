using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Services;
using SearchFabric.Index;

namespace SearchFabric.API
{
    public class SearchFabricAPI : StatelessService
    {
        public const string ServiceTypeName = "SearchFabricAPIType";

        protected override ICommunicationListener CreateCommunicationListener()
        {
            string serviceName = "SearchFabricIndex";
            string appName = this.ServiceInitializationParameters.CodePackageActivationContext.ApplicationName;

            Registry.SearchFabricIndex = ServiceProxy.Create<ISearchFabricIndex>(1,new Uri(appName + "/" + serviceName));

            return new OwinCommunicationListener("index", new Startup());
        }
        
        
    }
}
