using System;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcService.Generated;
using CommonProto;


namespace GrpcServiceExample.Consume
{
    class ConsumerClient
    {
        static void Main(string[] args)
        {
            ConsumerClient client = new ConsumerClient();

            var reverseString1 = client.ReverseAsync().GetAwaiter().GetResult();
            var reverseString2 = client.Reverse();

            Console.WriteLine(reverseString1);
            Console.WriteLine(reverseString2);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Get Handpiece Types:");
            Console.WriteLine("");

            client.GetHandpieces().GetAwaiter().GetResult();

            Console.WriteLine("");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();


        }   //  Main()

        public ConsumerClient()
        {
            _channel = new Channel(Common.HOST, Common.CHANNEL, ChannelCredentials.Insecure);
            _client = new RevService.RevServiceClient(_channel);
        }   //  ConsumerClient()

        #region Rever String Calls  ===============================================================
        private async Task<string> ReverseAsync()
        {
            var data = new Data() { Str = "test" };
            var res = await _client.ReverseAsync(data);

            return res.Str;
        }   //  ReverseAsync()

        private string Reverse() => _client.Reverse(new Data() { Str = "test" }).Str;
        #endregion  Reverse String Calls

        #region Handpiece Type Calls    ===========================================================
        private async Task GetHandpieces()
        {
            using (var result = _client.GetHandpieceTypes(new RequestHandpieces()))
            {
                while (await result.ResponseStream.MoveNext())
                {
                    var handpiece = result.ResponseStream.Current;
                    Console.WriteLine($"\tHandpiece -- {handpiece.Gauge} Gauge");
                }
            }   //  using()
        }   //  GetHandpieces()
        #endregion  Handpiece Type Calls

        #region Private Properties  ===============================================================
        Channel _channel = null;
        RevService.RevServiceClient _client = null;
        #endregion  Private Properties
    }   //  class ConsumerClient
}   //  namespace GrpcServiceExample.Consume
