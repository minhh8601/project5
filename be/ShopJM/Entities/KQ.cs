using System;
using System.Collections.Generic;
using System.Linq;
namespace ShopJM.Entities
{
    public class KQ
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public long totalItem { get; set; }
        public dynamic data { get; set; }
    }
}
