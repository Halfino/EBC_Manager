using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EBC_Manager
{
    /// <summary>
    /// Interakční logika pro NewRoom.xaml
    /// </summary>
    public partial class NewRoom : Window
    {
        private object test { get; set; }
        public NewRoom()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with centre param
        /// </summary>
        /// <param name="centre"></param>
        public NewRoom(object centre)
        {
            InitializeComponent();
            test = centre;
        }

        /// <summary>
        /// Handle Create button click. Should check user input and redirect me back to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createRoomButton_Click(object sender, RoutedEventArgs e)
        {
            MeetingCentre cent = (MeetingCentre)test;
            InputTester checker = new InputTester();
            int capacity = 0;

            int.TryParse(roomCapacity.Text, out capacity);
            string description = new TextRange(roomDescription.Document.ContentStart, roomDescription.Document.ContentEnd).Text;

            //checking user input
            if(checker.ensureName(roomName.Text) && checker.ensureCode(roomCode.Text) && checker.ensureDescription(description) && checker.ensureCapacity(capacity))
            {
                cent.meetingRoomsList.Add(new MeetingRoom(roomName.Text, roomCode.Text, description, capacity, (bool)videoCheck.IsChecked, cent.centreCode));
                Globals.needSave = true;
                MainWindow newMain = new MainWindow();
                newMain.Show();
                this.Close();
            }
            else
            {
                string message = "Bad data format:" +
                    Environment.NewLine + "Name – text in 2 - 100 characters." +
                    Environment.NewLine + "Code - unique code length 5 to 50, can contain numbers, letters a-z and A-Z, also special characters (. : - _)" +
                    Environment.NewLine + "Description - length 10 - 300 characters" +
                    Environment.NewLine + "Capacity - integer 1 to 100 if requested";
                MessageBox.Show(message);
            }

        }

        /// <summary>
        /// Handle cancel button click. SHould redirect to main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMain = new MainWindow();
            newMain.Show();
            this.Close();
        }
    }
}
