using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corwords.Struct
{
    public abstract class CordocStruct
    {
        public bool HasYaml { get; set; }
        public string Yaml { get; set; }
        public string Markdown { get; set; }
        public string Html { get; set; }
        public string Raw { get; set; }
    }
}
