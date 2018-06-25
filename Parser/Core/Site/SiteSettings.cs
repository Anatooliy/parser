
namespace Parser.Core.Site
{
    class SiteSettings : IParserSettings
    {
        public SiteSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        public string BaseUrl { get; set; } = "https://book24.kz";

        public string Section { get; set; } = "/catalog/uchimsya_v_shkole";

        public string Prefix { get; set; } = "?PAGEN_1={CurrentId}";

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }
    }
}
