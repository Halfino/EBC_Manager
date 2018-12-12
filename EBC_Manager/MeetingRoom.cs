using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Meeting room class
/// </summary>
namespace EBC_Manager
{
    class MeetingRoom
    {
        public string roomName { get; set; }
        public string roomCode { get; set; }
        public string roomDescription { get; set; }
        public int roomCapacity { get; set; }
        public bool roomVideo { get; set; }
        public string centreCode { get; set; }

        public MeetingRoom(string name, string code, string description, int capacity, bool video, string cCode)
        {
            this.roomName = name;
            this.roomCode = code;
            this.roomDescription = description;
            this.roomCapacity = capacity;
            this.roomVideo = video;
            this.centreCode = cCode;
        }
    }
}
