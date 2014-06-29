using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    [Serializable]
    public class Document
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
