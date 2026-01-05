using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MemCleaner
{
    internal static class MemoryCleaner
    {
        private const int SystemMemoryListInformation = 80;

        public static void CleanCurrentProcess()
        {
            NativeMethods.EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }

        public static void CleanAllProcesses()
        {
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    NativeMethods.EmptyWorkingSet(process.Handle);
                }
                catch { }
            }
        }

        public static void ClearSystemCache()
        {
            const int command = 4;
            IntPtr ptr = Marshal.AllocHGlobal(sizeof(int));
            Marshal.WriteInt32(ptr, command);

            int status = NativeMethods.NtSetSystemInformation(
                SystemMemoryListInformation, ptr, sizeof(int));

            if (status != 0)
                Console.WriteLine($"Falha ao limpar cache. NTSTATUS: 0x{status:X}");

            Marshal.FreeHGlobal(ptr);
        }

        public static void ShowMemoryStatus()
        {
            var mem = new NativeMethods.MEMORYSTATUSEX();
            mem.dwLength = (uint)Marshal.SizeOf(mem);

            NativeMethods.GlobalMemoryStatusEx(ref mem);

            Console.WriteLine($"Uso de memória: {mem.dwMemoryLoad}%");
            Console.WriteLine($"RAM Total: {mem.ullTotalPhys / 1024 / 1024} MB");
            Console.WriteLine($"RAM Livre: {mem.ullAvailPhys / 1024 / 1024} MB");
        }
    }
}