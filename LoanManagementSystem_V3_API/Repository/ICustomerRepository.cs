using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystem_V3_API.Repository
{
    public interface ICustomerRepository
    {

        // This is the Interface of Customer Repository here we neeed to declare the methods that are to be implemented in the 
        //child class 


        #region Register a New Customer 

        Task<ActionResult<int>> RegisterNewCustomer(Customer customer);
        // the declaration of this method is responsible to add details of a new customer.

        #endregion



        #region Get Status Updates of Loan Applied by customer

        Task<ActionResult<IEnumerable<vw_LoanStatusUpdate>>> GetStatusUpdateOfLoans(int custId);

        #endregion


        #region Get Details of All Available Loans

        Task<ActionResult<IEnumerable<LoanType>>> GetDetailsOfAllLoans();

        #endregion


        #region Apply for a Loan 

        Task<ActionResult<int>> ApplyForLoan(LoanRequest loan);

        #endregion


        #region Check Loan Eligibility for a Loan 

        Task<ActionResult<IEnumerable<LoanType>>> GetEligibleLoans(vw_CheckEligibility condition);

        #endregion

    }
}
