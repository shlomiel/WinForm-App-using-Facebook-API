using FacebookWrapper.ObjectModel;

namespace DP_20C_OmriShlomi_Logic
{
    public class UserAge : IAgeCalculator
    {
        private User m_UserToGetAge;

        public UserAge(User i_TheUser)
        {
            m_UserToGetAge = i_TheUser;
        }

        public string OnCalcAndGetAge()
        {
            return m_UserToGetAge.Birthday;
        }
    }
}
