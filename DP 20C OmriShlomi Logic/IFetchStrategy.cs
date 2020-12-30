using System.Collections.Generic;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace DP_20C_OmriShlomi_Logic
{
    public interface IFetchStrategy
    {
         void FetchFromApi(User i_FaceBookUser, List<string> i_InfoToReturn);
    }
}
