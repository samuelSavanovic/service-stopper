using System;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using System.IO;

namespace ServiceStopper
{
    class ServiceStopper { 
        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();
        public StreamWriter sw;
        public ServiceController[] scServices;

        public ServiceStopper() {
            FreeConsole();
            string path = @"C:\Program Files (x86)\Service Stopper\log.txt";
            sw = File.AppendText(path);
            
           stopServices();
        }
        void stopCurrentService(ServiceController sc) {
            if(sc.Status == ServiceControllerStatus.Running) {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                sw.WriteLine("{0}, {1}", sc.DisplayName, sc.Status.ToString());
            }
        }
        void stopServices () {
            
            while (true) {
                scServices = ServiceController.GetServices();

                foreach (ServiceController sc in scServices)
                {
                    if (sc.DisplayName == "Windows Update") {
                        stopCurrentService(sc);
                    }
                    if (sc.DisplayName == "Background Intelligent Transfer Service") {
                        stopCurrentService(sc);
                    }
                    if (sc.DisplayName == "Delivery Optimization") {
                        stopCurrentService(sc);
                    }
                    if (sc.DisplayName == "Update Orchestrator Service") {
                        stopCurrentService(sc);
                    }
                }
                scServices = null;
                sw.Flush();
                System.Threading.Thread.Sleep(5000);
            }
        }

        static void Main() 
        { 
           new ServiceStopper();
            
        } 
    }
}
