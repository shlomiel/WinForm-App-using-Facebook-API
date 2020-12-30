using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace DP_20C_OmriShlomi_Logic
{
    public class FetchFriendsStrategy : IFetchStrategy
    {
        void IFetchStrategy.FetchFromApi(User i_FaceBookUser, List<string> i_InfoToReturn)
        {
            if (i_FaceBookUser.Friends.Count > 0)
            {
                foreach (User friend in i_FaceBookUser.Friends)
                {
                    i_InfoToReturn.Add(string.Format("{0} {1}", friend.FirstName, friend.LastName));
                }
            }
            else
            {
                i_InfoToReturn = null;
            }
        }
    }
}
