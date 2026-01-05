using MemCleaner;
using System;

public static class Program
{
    static void Main()
    {
        Console.WriteLine("Status antes da limpeza:");
        MemoryCleaner.ShowMemoryStatus();

        Console.WriteLine("\nLimpando memória do sistema...");
        MemoryCleaner.ClearSystemCache();

        Console.WriteLine("Limpando processos...");
        MemoryCleaner.CleanAllProcesses();

        Console.WriteLine("\nStatus após limpeza:");
        MemoryCleaner.ShowMemoryStatus();

        Console.WriteLine("\nConcluído.");
        Console.ReadKey();
    }
}