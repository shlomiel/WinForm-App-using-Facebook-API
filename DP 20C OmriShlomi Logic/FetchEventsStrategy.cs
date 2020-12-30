using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace DP_20C_OmriShlomi_Logic
{
    public class FetchEventsStrategy : IFetchStrategy
    {
        void IFetchStrategy.FetchFromApi(FacebookWrapper.ObjectModel.User i_FaceBookUser, List<string> i_InfoToReturn)
        {
            if (i_FaceBookUser.Events.Count > 0)
            {
                foreach (Event fbEvent in i_FaceBookUser.Events)
                {
                    i_InfoToReturn.Add(fbEvent.Place.Name);
                }
            }
            else
            {
                i_InfoToReturn = null;
            }
        }
    }
}
