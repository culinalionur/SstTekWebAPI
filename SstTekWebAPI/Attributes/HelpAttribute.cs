using System.Text.Json.Serialization;

namespace SstTekWebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Property)]
    public class HelpAttribute : Attribute
    {
        [JsonPropertyName("")]
        public string Url { get; set; }
        public HelpAttribute(string url)
        {
            Url = url;
        }
    }
}
