using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DP_20C_OmriShlomi_Logic
{
    public class CompositePages : IPageComponent
    {
        public List<IPageComponent> Pages { get; set; }

        public void PostOnPages(string o_Post)
        {
            foreach (IPageComponent page in Pages)
            {
                page.OnPostOnPage(o_Post);
            }
        }

        public long OnGetNumberOfLikes()
        {
            long totalLikesCount = 0;
            foreach (IPageComponent page in Pages)
            {
                totalLikesCount += page.OnGetNumberOfLikes();
            }

            return totalLikesCount;
        }

        public void OnPostOnPage(string o_Post)
        {
            PostOnPages(o_Post);
        }
    }
}
