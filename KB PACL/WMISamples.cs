using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;


namespace KB_PACL
{
    class WMISamples
    {
        private static void getRegistrySample()
        {
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey("Volatile Environment\\1");
            if (rkey != null)
            {
                Object o = rkey.GetValue("SESSIONNAME");
                Console.WriteLine(o);
            }
            else
            {
                Console.WriteLine("rkey is null.");
            }
        }

        private static void WMICount()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT PortName FROM Win32_Printer");
            Console.WriteLine(searcher.Get().Count);
            Console.WriteLine(searcher.Get());
        }

        private static void WMISample1()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Printer");
            //String res = string.Empty;
            String[] result = new String[searcher.Get().Count];
            int i = 0;
            foreach (ManagementObject queryObj in searcher.Get())
            {
               // res += queryObj["PortName"].ToString();
                result[i] = queryObj["PortName"].ToString();
                Console.WriteLine(result[i]);
                i++;
            }
            //Console.WriteLine(res);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("getRegisty----------");
            getRegistrySample();
            Console.WriteLine("WMICount----------");
            WMICount();
            Console.WriteLine("WMISample1----------");
            WMISample1();
        }

        
    }
}
