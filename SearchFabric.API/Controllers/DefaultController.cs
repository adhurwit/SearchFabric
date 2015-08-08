using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ServiceFabric.Data.Collections;
using SearchFabric.API.Models;

namespace SearchFabric.API.Controllers
{
    public class DefaultController : ApiController
    {


        // GET api/values
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };

        }

        public IEnumerable<DocDto> Get(string q)
        {
            List<DocDto> docs = new List<DocDto>();
            foreach(var d in Registry.SearchFabricIndex.Query(q).GetAwaiter().GetResult())
                docs.Add(new DocDto() { Id = d.Id, Text = d.Text });
            return docs;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "get doc: " + id;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]DocDto value)
        {
            Registry.SearchFabricIndex.Add(new Index.TestDocument() { Id = value.Id, Text = value.Text });
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
