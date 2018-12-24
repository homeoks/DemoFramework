using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class PagingModel<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItem { get; set; }
        public List<T> Data { get; set; }
        public int TotalPage
        {
            get
            {
                if (PageSize == 0) PageSize = 1;
                var totalPage = TotalItem / PageSize;
                if (TotalItem % PageSize > 0)
                {
                    totalPage++;
                }
                if (totalPage == 0) totalPage = 1;
                return totalPage;
            }
        }
    }
}
