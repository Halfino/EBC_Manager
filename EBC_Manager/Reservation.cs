using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBC_Manager
{
    class Reservation
    {
        public DateTime meetingDate { get; set; }
        public TimeSpan meetingStartTime { get; set; }
        public TimeSpan meetingEndTime { get; set; }
        public int expectedPersonCount { get; set; }
        public string customer { get; set; }
        public bool videoConference { get; set; }
        public string note { get; set; }
        public string roomCode { get; set; }

        public Reservation(DateTime date, TimeSpan startTime, TimeSpan endTime, int personCount, string roomCustomer, bool needConference, 
            string reservationNote, string reservationRoomCode)
        {
            this.meetingDate = date;
            this.meetingStartTime = startTime;
            this.meetingEndTime = endTime;
            this.expectedPersonCount = personCount;
            this.customer = roomCustomer;
            this.videoConference = needConference;
            this.note = reservationNote;
            this.roomCode = reservationRoomCode;
        }
    }
}
