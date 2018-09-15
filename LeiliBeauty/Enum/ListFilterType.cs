using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeiliBeauty.Enum
{
    public enum ListFilterType
    {
        [Description("تمام محصولات")]
        All,
        [Description("محصولات موجود")]
        Stored,
        [Description("محصول هایی که به زودی موجود میشوند")]
        InWay

    }
}