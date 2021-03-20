using System.Diagnostics;

namespace CluelessNetwork
{
    public static class Settings
    {
        public static readonly bool PrintNetworkDebugMessagesToConsole = Debugger.IsAttached;
    }
}