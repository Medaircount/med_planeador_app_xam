using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneScanning.Guards
{
    public class Guardian
    {
        public bool CanAccessPage()
        {
            try
            {
                string result = Preferences.Get("userStorage", string.Empty);
             if (String.IsNullOrEmpty(result))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return false;
            }
        }

        public void Clean() {
            try
            {
                Preferences.Remove("userStorage");
                Preferences.Remove("userId");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
    }
}
