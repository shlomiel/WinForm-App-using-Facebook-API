using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace DP_20C_OmriShlomi_Logic
{
    public class FetchPagesStrategy : IFetchStrategy
    {
        void IFetchStrategy.FetchFromApi(User i_FaceBookUser, List<string> i_InfoToReturn)
        {
            if (i_FaceBookUser.LikedPages.Count > 0)
            {
                foreach (PageWrapper page in i_FaceBookUser.LikedPages)
                {
                    i_InfoToReturn.Add(page.Name);
                }
            }
            else
            {
                i_InfoToReturn = null;
            }
        }
    }
}
