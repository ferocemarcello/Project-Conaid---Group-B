using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class Command
    {
        [DataMember]
        public string Action { get; set; }

        public delegate bool D();
        public Command(string action)
        {
            Action = action;
        }
        public bool Perform()
        {
            if(Action == "Perform")
            {
                throw new Exception("eh eh eh!");
            }
            else
            {
                MethodInfo methodToPerform = typeof(Command).GetMethod(Action, BindingFlags.Public | BindingFlags.Instance);
                D d;
                d = (D)Delegate.CreateDelegate(typeof(D), this, methodToPerform);
                bool result = d();
                return result;
            }
        }

        public bool start_scan()
        {
            Console.WriteLine("started scanning ...");
            return true;
        }

        public bool stop_scan()
        {
            Console.WriteLine("stopped scanning ...");
            return true;
        }

        public bool dance()
        {
            Console.WriteLine("dancing ...");
            return true;
        }

    }
}
