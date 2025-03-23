//namespace RestWithASPNET.FrameWrkDrivers.Extensions;
//{
//    public static class PollyExtensions
//    {
//        public static AsyncRetryPolicy<HttpResponseMessage> WaitTry()
//        {
//            var retryWaitPolicy = HttpPolicyExtensions
//                .HandleTransientHttpError()
//                .WaitAndRetryAsync(new[]
//                {
//                    TimeSpan.FromSeconds(1),
//                    TimeSpan.FromSeconds(2),
//                    TimeSpan.FromSeconds(3),
//                }, (outcome, timespan, retryCount, context) =>
//                {
//                    Console.ForegroundColor = ConsoleColor.Blue;
//                    Console.WriteLine($"Tentando pela {retryCount} vez!");
//                    Console.ForegroundColor = ConsoleColor.White;
//                });

//            return retryWaitPolicy;
//        }
//    }
//}