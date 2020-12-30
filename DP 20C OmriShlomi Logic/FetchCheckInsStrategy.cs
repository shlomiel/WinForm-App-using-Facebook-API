using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace DP_20C_OmriShlomi_Logic
{
    public class FetchCheckInsStrategy : IFetchStrategy
    {
        void IFetchStrategy.FetchFromApi(User i_FaceBookUser, List<string> i_InfoToReturn)
        {
            if (i_FaceBookUser.Checkins.Count > 0)
            {
                foreach (Checkin checkin in i_FaceBookUser.Checkins)
                {
                    i_InfoToReturn.Add(checkin.Place.Name);
                }
            }
            else
            {
                i_InfoToReturn = null;
            }
        }
    }
}
