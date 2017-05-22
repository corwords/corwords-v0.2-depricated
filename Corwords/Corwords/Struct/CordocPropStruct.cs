using System;
using YamlDotNet.Serialization;

namespace Corwords.Struct
{
    public class CordocPropStruct
    {
        // Core properties
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Author { get; set; }
        [YamlMember(Alias = "date_created", ApplyNamingConventions = false)]
        public DateTime DateCreated { get; set; }

        // Extended properties
        public string UniqueId { get; set; }
        public string UserId { get; set; }
        public string Permalink { get; set; }
        [YamlMember(Alias = "custom_props", ApplyNamingConventions = false)]
        public string CustomPropertyGraph { get; set; }
        [YamlMember(Alias = "date_updated", ApplyNamingConventions = false)]
        public DateTime DateUpdated { get; set; }
        [YamlMember(Alias = "date_published", ApplyNamingConventions = false)]
        public DateTime DatePublished { get; set; }
        [YamlMember(Alias = "date_expired", ApplyNamingConventions = false)]
        public DateTime DateExpired { get; set; }
    }
}