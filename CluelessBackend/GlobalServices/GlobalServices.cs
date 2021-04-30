namespace CluelessBackend
{
    public static class GlobalServiceInitializer
    {
        public static GameInstanceService GameInstanceService;
        public static ChatService ChatService;
        public static void InitializeServices()
        {
            GameInstanceService = new GameInstanceService();
            ChatService = new ChatService(GameInstanceService);
        }
    }
}