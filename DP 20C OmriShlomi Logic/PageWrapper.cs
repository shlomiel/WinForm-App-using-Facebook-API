using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace DP_20C_OmriShlomi_Logic
{
    public class PageWrapper : Page, IPageComponent
    {
        public void PostOnPage(string i_Post)  
        {
            PostStatus(i_Post);
        }

        public long OnGetNumberOfLikes()
        {
            return LikesCount.Value;
        }

        public string OnGetPageDescription()
        {
            return OnGetPageDescription();
        }

        public void OnPostOnPage(string o_Post)
        {
            PostOnPage(o_Post);
        }
    }
}
