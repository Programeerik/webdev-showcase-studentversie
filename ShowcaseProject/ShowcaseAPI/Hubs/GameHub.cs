using Microsoft.AspNetCore.SignalR;

namespace ShowcaseAPI.Hubs
{
    public class GameHub : Hub<IGameHub>
    {

        private static Dictionary<string, List<string>> _group = new Dictionary<string, List<string>>();
        private static int _turnCounter;
        private static List<char> _board = new List<char>();

        public async Task CreateGroup(string name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, name);

            if(!_group.ContainsKey(name))
            {
                _group[name] = new List<string>();
            }

            _group[name].Add(Context.ConnectionId);

            await Clients.Caller.GroupMade(name ,_group[name]);
            await Clients.Caller.ShowUserList(_group[name]);
        }

        public async Task JoinGroup(string name)
        {

            if (!_group.ContainsKey(name))
            {
                _group[name] = new List<string>();
            }

            if (_group[name].Count >= 2 && !(_group[name].Contains(Context.ConnectionId)))
            {
                await Clients.Caller.GroupFull(name);
                return;
            }else if(_group[name].Count >= 2 && (_group[name].Contains(Context.ConnectionId)))
            {
                await Clients.Group(name).ShowUserList(_group[name]);
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, name);

            _group[name].Add(Context.ConnectionId);

            await Clients.Group(name).JoinedGroup(name, _group[name]);
            await Clients.Caller.ShowUserList(_group[name]);

        }

        public async Task StartGame(string name)
        {
            await Clients.Group(name).RemoveLobby();
            await Clients.Caller.GameStarted(name,'X');
            await Clients.Others.GameStarted(name,'O');
            _board = new List<char> { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
            _turnCounter = 0;
        }

        public async Task SendMove(string name, char playerSymbol, int position)
        {

            if (playerSymbol.Equals('X') && ((_turnCounter % 2) == 0))
            {
                await Clients.Group(name).UpdateBoard(playerSymbol, position);
                if (_board[position].Equals(' '))
                {
                    _board[position] = playerSymbol;
                    _turnCounter++;
                }
            }
            else if(playerSymbol.Equals('O') && ((_turnCounter % 2) == 1))
            {
                await Clients.Group(name).UpdateBoard(playerSymbol, position);
                if (_board[position].Equals(' '))
                {
                    _board[position] = playerSymbol;
                    _turnCounter++;
                }
            }
            else
            {
                await Clients.Caller.NotYourTurnMessage("Het is niet jouw beurt, wacht op je tegenstander.");
            }

            if(!_board.Contains(' '))
            {
                await Clients.Group(name).GameOver("Het spel is afgelopen, helaas geen winnaar!");
            }

            if (checkHorizontal(playerSymbol))
            {
                await Clients.Group(name).GameWon(playerSymbol);
            }
            else if (checkVertical(playerSymbol))
            {
                await Clients.Group(name).GameWon(playerSymbol);
            }
            else if (checkDiagonal(playerSymbol))
            {
                await Clients.Group(name).GameWon(playerSymbol);
            }
        }

        private bool checkHorizontal(char playerSymbol)
        {
            if ((_board[0] == playerSymbol && _board[1] == playerSymbol && _board[2] == playerSymbol) || (_board[3] == playerSymbol && _board[4] == playerSymbol && _board[5] == playerSymbol || (_board[6] == playerSymbol && _board[7] == playerSymbol && _board[8] == playerSymbol)))
            {
                return true;
            }
            return false;
        }

        private bool checkVertical(char playerSymbol)
        {
            if ((_board[0] == playerSymbol && _board[3] == playerSymbol && _board[6] == playerSymbol) || (_board[1] == playerSymbol && _board[4] == playerSymbol && _board[7] == playerSymbol || (_board[2] == playerSymbol && _board[5] == playerSymbol && _board[8] == playerSymbol)))
            {
                return true;
            }
            return false;
        }

        private bool checkDiagonal(char playerSymbol)
        {
            if ((_board[0] == playerSymbol && _board[4] == playerSymbol && _board[8] == playerSymbol) || (_board[2] == playerSymbol && _board[4] == playerSymbol && _board[6] == playerSymbol))
            {
                return true;
            }
            return false;
        }

    }
}
