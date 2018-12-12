using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Export data logic
/// </summary>
namespace EBC_Manager
{
    class Exporter
    {
        public Exporter()
        {
        }

        /// <summary>
        /// Method to export Meeting Rooms into CSV file.
        /// </summary>
        /// <param name="meetingCentres"></param>
        public void exportCSV(List<MeetingCentre> meetingCentres)
        {
            var csv = new StringBuilder();
            var newLine = string.Format("MEETING_CENTRES,,,,,");
            csv.AppendLine(newLine);

            //process centres
            foreach(MeetingCentre centre in meetingCentres)
            {
                newLine = string.Format("{0},{1},{2},,,", centre.centreName, centre.centreCode, centre.centreDescription.Replace("\n", String.Empty));
                csv.AppendLine(newLine);
            }

            newLine = string.Format("MEETING_ROOMS,,,,,");
            csv.AppendLine(newLine);

            //process Rooms
            List<MeetingRoom> rooms = collectRooms(meetingCentres);
            foreach(MeetingRoom room in rooms)
            {
                string video = room.roomVideo == true ? "YES" : "NO";

                newLine = string.Format("{0},{1},{2},{3},{4},{5}", room.roomName, room.roomCode, room.roomDescription, room.roomCapacity, video, room.centreCode);
                csv.AppendLine(newLine);
            }
            File.WriteAllText("export.csv", csv.ToString());
        }

        /// <summary>
        /// Collects meeting room from each centre for export needs.
        /// </summary>
        /// <param name="centres"></param>
        /// <returns></returns>
        private List<MeetingRoom> collectRooms(List<MeetingCentre> centres)
        {
            List<MeetingRoom> roomsToExport = new List<MeetingRoom>();
            foreach(MeetingCentre centre in centres)
            {
                if (centre.meetingRoomsList.Any<MeetingRoom>())
                {
                    foreach (MeetingRoom room in centre.meetingRoomsList)
                    {
                        roomsToExport.Add(room);
                    }
                }

            }

            return roomsToExport;
        }
    }
}
