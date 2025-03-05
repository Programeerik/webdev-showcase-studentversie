namespace ShowcaseAPI.Hubs
{
    public interface IGameHub
    {
        Task GroupMade(string name, List<string> userIds);

        Task JoinedGroup(string name,List<string> userIds);

        Task GroupFull(string name);

        Task ShowUserList(List<string> userIds);

        Task RemoveLobby();

        Task GameStarted(string name, char playerSymbol);

        Task UpdateBoard(char playerSymbol, int position);

        Task NotYourTurnMessage(string message);
        
        Task GameOver(string message);

        Task GameWon(char playerSymbol);

    }
}
