using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using FacebookWrapper.ObjectModel;
using DP_20C_OmriShlomi_Logic;

namespace DP_20C_OmriShlomi_UI
{
    public partial class MainForm : Form
    {
        private AppLogic m_AppSettings;

        public List<IObserverControl> ControlsToNotify { get; set; }

        public void Attach(IObserverControl i_ObserverToAttach)
        {
            if (i_ObserverToAttach != null)
            {
                ControlsToNotify.Add(i_ObserverToAttach);
            }
        }

        public void Detach(IObserverControl i_ObserverToAttach)
        {
            ControlsToNotify.Remove(i_ObserverToAttach);
        }

        private void notify()
        {
            foreach (IObserverControl controlObserver in ControlsToNotify)
            {
                controlObserver.Update();
            }
        }

        public MainForm()
        {
            m_AppSettings = new AppLogic();
            InitializeComponent();
            ControlsToNotify = new List<IObserverControl>();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            m_AppSettings.LoginAndInitiate();
            bindDataSources();
            loginAndInitiate();
        }

        private void bindDataSources()
        {
            userBindingSource.DataSource = m_AppSettings.TheLoggedInUser;
        }

        private void loginAndInitiate()
        {
            try
            {
                if (m_AppSettings.TheLoggedInUser == null)
                {
                    MessageBox.Show("Login Failed");
                }
                else
                {
                    string textToPresent = "Hello " + m_AppSettings.TheLoggedInUser.FirstName;
                    AccessToken.Invoke(new Action(() => AccessToken.Text = textToPresent));
                    profilePicBox.Load(m_AppSettings.TheLoggedInUser.PictureLargeURL);
                    loadCoverPhoto();
                    chooseProfileActionGroupBox.Enabled = true;
                    foreach (TabPage tabPage in this.RadioButtonOne.TabPages)
                    {
                        foreach (Control controlInForm in tabPage.Controls)
                        {
                            Attach(new ControlObserver(controlInForm));
                        }
                    }

                    notify();
                }
            }
            catch
            {
                MessageBox.Show("Login Failed");
            }
        }

        private void loadCoverPhoto()
        {
            string coverPhotoUrl = m_AppSettings.GetCoverPhoto();
            if (coverPhotoUrl != null)
            {
                coverFeaturePicBox.Invoke(new Action(() => coverFeaturePicBox.Load(coverPhotoUrl)));
            }
            else
            {
                coverFeaturePicBox.Invoke(new Action(() => coverFeaturePicBox.Image = profilePicBox.Image));
            }
        }

        private void birthdayButton_Click(object sender, EventArgs e)
        {
            try
            {
                birthdayTextBox.Text = m_AppSettings.TheLoggedInUser.Birthday;
            }
            catch
            {
                MessageBox.Show("Failed to retrieve birthday :(");
            }
        }

        private void showEmailButton_Click(object sender, EventArgs e)
        {
            try
            {
                emailTextBox.Text = m_AppSettings.TheLoggedInUser.Email;
            }
            catch
            {
                MessageBox.Show("Failed to retrieve email :(");
            }
        }

        private void showFriendsListButoon_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxFriends.Items.Count > 0)
                {
                    listBoxFriends.Items.Clear();
                }

