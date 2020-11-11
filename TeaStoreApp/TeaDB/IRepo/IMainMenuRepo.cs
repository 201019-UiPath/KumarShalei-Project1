using TeaDB.Models;
namespace TeaDB.IRepo
{
    public interface IMainMenuRepo
    {
        /// <summary>
        /// Business Logic concerning our MainMenu 
        /// </summary>
        void NewCustomer(CustomerModel customer);
        CustomerModel GetCustomerInfo(string email);        
    }
}