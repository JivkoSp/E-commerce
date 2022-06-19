using BooksPlace.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Static
{
    public static class TrackUsers
    {
        private static ConcurrentDictionary<string, DateTime> activeUsers;
        private static ConcurrentDictionary<int, Dictionary<string, DateTime>> loggedUsersHistory;

        static TrackUsers()
        {
            activeUsers = new ConcurrentDictionary<string, DateTime>();
            loggedUsersHistory = new ConcurrentDictionary<int, Dictionary<string, DateTime>>();
        }

        public static void UpdateUserAccess(string userId)
        {
            activeUsers.AddOrUpdate(userId, DateTime.Now, (k, v) => DateTime.Now);
            loggedUsersHistory.TryAdd(DateTime.Now.Minute, GetActiveUsers());
        }

        public static Dictionary<string, DateTime> GetActiveUsers(int timeOut=10)
        {
            DateTime now = DateTime.Now;

            return activeUsers.Where(u => u.Value.AddMinutes(timeOut) >= DateTime.Now)
                    .ToDictionary(u => u.Key, u => u.Value);
        }

        public static ConcurrentDictionary<int, Dictionary<string, DateTime>> GetActiveUserHistory()
        {
            return loggedUsersHistory;
        }
    }
}
