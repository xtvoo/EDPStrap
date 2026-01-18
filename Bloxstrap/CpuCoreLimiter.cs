using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Bloxstrap
{
    public static class CpuCoreLimiter
    {
        /// <summary>
        /// Limits the current process to use only the specified number of CPU cores.
        /// </summary>
        /// <param name="coreCount">Number of CPU cores to allow (minimum 1, maximum number of logical processors).</param>
        public static void SetCpuCoreLimit(int coreCount)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return;

            string processName = App.RobloxPlayerAppName.Split('.')[0];
            Process[] processes = Process.GetProcessesByName(processName);
            
            if (processingHyperCore() || coreCount > 0)
            {
                 // Logic handled inside helper or below
            }
            
            // Helper to handle Hyper Core
            bool processingHyperCore()
            {
                 if (App.Settings.Prop.HyperCoreThreading)
                 {
                     foreach(var p in processes)
                     {
                         try { 
                             p.PriorityClass = ProcessPriorityClass.High; 
                             p.ProcessorAffinity = (IntPtr)((1L << Environment.ProcessorCount) - 1); // All cores
                             App.Logger.WriteLine("CpuCoreLimiter", $"Applied Hyper Core (High Priority + All Cores) to {p.Id}");
                         } catch {}
                     }
                     return true;
                 }
                 return false;
            }

            if (processingHyperCore()) return;

            int maxCores = Environment.ProcessorCount;
            if (coreCount <= 0 || coreCount > maxCores) return;

            long affinityMask = 0;
            for (int i = 0; i < coreCount; i++) affinityMask |= 1L << i;

            foreach(var p in processes)
            {
                try {
                     p.ProcessorAffinity = (IntPtr)affinityMask;
                     App.Logger.WriteLine("CpuCoreLimiter", $"Set affinity for {p.Id} to {coreCount} cores.");
                } catch (Exception ex) {
                     App.Logger.WriteLine("CpuCoreLimiter", $"Failed to set affinity: {ex.Message}");
                }
            }
        }
    }
}
