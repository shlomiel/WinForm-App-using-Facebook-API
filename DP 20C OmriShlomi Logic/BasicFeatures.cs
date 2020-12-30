using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace DP_20C_OmriShlomi_Logic
{
    internal class BasicFeatures
    {
        public IFetchStrategy FetchStartegy { get; set; }

        private PageSorterBase m_PageSorter;

        private User m_UserLoggedIn;

        public User LoginAndInitiate()
        {
            FacebookService.s_CollectionLimit = 100;
            FacebookWrapper.LoginResult result = FacebookWrapper.FacebookService.Login("864584273951131", "user_photos,user_link,public_profile,user_tagged_places,user_events,user_likes,user_location,user_posts,user_birthday, email, user_age_range, user_friends,instagram_basic");
            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                m_UserLoggedIn = result.LoggedInUser;
            }
            else
            {
                m_UserLoggedIn = null;
            }

            return m_UserLoggedIn;
        }

        public List<string> FetchRequiredInfoFromApi(User i_FaceBookUser)
        {
            List<string> infoToReturn = new List<string>();

            FetchStartegy.FetchFromApi(i_FaceBookUser, infoToReturn);

            return infoToReturn;
        }

        public Status SentStatusPost(User i_FaceBookUser, string i_TextToPost)
        {
            Status post;
            post = i_FaceBookUser.PostStatus(i_TextToPost);
            return post;
        }

        public string GetSelectedFriend(User i_FaceBookUser, string i_FriendsName)
        {
            string urlToReturn = null;
            foreach (User friend in i_FaceBookUser.Friends)
            {
                if (friend.Name == i_FriendsName)
                {
                    urlToReturn = friend.PictureNormalURL;
                    break;
                }
            }

            return urlToReturn;
        }

        public string GetSelectedEvent(User i_FaceBookUser, string i_selectedEvent)
        {
            string urlToReturn = null;
            foreach (Event anEvent in i_FaceBookUser.Events)
            {
                if (anEvent.Name == i_selectedEvent)
                {
                    urlToReturn = anEvent.PictureNormalURL;
                    break;
                }
            }

            return urlToReturn;
        }

        public string GetSelectedCheckIn(User i_FaceBookUser, string i_selectedCheckIn)
        {
            string urlToReturn = null;
            foreach (Checkin location in i_FaceBookUser.Checkins)
            {
                if (location.Name == i_selectedCheckIn)
                {
                    urlToReturn = location.PictureURL;
                    break;
                }
            }

            return urlToReturn;
        }

        public string GetUserAge(User i_FaceBookUser)
        {
            IAgeCalculator getUserAgeProxy = new UserAgeProxy();
            (getUserAgeProxy as UserAgeProxy).TheUsersAge = new UserAge(i_FaceBookUser);
            return getUserAgeProxy.OnCalcAndGetAge();
        }

        public void PostToPages(User i_FaceBookUser, string o_Post, List<string> i_PagesToPostTo)
        {
            int pagesToPostCounter = 0;
            CompositePages likedPagesToPost = new CompositePages();

            foreach (PageWrapper likedPage in i_FaceBookUser.LikedPages)
            {
                if (likedPage.Name == i_PagesToPostTo[pagesToPostCounter])
                {
                    likedPagesToPost.Pages.Add(likedPage);
                    pagesToPostCounter++;
                }
            }

            likedPagesToPost.OnPostOnPage(o_Post);
        }

        internal List<string> FetchSortedPages(string i_SortType)
        {
            List<string> sortedPagesToReturn = new List<string>();
            switch(i_SortType)
            {
                case "sortByLikesCountRadioButton":
                    i_SortType = "LikesCount";
                    m_PageSorter = new PageSorterByLikes();
                    break;
                case "sortByCheckinsCountRadioButton":
                    i_SortType = "CheckinsCount";
                    m_PageSorter = new PageSorterByCheckins();
                    break;
                case "sortByTalkingAboutRadioButton":
                    i_SortType = ("TalkAboutCount");
                    m_PageSorter = new PageSorterByTalkingAbout();
                    break;
            }

            m_PageSorter.Sort(m_UserLoggedIn.LikedPages.ToList());

            foreach (Page page in m_UserLoggedIn.LikedPages.ToList())
            {
                sortedPagesToReturn.Add(page.Name);
            }

            return sortedPagesToReturn;
        }
    }
}
