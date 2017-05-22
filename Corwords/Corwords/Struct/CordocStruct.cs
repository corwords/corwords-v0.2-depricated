namespace Corwords.Struct
{
    public abstract class CordocStruct : CordocPropStruct
    {
        // Properties used for conversion
        public bool HasYaml { get; set; }
        public string Yaml { get; set; }
        public string Markdown { get; set; }
        public string Html { get; set; }
        public string Raw { get; set; }
    }
}