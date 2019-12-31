using System;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcService.Generated;
using CommonProto;


namespace GrpcServiceExample.Consume
{
    class Client
    {
        static void Main(string[] args)
        {
            var reverseString1 = ReverseAsync().GetAwaiter().GetResult();
            var reverseString2 = Reverse();

            Console.WriteLine(reverseString1);
            Console.WriteLine(reverseString2);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }   //  Main()

        static async Task<string> ReverseAsync()
        {
            Channel channel = new Channel(Common.HOST, Common.CHANNEL, ChannelCredentials.Insecure);

            RevService.RevServiceClient client = new RevService.RevServiceClient(channel);

            var data = new Data() { Str = "test" };
            var res = await client.ReverseAsync(data);

            return res.Str;
        }   //  ReverseAsync()

        static string Reverse()
        {
            Channel channel = new Channel(Common.HOST, Common.CHANNEL, ChannelCredentials.Insecure);

            RevService.RevServiceClient client = new RevService.RevServiceClient(channel);

            var data = new Data() { Str = "test" };

            return client.Reverse(data).Str;
        }   //  Reverse()
    }   //  class Client
}   //  namespace GrpcServiceExample.Consume
