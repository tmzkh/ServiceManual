using System;
using System.Collections.Generic;
using System.Text;

namespace EtteplanMORE.ServiceManual.ApplicationCore.PaginationOptions {
    /// <summary>
    /// Options for paginating api results
    /// </summary>
    public class PaginationOpts {
        const int maxPageSize = 100;
        public int PageNumber { get; set; } = 1;

        private int pageSize = 50;
        public int PageSize {
            get {
                return pageSize;
            }
            set {
                pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

    }
}
