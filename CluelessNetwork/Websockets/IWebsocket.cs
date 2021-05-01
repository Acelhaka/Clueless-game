using System;

namespace CluelessNetwork.Websockets
{
    public interface IWebsocket : IDisposable
    {
        public void WriteObject<T>(T objectToWrite);
        public T? ReadObject<T>() where T : class;
    }
}