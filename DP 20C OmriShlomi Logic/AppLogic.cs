using System.Collections.Generic;
using System.Drawing;
using FacebookWrapper.ObjectModel;

namespace DP_20C_OmriShlomi_Logic
{
    public class AppLogic
    {
        public User TheLoggedInUser { get; set; }

        private BasicFeatures m_BasicFeatureSubSystem = new BasicFeatures();
        private FirstFeature m_FirstFeatureSubSystem = new FirstFeature();
        private SecondFeature m_SecondFeatureSubSystem = new SecondFeature();

        public void LoginAndInitiate()
        {
            TheLoggedInUser = m_BasicFeatureSubSystem.LoginAndInitiate();
        }

        public string GetCoverPhoto()
        {
            string urlToReturn = null;
            foreach (Album album in TheLoggedInUser.Albums)
            {
                if (album.Name == "Cover Photos" && album.Count > 0)
                {
                    urlToReturn = album.CoverPhoto.PictureNormalURL;
                }
            }

            return urlToReturn;
        }

        public List<string> FetchFriends()
        {
            m_BasicFeatureSubSystem.FetchStartegy = new FetchFriendsStrategy();
            return m_BasicFeatureSubSystem.FetchRequiredInfoFromApi(TheLoggedInUser);
        }

        public List<string> FetchPages()
        {
            m_BasicFeatureSubSystem.FetchStartegy = new FetchPagesStrategy();
            return m_BasicFeatureSubSystem.FetchRequiredInfoFromApi(TheLoggedInUser);
        }

        public List<string> FetchCheckIns()
        {
            m_BasicFeatureSubSystem.FetchStartegy = new FetchCheckInsStrategy();
            return m_BasicFeatureSubSystem.FetchRequiredInfoFromApi(TheLoggedInUser);
        }

        public List<string> FetchEvents()
        {
            m_BasicFeatureSubSystem.FetchStartegy = new FetchEventsStrategy();
            return m_BasicFeatureSubSystem.FetchRequiredInfoFromApi(TheLoggedInUser);
        }

        public Status SentStatusPost(string i_TextToPost)
        {
            return m_BasicFeatureSubSystem.SentStatusPost(TheLoggedInUser, i_TextToPost);
        }

        public string FetchHomeTown()
        {
            return TheLoggedInUser.Hometown.Name;
        }

        public string GetTripUrl(string i_StartingPoint, string i_Destenation, string i_CommuteType)
        {
            return m_FirstFeatureSubSystem.GetTripUrl(i_StartingPoint, i_Destenation, i_CommuteType);
        }

        public Image TurnImageBlackAndWhite(Image i_OriginalImage)
        {
            return m_SecondFeatureSubSystem.TurnImageBlackAndWhite(i_OriginalImage);
        }

        public Image RotateImage(Bitmap i_OriginalImage)
        {
            return m_SecondFeatureSubSystem.RotateImage(i_OriginalImage);
        }

        public string GetSelectedFriend(string i_FriendsName)
        {
            return m_BasicFeatureSubSystem.GetSelectedFriend(TheLoggedInUser, i_FriendsName);
        }

        public string GetSelectedEvent(string i_selectedEvent)
        {
            return m_BasicFeatureSubSystem.GetSelectedEvent(TheLoggedInUser, i_selectedEvent);
        }

        public string GetSelectedCheckIn(string i_selectedCheckIn)
        {
            return m_BasicFeatureSubSystem.GetSelectedCheckIn(TheLoggedInUser, i_selectedCheckIn);
        }

        public string GetUserAge()
        {
            return m_BasicFeatureSubSystem.GetUserAge(TheLoggedInUser);
        }

        public void PostToPages(string o_Post, List<string> i_PagesToPostTo)
        {
            m_BasicFeatureSubSystem.PostToPages(TheLoggedInUser, o_Post, i_PagesToPostTo);
        }

        public void SetUserAbout(string i_newAbout)
        {
            TheLoggedInUser.About = i_newAbout;
        }

        public List<string> FetchSortedPages(string o_PagesToReturn)
        {
            return m_BasicFeatureSubSystem.FetchSortedPages(o_PagesToReturn);
        }
    }
}