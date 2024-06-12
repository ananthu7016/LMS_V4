using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystem_V3_API.Repository
{
    public interface IManagerRepository
    {




        #region Get Details of Loans Applied By Every Customer

        Task<ActionResult<IEnumerable<vw_LoanRequest>>> GetAllRequestedLoans();


        #endregion


        #region Get Details of All Loan Officers

        Task<ActionResult<IEnumerable<vw_Dropdown>>> GetDetailsOfOfficers();

        #endregion


        #region Assign a Officer for Verification

        Task<ActionResult<int>> AssignVerificationOfficer(LoanVerification detail);

        #endregion


        #region Get all Details of Verified Loans for Approval

        Task<ActionResult<IEnumerable<vw_ApprovalDetails>>> GetDetailsOfLoanToApprove();

        #endregion


        #region Approve a Loan 
        Task<ActionResult<int>> ApproveALoan(vw_ApprovalDetails loan);

        #endregion


        #region Reject a Loan 

        Task<ActionResult<int>> RejectALoan(vw_ApprovalDetails loan);

        #endregion


        #region Add Details of a new  Loan 

        Task<ActionResult<int>> AddNewLoanDetails(LoanType loan);

        #endregion


        #region Disable or enable a Loan status

        Task<ActionResult<int>> ToggleLoanStatus(int loan_id);

        #endregion



    }
}
