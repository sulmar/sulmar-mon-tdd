using System.Threading.Tasks;

namespace TestApp.Mocking
{

    public interface IMessageClient
    {
        Task SendAsync(User sender, User recipient, SalesReport report);
    }





}