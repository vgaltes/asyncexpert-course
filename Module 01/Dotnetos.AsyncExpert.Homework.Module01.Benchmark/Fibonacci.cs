using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    public class FibonacciCalc
    {
        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add memory profiler to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            var t = new Dictionary<ulong, ulong>();

            ulong FibCached(ulong n)
            {
                if (t.ContainsKey(n)) return t[n];

                if (n <= 2) return 1;

                var result = FibCached(n - 2) + FibCached(n - 1);

                t.Add(n, result);

                return result;
            }

            return FibCached(n);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            ulong a = 0, b = 1, c = 0;
            for (ulong i = 2; i < n; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }

            return c;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
