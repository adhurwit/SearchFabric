using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFabric.API.Models
{
    [Serializable]
    public class DocDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

    }
}
