using System;

namespace DP_20C_OmriShlomi_Logic
{
    public class PageSorterByLikes : PageSorterBase
    {
        protected override int shouldSwap(PageWrapper i_Page1, PageWrapper i_Page2)
        {
            return (int)Math.Max((int)i_Page1.LikesCount, (int)i_Page2.LikesCount);
        }
    }
}
