using Corwords.Struct;
using MarkdownSharp;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

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
            Parse();
        }

        public void Parse()
        {
            // Set Markdown to Raw upfront
            Markdown = Raw;

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

                    var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
                    MapProperties(deserializer.Deserialize<CordocPropStruct>(Yaml));
                }
            }

            var md = new Markdown(new MarkdownOptions() { LinkEmails = true, AutoHyperlink = true, AutoNewlines = false });
            Html = md.Transform(Markdown).Trim();
        }

        private void MapProperties(CordocPropStruct props)
        {
            Title = props.Title;
            Description = props.Description;
            Author = props.Author;
            DateCreated = props.DateCreated;

            UniqueId = props.UniqueId;
            UserId = props.UserId;
            Permalink = props.Permalink;
            CustomPropertyGraph = props.CustomPropertyGraph;
            DateUpdated = props.DateUpdated;
            DatePublished = props.DatePublished;
            DateExpired = props.DateExpired;
        }
    }
}