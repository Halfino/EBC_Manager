using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/// <summary>
/// Meeting Centre class
/// </summary>
namespace EBC_Manager
{
    class MeetingCentre
    {
        public string centreName { get; set; }
        public string centreCode { get; set; }
        public string centreDescription { get; set; }
        public List<MeetingRoom> meetingRoomsList { get; set; }

        public MeetingCentre (string name, string code, string description)
        {
            this.centreName = name;
            this.centreCode = code;
            this.centreDescription = description;
            this.meetingRoomsList = new List<MeetingRoom>();
        }
    }
}
