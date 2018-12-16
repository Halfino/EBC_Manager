using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EBC_Manager
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handle exit click from menu.If there are unsaved changes, popup window appears to ask for saving. If clicked cancel, app is exitted without saving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitClick(object sender, RoutedEventArgs e)
        {
            if (Globals.needSave)
            {
                MessageBoxResult myResult;
                myResult = MessageBox.Show("Save data before exit?", "Save Confirmation", MessageBoxButton.OKCancel);
                if (myResult == MessageBoxResult.OK)
                {
                    saveClick(sender, e);
                }
            }
            Application.Current.Shutdown();

            
        }

        /// <summary>
        /// Handles data import click from menu. Opens file selection and parses selected file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            string fileName = "";
            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
                
            }
            Globals.meetingCentresList.RemoveAll(mc => mc.centreName != null);
            Importer import = new Importer();
            import.parseCSV(fileName);
            MeetingCentres.Items.Refresh();
            Globals.needSave = true;
        }

        /// <summary>
        /// Saves data into export.csv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveClick(object sender, RoutedEventArgs e)
        {
            Exporter export = new Exporter();
            export.exportCSV(Globals.meetingCentresList);
            Globals.needSave = false;
        }

        /// <summary>
        /// Init method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridInit(object sender, EventArgs e)
        {
            MeetingCentres.ItemsSource = Globals.meetingCentresList;

        }

        /// <summary>
        /// Handles click new meeting centre. Opens new window with form for new centre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickNewMeetingCentre(object sender, RoutedEventArgs e)
        {
            NewMeetingCentre newCentreWindow = new NewMeetingCentre();
            newCentreWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Handles selecting data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeSelectionCentre(object sender, SelectionChangedEventArgs e)
        {
            MeetingCentre selected = (MeetingCentre) MeetingCentres.SelectedItem;
            try
            {
                editCentreName.Text = selected.centreName;
                editCentreCode.Text = selected.centreCode;
                editCentreDescription.Text = selected.centreDescription;
                MeetingRooms.ItemsSource = selected.meetingRoomsList;
            }
            catch(Exception ex)
            {
                editCentreName.Text = "";
                editCentreCode.Text = "";
                editCentreDescription.Text = "";
            }
        }

        /// <summary>
        /// handles edit centre click.Checks all inputs and update data if pass. otherwise popup message window w2ith general info about data format.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMeetingCentreButton_Click(object sender, RoutedEventArgs e)
        {
            if(MeetingCentres.SelectedItem != null)
            {
                InputTester checker = new InputTester();
                //checking inputs
                if (checker.ensureName(editCentreName.Text) && checker.ensureCode(editCentreCode.Text) && checker.ensureDescription(editCentreDescription.Text))
                {
                    MeetingCentre selected = (MeetingCentre)MeetingCentres.SelectedItem;
                    selected.centreName = editCentreName.Text;
                    selected.centreCode = editCentreCode.Text;
                    selected.centreDescription = editCentreDescription.Text;
                    Globals.needSave = true;
                    MeetingCentres.Items.Refresh();
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
            else
            {
                MessageBox.Show("Select Meeting Centre first!");
            }

        }

        /// <summary>
        /// Takes care about deleting selected items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteMeetingCentreButton_Click(object sender, RoutedEventArgs e)
        {
            if(MeetingCentres.SelectedItem != null)
            {
                MeetingCentre selected = (MeetingCentre)MeetingCentres.SelectedItem;
                MessageBoxResult myResult;
                myResult = MessageBox.Show("Really Delete " + selected.centreName + " ?", "Delete Confirmation", MessageBoxButton.OKCancel);
                if (myResult == MessageBoxResult.OK)
                {
                    Globals.meetingCentresList.Remove(selected);
                    Globals.needSave = true;
                    MeetingCentres.Items.Refresh();
                    MeetingRooms.ItemsSource = null;
                    MeetingRooms.Items.Clear();
                }
            }
            else
            {
                MessageBox.Show("Select Meeting Centre first!");
            }
        }

        /// <summary>
        /// Handles meeting rooms selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MeetingRoom selected = (MeetingRoom)MeetingRooms.SelectedItem;
            try
            {
                editRoomName.Text = selected.roomName;
                editRoomCode.Text = selected.roomCode;
                editRoomDescription.Text = selected.roomDescription;
                editRoomCapacity.Text = selected.roomCapacity.ToString();
                editVideo.IsChecked = selected.roomVideo;
            }
            catch (Exception ex)
            {
                editRoomName.Text = "";
                editRoomCode.Text = "";
                editRoomDescription.Text = "";
                editRoomCapacity.Text = "";
                editVideo.IsChecked = false;
            }
        }

        /// <summary>
        /// Handle new room click - Opens new window with new room form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if(MeetingCentres.SelectedItem != null)
            {
                NewRoom newRoom = new NewRoom(MeetingCentres.SelectedItem);
                newRoom.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Meeting Centre first!");
            }
        }

        /// <summary>
        /// method to handle deleting room. works only if room is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (MeetingRooms.SelectedItem != null)
            {
                MeetingRoom selected = (MeetingRoom)MeetingRooms.SelectedItem;
                MessageBoxResult myResult;
                myResult = MessageBox.Show("Confirm deleting " + selected.roomName + " ?", "Delete Confirmation", MessageBoxButton.OKCancel);
                if (myResult == MessageBoxResult.OK)
                {
                    MeetingCentre selectedCentre = (MeetingCentre)MeetingCentres.SelectedItem;
                    selectedCentre.meetingRoomsList.Remove(selected);
                    Globals.needSave = true;
                    MeetingRooms.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Select Meeting Room first!");

            }

        }

        /// <summary>
        /// Method to handle edit room button, works only if room is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if(MeetingRooms.SelectedItem != null)
            {
                MeetingRoom selected = (MeetingRoom)MeetingRooms.SelectedItem;
                InputTester checker = new InputTester();
                int capacity = 0;
                int.TryParse(editRoomCapacity.Text, out capacity);

                //ensure correct inputs
                if (checker.ensureName(editRoomName.Text) &&
                    checker.ensureCode(editRoomCode.Text) &&
                    checker.ensureDescription(editRoomDescription.Text) &&
                    checker.ensureCapacity(capacity))
                {
                    selected.roomName = editRoomName.Text;
                    selected.roomCode = editRoomCode.Text;
                    selected.roomDescription = editRoomDescription.Text;
                    selected.roomCapacity = capacity;
                    selected.roomVideo = (bool)editVideo.IsChecked;
                    Globals.needSave = true;
                    MeetingRooms.Items.Refresh();
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
            else
            {
                MessageBox.Show("Select Meeting Room first!");
            }

        }

        private void CentreSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MeetingCentre selected = (MeetingCentre)CentreSelector.SelectedItem;
            RoomSelector.ItemsSource = selected.meetingRoomsList;
        }

        private void MeetingDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = (DateTime) meetingDatePicker.SelectedDate;
            List<Reservation> selectedReservations = new List<Reservation>();
            MeetingCentre selectedCentre = (MeetingCentre)CentreSelector.SelectedItem;
            MeetingRoom selectedRoom = (MeetingRoom)RoomSelector.SelectedItem;
            
            foreach(Reservation reservation in selectedRoom.reservationsList)
            {
                if(reservation.meetingDate == selectedDate)
                {
                    selectedReservations.Add(reservation);
                }
            }

            meetingsBox.ItemsSource = selectedReservations;
        }

        private void TestData_Click(object sender, RoutedEventArgs e)
        {
            MeetingRoom selectedRoom = (MeetingRoom)RoomSelector.SelectedItem;
            DateTime date = new DateTime(2018, 12, 12);
            TimeSpan start = new TimeSpan(10, 00, 00);
            TimeSpan end = new TimeSpan(11, 00, 00);
            Reservation res1 = new Reservation(date, start, end, 1, "Jan Koranda", false, "nic", selectedRoom.roomCode);
            selectedRoom.reservationsList.Add(res1);
            Reservation added = selectedRoom.reservationsList.First();
            testLabel.Content = selectedRoom.reservationsList.Count;
            
        }
    }
}
