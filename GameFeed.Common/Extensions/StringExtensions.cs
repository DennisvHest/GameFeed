using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFeed.Common.Extensions {

    public static class StringExtensions {

        public static int? ParseNullableInt(this string val) {
            int i;
            return int.TryParse(val, out i) ? (int?)i : null;
        }
    }
}
