using System.ComponentModel;

namespace Work.Logic
{
    public enum SearchFilter
    {
        [Description("Starts With")]
        StartsWith,

        [Description("Contains")]
        Contains,

        [Description("Equals")]
        Equals
    }

    public class FilterModel
    {
        public SearchFilter Filter { get; set; }

        public string Match { get; set; }
    }
}