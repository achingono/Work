using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Work.Data.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class UrlAttribute: RegularExpressionAttribute
    {
        public UrlAttribute()
            : base(@"(?<protocol>http|ftp|https|file)://(?<domain>[\w\.]+)(?<port>:\d+)?(?<path>/.*)?")
        { }
    }
}
