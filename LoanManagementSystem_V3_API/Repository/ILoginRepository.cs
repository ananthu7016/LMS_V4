using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystem_V3_API.Repository
{
    public interface ILoginRepository
    {
        // this is the Interface for the Login Repository here we need to declare the methods which should be implemented 


        #region Validate Staff using Credentials Provided 

        Task<User> ValidateUser(string username, string password);

        #endregion


        #region Validate Customer using Credentials Provided 
        Task<Customer> ValidateCustomer(string username, string password);

        #endregion



    }
}
