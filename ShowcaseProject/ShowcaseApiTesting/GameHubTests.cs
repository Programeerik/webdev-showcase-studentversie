namespace ShowcaseApiTesting
{
    using Moq;
    using ShowcaseAPI.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    [TestFixture]
    public class GameHubTests
    {
        [Test]
        public async Task CreateGroup()
        {

            Mock<IHubCallerClients<IGameHub>> _mockClients = new Mock<IHubCallerClients<IGameHub>>();
            Mock<IGameHub> _mockClientProxy = new Mock<IGameHub>();
            Mock<IGroupManager> _mockGroups = new Mock<IGroupManager>();
            Mock<HubCallerContext> _mockContext = new Mock<HubCallerContext>();

            GameHub _hub = new GameHub
            {
                Clients = _mockClients.Object,
                Groups = _mockGroups.Object,
                Context = _mockContext.Object
            };

            _mockClients.Setup(clients => clients.Caller).Returns(_mockClientProxy.Object);


            var groupName = "123456"; 
            var connectionId = "test-connection";
            _mockContext.Setup(c => c.ConnectionId).Returns(connectionId);

            await _hub.CreateGroup(groupName);

            _mockGroups.Verify(g => g.AddToGroupAsync(connectionId, groupName, default), Times.Once);
        }
    }
}