using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
/// <summary>
/// Import data logic
/// </summary>
namespace EBC_Manager
{
    class Importer
    {
        public Importer()
        {

        }

        /// <summary>
        /// Imports CSV into app data.
        /// </summary>
        /// <param name="file"></param>
        public void parseCSV(string file)
        {
            try
            {
                using (var reader = new StreamReader(@file))
                {
                    List<string> centreList = new List<string>();
                    List<string> roomList = new List<string>();
                    bool centre = true;
                    bool isFirst = true;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        //skip First Line
                        if (isFirst)
                        {
                            isFirst = false;
                            continue;
                        }

                        //skip MEETING_ROOMS line
                        if (values[0] == "MEETING_ROOMS")
                        {
                            centre = false;
                            continue;
                        }

                        //Nevim proc, nicmene obcas mi importer tendenci vyrtvaret prazdny objekt centra (prazdne stringy) a toto tomu zamezi. Byl bych rad, kdybych se dovedel, proc se tak deje
                        if (values[0] == "")
                        {
                            continue;
                        }

                        //Porcess meeting centres.
                        if (centre)
                        {
                            MeetingCentre newCentre = new MeetingCentre(values[0], values[1], values[2]);
                            Globals.meetingCentresList.Add(newCentre);
                        }
                        //process meeting rooms
                        else
                        {
                            MeetingCentre thisCentre = Globals.meetingCentresList.Find(mc => mc.centreCode == values[5]);
                            int capacity = 0;
                            int.TryParse(values[3], out capacity);
                            bool hasVideo = false;
                            if (values[4] == "YES")
                            {
                                hasVideo = true;
                            }
                            MeetingRoom newRoom = new MeetingRoom(values[0], values[1], values[2], capacity, hasVideo, values[5]);
                            thisCentre.meetingRoomsList.Add(newRoom);
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }
    }
}
