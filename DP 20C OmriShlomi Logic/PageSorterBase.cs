using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace DP_20C_OmriShlomi_Logic
{
    public abstract class PageSorterBase
    {
        public void Sort(List<Page> i_ListToSort)
        {
            i_ListToSort.Sort((page1, page2) =>
            {
                return shouldSwap(page1 as PageWrapper, page2 as PageWrapper);
            });
        }

        protected abstract int shouldSwap(PageWrapper i_Page1, PageWrapper i_Page2);
    }
}