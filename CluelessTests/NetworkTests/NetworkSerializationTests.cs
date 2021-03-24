using System.IO;
using CluelessNetwork;
using CluelessNetwork.NetworkSerialization;
using FluentAssertions;
using Xunit;

namespace CluelessTests.NetworkTests
{
    public class NetworkSerializationTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestSendConnectionInfo(bool isHost)
        {
            var connectionInfo = new InitialConnectionInfo {IsHost = isHost, Name = string.Empty};
            using var stream = new MemoryStream();
            stream.WriteObject(connectionInfo);
            stream.Seek(0, SeekOrigin.Begin);
            var subject = stream.ReadObject<InitialConnectionInfo>();
            // Test equality
            subject.Should().Be(connectionInfo);
            // Test that they are really separate instances (reference equality)
            subject.Should().NotBeSameAs(connectionInfo);
        }
    }
}