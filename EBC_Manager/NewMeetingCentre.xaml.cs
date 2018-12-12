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
    /// Interakční logika pro NewMeetingCentre.xaml
    /// </summary>
    public partial class NewMeetingCentre : Window
    {
        public NewMeetingCentre()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handle cancel button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelCentreCreationButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Handle createCentreButtonClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createCentreButton_Click(object sender, RoutedEventArgs e)
        {
            string description = new TextRange(centreDescriptionText.Document.ContentStart, centreDescriptionText.Document.ContentEnd).Text;
            InputTester checker = new InputTester();

            //Checkiung input
            if (checker.ensureName(centreNameBox.Text) && checker.ensureCode(centreCodeBox.Text) && checker.ensureDescription(description))
            {
                MeetingCentre newMeetingCentre = new MeetingCentre(centreNameBox.Text, centreCodeBox.Text, description);
                Globals.meetingCentresList.Add(newMeetingCentre);
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
    }
}
