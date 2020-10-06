using Grpc.Core;
using Grpc.Net.Client;
using System;

namespace Api.Factories
{
    public class GrpcChannelFactory<T>: IDisposable where T : ClientBase<T>
    {
        private readonly GrpcChannel _grpcChannel;

        public GrpcChannelFactory(string address)
        {
            _grpcChannel = GrpcChannel.ForAddress(address);
        }
        public T CreateChannel()
        {          
           return (T)Activator.CreateInstance(typeof(T),new object[] { _grpcChannel });
        }

        public void Dispose()
        {
            _grpcChannel.ShutdownAsync();
            _grpcChannel.Dispose();
        }
    }
}
