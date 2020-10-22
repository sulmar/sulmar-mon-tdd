using System.Collections.Generic;

namespace TestApp.Mocking
{
    public interface IUserService
    {
        IEnumerable<User> GetBossesRecipients();
        IEnumerable<User> Get(UserSearchCriteria criteria);
        User GetBot();
    }





}