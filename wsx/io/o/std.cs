using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace wsx.io.o
{
    public class std
    {
        public static bool _rec = true;
        public static bool _prnt = true;

        public class ActionWrite
        { 
            public DateTime Time { get; set; }
            public string UserName { get; set; }
            public string Output { get; set; }
        }

        public static List<ActionWrite> WriteHistory = [];


        public static bool print(string message)
        {
            if (_rec)
            {
                Record(message);
            }

            if (_prnt)
            {
                Console.Write(message);
            }


            return true;
        }
        public static bool println(string message)
        {
            if (_rec)
            {
                Record(message);
            }

            if (_prnt)
            {
                Console.WriteLine(message);
            }


            return true;
        }

        public static bool Record(string Message)
        {
            WriteHistory.Add(new ActionWrite
            {
                Time = DateTime.Now,
                UserName = wsx.mem.temp.Sess.__User__.CurrentUserName,
                Output = Message
            });
            //SaveActionWrites();
            return true;
        }
        private bool SaveActionWrites()
        {
            //to do
            return true;
        }
    }
}
