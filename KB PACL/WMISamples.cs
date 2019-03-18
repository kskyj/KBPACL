using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Printing;
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
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Printer");
            Console.WriteLine(searcher.Get().Count);
            ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT PortName FROM Win32_Printer");
            Console.WriteLine(searcher2.Get().Count);
            ManagementObjectSearcher searcher3 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Printer WHERE PortName ='test1'");
            Console.WriteLine(searcher3.Get().Count);
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
        private static void WMISample2()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_TCPIPPrinterPort");
            String[] result = new String[searcher.Get().Count];
            int i = 0;
            foreach (ManagementObject queryObj in searcher.Get())
            {
                result[i] = queryObj["Name"].ToString();
                Console.WriteLine(result[i]);
                i++;
            }
            //Console.WriteLine(res);
        }
        private static void CsharpAPI()
        {
            var ps = new PrintServer();
            var queues = ps.GetPrintQueues();
            var pq = queues.Where(t => t.FullName.Contains("Epson")).First();
            if (pq == null) { 
                return;
            }
            Console.WriteLine(pq);
            pq.Refresh();
            var jobs = pq.GetPrintJobInfoCollection();
            Console.WriteLine(jobs);
            foreach (var job in jobs)
            {
                job.Cancel();
            }
        }
    static void Main(string[] args)
        {
            while (true)
           {
                Console.WriteLine("getRegisty----------");
                getRegistrySample();
                Console.WriteLine("WMICount----------");
                WMICount();
                Console.WriteLine("WMISample1----------");
                WMISample1();
                Console.WriteLine("WMISample2----------");
                WMISample2();

                //WIN32 말고 C# API를 이용하자.
                CsharpAPI();


                // 마지막에 항상 추가, 빌드방법 Windows Application 으로 변경
                GC.Collect();
            }
        }

        
    }
}
