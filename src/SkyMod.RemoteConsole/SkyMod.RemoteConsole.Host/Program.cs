namespace SkyMod.RemoteConsole.Host
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Program
    {
        private static HttpListener httpListener;
        private static ManualResetEvent finishedEvent = new ManualResetEvent(false);

        internal static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://+:44324/skymod-remotecon/");
            httpListener.Start();

            Task.Run(async () =>
                {
                    while (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        var requestContext = await httpListener.GetContextAsync();

                        switch (requestContext.Request.QueryString["level"])
                        {
                            case "error":
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;

                            case "warn":
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;

                            default:
                                Console.ResetColor();
                                break;
                        }

                        using (var streamReader = new StreamReader(requestContext.Request.InputStream))
                        {
                            Console.WriteLine("[{0}] {1}", DateTime.Now, await streamReader.ReadToEndAsync());
                        }
                    }

                    finishedEvent.Set();
                }, cancellationTokenSource.Token);

            Console.WriteLine("Press ENTER to close the host.");
            Console.ReadLine();

            cancellationTokenSource.Cancel();
            Console.WriteLine("Waiting to close...");

            finishedEvent.WaitOne();
        }
    }
}
