using System;
using System.Collections.Concurrent;
using System.Diagnostics;


var lockTotal = new object();

var primesList = new List<long>();
var primesBag = new ConcurrentBag<long>();
long count = 0;
long total = 0;


//var task = Task.Run(() => Parallel.Invoke(
//    () => Console.WriteLine("1"),
//    () => Console.WriteLine("2"),
//    () => Console.WriteLine("3")
//));

//await task;


// ForEach Loop


Stopwatch stopwatch = Stopwatch.StartNew();

foreach (var num in Range(1, 10_000_000))
{
    if (IsPrime(num))
    {
        primesList.Add(num);
        count++;
        total += num;
    }
}

stopwatch.Stop();

PrintResults("Sequential", count, total, stopwatch);


// Parallel ForEach Loop

primesBag.Clear();
total = 0;
count = 0;

var cancellationTokenSource = new CancellationTokenSource();
cancellationTokenSource.CancelAfter(1000);

var options = new ParallelOptions
{
    CancellationToken = cancellationTokenSource.Token
};

stopwatch.Restart();

try
{
    Parallel.ForEach(Range(1, 10_000_000), options, i =>
    {
        if (IsPrime(i))
        {
            primesBag.Add(i);
            Interlocked.Increment(ref count);
            Interlocked.Add(ref total, i);
        }
    });
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation canceled");
}

stopwatch.Stop();

PrintResults("Parallel ForEach", count, total, stopwatch);


// Parallel For Loop

primesBag.Clear();
total = 0;
count = 0;

stopwatch.Restart();

Parallel.For(1, 10_000_000, i =>
{
    if (IsPrime(i))
    {
        primesBag.Add(i);
        
        lock (lockTotal)
        {
            count++;
            total += i;
        }
    }
});

stopwatch.Stop();
PrintResults("Parallel For", count, total, stopwatch);


// PLINQ

primesBag.Clear();
total = 0;
count = 0;

stopwatch.Restart();

var result = Range(1, 10_000_000)
    .AsParallel()
    .AsOrdered()
    .Where(IsPrime);

result.ForAll(i => {
    primesBag.Add(i);
    Interlocked.Increment(ref count);
    Interlocked.Add(ref total, i);
});

stopwatch.Stop();

PrintResults("PLINQ", count, total, stopwatch);

Console.WriteLine(string.Join(", ", result.Take(20)));


Console.WriteLine("Press a key to quit");
Console.ReadKey();

static IEnumerable<long> Range(long from, long to)
{
    while (from < to)
    {
        yield return from++;
    }
}

static bool IsPrime(long number)
{
    if (number == 1)
    {
        return false;
    }

    long i = 2;

    while (i * i <= number)
    {
        if (number % i == 0)
        {
            return false;
        }

        i++;
    }

    return true;
}

static void PrintResults(string title, long count, long total, Stopwatch stopwatch)
{
    Console.WriteLine($"# {title}");
    Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds}");
    Console.WriteLine($"Count: {count}");
    Console.WriteLine($"Total: {total}");
    Console.WriteLine();
}