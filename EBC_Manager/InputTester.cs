using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/// <summary>
/// Logic to test user input
/// </summary>
namespace EBC_Manager
{
    class InputTester
    {
        public InputTester()
        {

        }

        /// <summary>
        /// Checking name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Boolean ensureName(string name)
        {
            // Name – název budovy(řetězec o délce 2..100 znaků).
            if (Enumerable.Range(2, 100).Contains(name.Length))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// checking code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Boolean ensureCode(string code)
        {
            //Code – unikátní identifikátor budovy (řetězec o délce 5..50 znaků, který může obsahovat velká a malá písmena anglické abecedy,
            //číslice a znaky: tečka, pomlčka, dvojtečka a podtržítko).
            Regex codePattern = new Regex(@"[a-zA-z\d\.\-\:_]");
            if (codePattern.IsMatch(code) && Enumerable.Range(5, 50).Contains(code.Length) && findCode(code) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// checking description
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public Boolean ensureDescription(string description)
        {
            //Description – slovní popis budovy (řetězec o délce 10..300 znaků).
            if (Enumerable.Range(10, 300).Contains(description.Length))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checking capacity
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public Boolean ensureCapacity(int capacity)
        {
            //Capacity – maximální počet osob, který se pohodlně vejde do místnosti (celé číslo v rozsahu 1..100).
            if (Enumerable.Range(1, 100).Contains(capacity))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Helper method for code checking. Returns false if code is unique
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Boolean findCode(string code)
        {
            if(Globals.meetingCentresList.Find(mc => mc.centreCode == code) == null)
            {
                bool codeInRooms = false;
                foreach (MeetingCentre mc in Globals.meetingCentresList)
                {
                    if (mc.meetingRoomsList.Find(mr => mr.roomCode == code) != null)
                    {
                        return true;
                    }
                    
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
