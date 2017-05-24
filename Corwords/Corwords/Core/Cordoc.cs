using Corwords.Struct;
using MarkdownSharp;
using System.Linq;
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

            // Split the content into multiple documents if they exist
            var contentParts = Raw.Split("---", System.StringSplitOptions.RemoveEmptyEntries);

            // We're making some assumptions above:
            //   First, we're assuming that if there's more than 1 content part, then YAML is at the top.
            //   Next, we're assuming that the remaining part may or may not be mixed with HTML and Markdown.
            //   Finally, if there are more than 2 parts, we'll combine the rest (for now) and assume that it's 
            //      one big piece of HTML and Markdown. We'll even add back in the separator. We're doing this 
            //      in the event we need to handle a 3rd part in the future.

            // As stated above, if more than 2 parts, assume YAML as the first part
            if (contentParts.Length >= 2)
            {
                // Set Cordoc Properties
                HasYaml = true;
                Yaml = contentParts[0].Trim();
                Markdown = string.Join("---\n\r", contentParts.Skip(1)).Trim();

                // Deserialize YAML
                var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
                MapProperties(deserializer.Deserialize<CordocPropStruct>(Yaml));
            }

            // Populate Markdown
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