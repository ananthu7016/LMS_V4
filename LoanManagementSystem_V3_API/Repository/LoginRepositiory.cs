using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LoanManagementSystem_V3_API.Repository
{
    public class LoginRepositiory : ILoginRepository
    {
        // this is the implementation of The ILogin Interface 




        // first we need to get access to the Database context throgh the instance which is created by  DI

        // defining a private readonly instance to store the instance 
        private readonly LmsV3DbContext _context;

        public LoginRepositiory(LmsV3DbContext context)
        {
            _context = context;
        }

        //--------------------------------------------------------------


        #region Validate Staff using the credential provided by the staff 


        public async Task<User> ValidateUser(string username, string password)
        {
            if(_context != null)
            {
                // defining an instance of user to store the details of the user with the password and username 
                User user = new User();
                bool isUserExist = false;

                try
                {
                    user = await _context.Users.Where(u => u.UserName == username && u.Password == password).FirstAsync();
                    // this will return a user if the user exist 
                    isUserExist = true;
                }
                catch (Exception ex) { }

                if(isUserExist)
                {
                    // we need to retrn the user 
                    return user;
                }
               
            }

            return new User();
            // if no userexist we will return empty Instance 
        }

        #endregion



        #region Validate Customer using Credentials Provided 
        public async Task<Customer> ValidateCustomer(string username, string password)
        {
            Customer customer = new Customer();
            customer.CustId = 0;
            // this is to store the instance of the customer created
            if (_context !=null)
            {

                try
                {
                    customer = await _context.Customers.Where(c => c.UserName == username && c.Password == password).FirstAsync();
                }
                catch(Exception ex) { }

                // then we need to retrun the customer 

                return customer;
            }

           return customer;
            // retruning and empty instace if something went wrong;
        }

        #endregion



    }
}
