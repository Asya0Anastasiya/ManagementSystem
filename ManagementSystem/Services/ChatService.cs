namespace UserService.Services
{
    public class ChatService
    {
        private static readonly Dictionary<string, string> _users = new();

         public bool AddUserToList(string userToAdd)
         {
            lock (_users)
            {
                foreach (var user in _users)
                {
                    if (user.Key.ToLower() == userToAdd.ToLower())
                    {
                        return false;
                    }
                }

                _users.Add(userToAdd, null);
                return true;
            }
         }

        public void AddUserConnectionId(string username, string connectionId)
        {
            lock ( _users)
            {
                if (_users.ContainsKey(username))
                {
                    _users[username] = connectionId;
                }
            }
        }

        public string GetUserByConnectionId(string connectionId)
        {
            lock(_users)
            {
                return _users.Where(x => x.Value == connectionId).Select(x => x.Key).FirstOrDefault();
            }
        }

        public string GetConnectionIdByUser(string username)
        {
            lock (_users)
            {
                return _users.Where(x => x.Key == username).Select(x => x.Value).FirstOrDefault();
            }
        }

        public void RemoveUserFromList(string username)
        {
            lock (_users)
            {
                if (_users.ContainsKey(username))
                {
                    _users.Remove(username);
                }
            }
        }

        public string[] GetOnlineUsers()
        {
            lock(_users)
            {
                return _users.OrderBy(x => x.Key).Select(x => x.Key).ToArray();
            }
        }
    }
}
