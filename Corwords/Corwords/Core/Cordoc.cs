using Corwords.Struct;
using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corwords.Core
{
    public class Cordoc : CordocStruct
    {
        public Cordoc()
        {
            HasYaml = false;
        }

        public Cordoc(string content)
        {
            HasYaml = false;
            Raw = content;
        }

        public void Parse()
        {
            // Determine if the document is being described
            var startYaml = Raw.IndexOf("---");
            if (startYaml >= 0)
            {
                var endYaml = Raw.IndexOf("---", startYaml + 3);
                if (endYaml >= 0)
                {
                    HasYaml = true;

                    // Pull Yaml out of content
                    Yaml = Raw.Substring(0, endYaml + 3);
                    Markdown = Raw.Substring(endYaml + 3);

                    /// TODO Parse Yaml
                }
            }

            /// TODO Parse Markdown content
            var md = new Markdown();
            Html = md.Transform(Markdown);
        }
    }
}
