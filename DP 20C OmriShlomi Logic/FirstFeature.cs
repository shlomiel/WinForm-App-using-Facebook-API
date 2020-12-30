namespace DP_20C_OmriShlomi_Logic
{
    internal class FirstFeature
    {
        private static string s_GoogleUrl = "https://www.google.com/maps/dir/?api=1&origin={0}&destination={1}&travelmode={2}";

        public string GetTripUrl(string i_StartingPoint, string i_Destenation, string i_CommuteType)
        {
            i_StartingPoint.Replace(' ', '+');
            i_Destenation.Replace(' ', '+');
            return string.Format(s_GoogleUrl, i_StartingPoint, i_Destenation, i_CommuteType);
        }
    }
}