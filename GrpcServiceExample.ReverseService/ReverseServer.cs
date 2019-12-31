using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServiceExample.ReverseService.ServiceImplementation;
using GrpcService.Generated;
using CommonProto;

namespace GrpcServiceExample.ReverseService
{
    public class ReverseServer
    {
        private readonly Server _server;

        public ReverseServer()
        {
            _server = new Server()
            {
                Services = { RevService.BindService(new ReverseServiceImplementation()) },
                Ports = { new ServerPort(Common.HOST, Common.CHANNEL, ServerCredentials.Insecure)},
            };
        }   //  ReverseServer()

        public void Start()
        {
            _server.Start();
        }   //  Start()

        public async Task ShutDownAsync()
        {
            await _server.ShutdownAsync();
        }   //  ShutDownAsync()
    }   //  class ReverseServer
}   //  namespace GrpcServiceExample.ReverseService
