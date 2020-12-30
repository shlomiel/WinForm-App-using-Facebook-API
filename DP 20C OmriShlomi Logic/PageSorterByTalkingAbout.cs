using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DP_20C_OmriShlomi_Logic
{
    public class PageSorterByTalkingAbout : PageSorterBase
    {
        protected override int shouldSwap(PageWrapper i_Page1, PageWrapper i_Page2)
        {
            return (int)Math.Max((int)i_Page1.TalkingAboutCount, (int)i_Page2.TalkingAboutCount);
        }
    }
}
