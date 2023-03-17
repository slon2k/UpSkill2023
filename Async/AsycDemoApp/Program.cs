internal class Program
{
    private static async Task Main(string[] args)
    {
        var cancellationTokenSource = new CancellationTokenSource();

        var token = cancellationTokenSource.Token;

        Task countTask = CountAsync(5, token);

        ConsoleKey key;

        while ((key = Console.ReadKey().Key) != ConsoleKey.Escape )
        {
            Console.WriteLine(" key is pressed");
        };

        cancellationTokenSource.Cancel();

        await countTask;

        Console.WriteLine("The end");
    }

    async static Task CountAsync(int count, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Counting started");

        do
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Counting cancelled");

                return;
            }

            await Task.Delay(1000);
            
            Console.WriteLine(count);
        } while (count-- > 0);

        Console.WriteLine("Counting finished");
    }
}