                List<string> friendFetched = m_AppSettings.FetchFriends();
                if (friendFetched != null)
                {
                    foreach (string friend in friendFetched)
                    {
                        listBoxFriends.Items.Add(friend);
                    }
                }
                else
                {
                    MessageBox.Show("No friends retrieve :(");
                }
            }
            catch
            {
                MessageBox.Show("Failed retrieveing friends :(");
            }
        }

        private void likedPagesButon_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBoxLikedPages.Items.Count > 0)
                {
                    checkedListBoxLikedPages.Items.Clear();
                }

                List<string> likedPagesFetched = m_AppSettings.FetchPages();
                if (likedPagesFetched != null)
                {
                    foreach (string page in likedPagesFetched)
                    {
                        checkedListBoxLikedPages.Items.Add(page);
                    }
                }
                else
                {
                    MessageBox.Show("No liked pages to retrieve :(");
                }
            }
            catch
            {
                MessageBox.Show("fetching liked pages failed");
            }
        }

        private void getCheckIns(ListBox listBoxToFill)
        {
            try
            {
                List<string> checkInsFetched = m_AppSettings.FetchCheckIns();

                if (listBoxCheckins.Items.Count > 0)
                {
                    listBoxCheckins.Items.Clear();
                }

                if (checkInsFetched != null)
                {
                    foreach (string checkIn in checkInsFetched)
                    {
                        listBoxCheckins.Invoke(new Action(() => listBoxToFill.Items.Add(checkIn)));
                    }
                }
                else
                {
                    MessageBox.Show("No Check-ins to retrieve :(");
                }
            }
            catch
            {
                MessageBox.Show("Failed to retrieve Check-ins :(");
            }
        }

        private void checkInButton_Click(object sender, EventArgs e)
        {
            new Thread(() => getCheckIns(listBoxCheckins)).Start();
        }

        private void showEventsListButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> userEvents = m_AppSettings.FetchEvents();
                if (listBoxFriends.Items.Count > 0)
                {
                    listBoxEvents.Items.Clear();
                }

                if (userEvents != null)
                {
                    foreach (string userEvent in userEvents)
                    {
                        listBoxEvents.Items.Add(userEvent);
                    }
                }
                else
                {
                    MessageBox.Show("No Events to retrieve :(");
                }
            }
            catch
            {
                MessageBox.Show("Failed to retrieve events :(");
            }
        }

        private void friendsList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            displaySelectedFriend();
        }

        private void displaySelectedFriend()
        {
            try
            {
                if (listBoxFriends.SelectedItems.Count == 1)
                {
                    string selectedFriend = listBoxFriends.SelectedItem.ToString();

                    if (m_AppSettings.GetSelectedFriend(selectedFriend) != null)
                    {
                        pictureBoxFriend.LoadAsync(m_AppSettings.GetSelectedFriend(selectedFriend));
                    }
                    else
                    {
                        pictureBoxFriend.Image = pictureBoxFriend.ErrorImage;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Faied to retrive friend`s profile photo :(");
            }
        }

        private void listBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            displaySelectedEvent();
        }

        private void displaySelectedEvent()
        {
            try
            {
                if (listBoxEvents.SelectedItems.Count == 1)
                {
                    string selectedEvent = listBoxEvents.SelectedItems.ToString();

                    if (m_AppSettings.GetSelectedEvent(selectedEvent) != null)
                    {
                        pictureBoxEvents.LoadAsync(m_AppSettings.GetSelectedEvent(selectedEvent));
                    }
                    else
                    {
                        pictureBoxEvents.Image = pictureBoxEvents.ErrorImage;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Faied to retrive event photo :(");
            }
        }

        private void listBoxCheckins_SelectedIndexChanged(object sender, EventArgs e)
        {
            displaySelectedCheckin();
        }

        private void displaySelectedCheckin()
        {
            try
            {
                if (listBoxCheckins.SelectedItems.Count == 1)
                {
                    string selectedCheckIn = listBoxCheckins.SelectedItems.ToString();

                    if (m_AppSettings.GetSelectedCheckIn(selectedCheckIn) != null)
                    {
                        pictureBoxCheckins.LoadAsync(m_AppSettings.GetSelectedCheckIn(selectedCheckIn));
                    }
                    else
                    {
                        pictureBoxCheckins.Image = pictureBoxCheckins.ErrorImage;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Faied to retrive checkin loaction photo :(");
            }
        }

        private void postStatusButton_Click(object sender, EventArgs e)
        {
            try
            {
                Status postedStatus = m_AppSettings.SentStatusPost(textBoxStatus.Text);
                if (postedStatus != null)
                {
                    MessageBox.Show("Status Posted! ID: " + postedStatus.Id);
                }
                else
                {
                    MessageBox.Show("Failed to post status");
                    textBoxStatus.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Failed to post status");
            }
        }

        private void homeTownButton_Click(object sender, EventArgs e)
        {
            try
            {
                startingPointTextBox.Text = m_AppSettings.FetchHomeTown();
            }
            catch
            {
                MessageBox.Show("Failed fetching hometown :(");
            }
        }

        private void showCheckInsDestenationsButton_Click(object sender, EventArgs e)
        {
            new Thread(() => getCheckIns(listBoxCheckinsDestenations)).Start();
        }

        private void listBoxCheckinsDestenations_SelectedIndexChanged(object sender, EventArgs e)
        {
            destenationTextBox.Text = listBoxCheckinsDestenations.SelectedItem as string;
        }

        private void startTripButton_Click(object sender, EventArgs e)
        {
            string commuteSelection = null;
            foreach (RadioButton radioButton in commuteGroupBox.Controls)
            {
                if (radioButton.Checked == true)
                {
                    commuteSelection = radioButton.Text;
                }
            }

            string travelRootGoogleUrl = m_AppSettings.GetTripUrl(startingPointTextBox.Text, destenationTextBox.Text, commuteSelection);
            System.Diagnostics.Process.Start(travelRootGoogleUrl);
        }

        private void activatePhotoButton_Click(object sender, EventArgs e)
        {
            coverFeaturePicBox.Show();
            picFeature.BackgroundImage = null;
            foreach (RadioButton radioButton in chooseProfileActionGroupBox.Controls)
            {
                if (radioButton.Checked == true)
                {
                    switch (radioButton.Name)
                    {
                        case "makeCoverRadioButton":
                            coverFeaturePicBox.Hide();
                            picFeature.BackgroundImage = coverFeaturePicBox.Image;
                            break;
                        case "makePhotoDanceRadioButton":
                            loadCoverPhoto();
                            picDanceTimerLeft.Enabled = true;
                            picDanceTimerRight.Enabled = true;
                            break;
                        case "rotatePhotoRadioButton":
                            coverFeaturePicBox.Image = m_AppSettings.RotateImage(coverFeaturePicBox.Image as Bitmap);
                            break;
                        case "blackAndWhiteRadioButton":
                            loadCoverPhoto();
                            Image bAndW = m_AppSettings.TurnImageBlackAndWhite(coverFeaturePicBox.Image);
                            coverFeaturePicBox.Image = bAndW;
                            break;
                    }
                }
            }
        }

        private void picDanceTimerLeft_Tick(object sender, EventArgs e)
        {
            Point picLocation = new Point(coverFeaturePicBox.Location.X, coverFeaturePicBox.Location.Y);
            picLocation.X -= 20;
            coverFeaturePicBox.Location = picLocation;
            System.Threading.Thread.Sleep(100);
            picLocation.Y += 15;
            coverFeaturePicBox.Location = picLocation;
            System.Threading.Thread.Sleep(100);
            picLocation.X += 20;
            coverFeaturePicBox.Location = picLocation;
            System.Threading.Thread.Sleep(100);
            picLocation.Y -= 15;
            coverFeaturePicBox.Location = picLocation;
            System.Threading.Thread.Sleep(100);
            picDanceTimerLeft.Enabled = false;
        }

        private void picDanceTimerRight_Tick(object sender, EventArgs e)
        {
            Point picLocation = new Point(coverFeaturePicBox.Location.X, coverFeaturePicBox.Location.Y);
            picLocation.X += 20;
            coverFeaturePicBox.Location = picLocation;
            System.Threading.Thread.Sleep(100);
            picLocation.Y -= 15;
            coverFeaturePicBox.Location = picLocation;
            System.Threading.Thread.Sleep(100);
            picLocation.X -= 20;
            coverFeaturePicBox.Location = picLocation;
            System.Threading.Thread.Sleep(100);
            picLocation.Y += 15;
            coverFeaturePicBox.Location = picLocation;
            System.Threading.Thread.Sleep(100);
            picDanceTimerRight.Enabled = false;
        }

        private void ageRangeButoon_Click(object sender, EventArgs e)
        {
            ageRangeTextBox.Text = m_AppSettings.GetUserAge();
        }

        private void showHomeTownButton_Click(object sender, EventArgs e)
        {
            try
            {
                hometownTextBox.Text = m_AppSettings.FetchHomeTown();
            }
            catch
            {
                MessageBox.Show("Failed fetching hometown :(");
            }
        }

        private enum eImageType
        {
            Jpeg = 1,
            Bmp,
            Gif
        }

        private void savePhotoButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveDialog.Title = "Save an Image File";
            saveDialog.ShowDialog();

            if (saveDialog.FileName != string.Empty)
            {
                System.IO.FileStream fileStream = (System.IO.FileStream)saveDialog.OpenFile();

                switch (saveDialog.FilterIndex)
                {
                    case (int)eImageType.Jpeg:
                        this.coverFeaturePicBox.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case (int)eImageType.Bmp:
                        this.coverFeaturePicBox.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case (int)eImageType.Gif:
                        this.coverFeaturePicBox.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fileStream.Close();
            }
        }

        private void checkedListBoxLikedPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxLikedPages.SelectedItem != null)
            {
                postOnLikedPagesButon.Enabled = true;
            }
        }

        private void postOnLikedPagesButon_Click(object sender, EventArgs e)
        {
            List<string> checkedPages = new List<string>();
            if (checkedListBoxLikedPages.CheckedItems.Count == 0)
            {
                MessageBox.Show("No pages has chosen");
            }
            else if (textBoxStatusForPages.Text.Length == 0)
            {
                MessageBox.Show("No text to post");
            }
            else
            {
                try
                {
                    foreach (CheckBox item in checkedListBoxLikedPages.CheckedItems)
                    {
                        checkedPages.Add(item.Text);
                    }

                    m_AppSettings.PostToPages(textBoxStatusForPages.Text, checkedPages);
                }
                catch
                {
                    MessageBox.Show("Post on chosen pages has failed");
                }
            }
        }

        private void aboutTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                m_AppSettings.SetUserAbout(this.Text);
            }
            catch
            {
                MessageBox.Show("Failed updating about info :(");
            }
        }

        private void sortPagesButton_Click(object sender, EventArgs e)
        {
            List<string> sortedPages = new List<string>();

            try
            {
                foreach (RadioButton radioButton in pageSortGroupBox.Controls)
                {
                    if (radioButton.Checked == true)
                    {
                        switch (radioButton.Name)
                        {
                            case "sortByLikesCountRadioButton":
                                sortedPages = m_AppSettings.FetchSortedPages("LikesCount");
                                break;
                            case "sortByCheckinsCountRadioButton":
                                sortedPages = m_AppSettings.FetchSortedPages("CheckinsCount");
                                break;
                            case "sortByTalkingAboutRadioButton":
                                sortedPages = m_AppSettings.FetchSortedPages("TalkAboutCount");
                                break;
                        }

                        if (sortedPages != null)
                        {
                            checkedListBoxLikedPages.Items.Clear();
                            foreach (string page in sortedPages)
                            {
                                checkedListBoxLikedPages.Items.Add(page);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("fetching liked pages failed");
            }
        }
    }
}
