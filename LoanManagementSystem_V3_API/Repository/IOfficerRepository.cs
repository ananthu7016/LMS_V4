using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystem_V3_API.Repository
{
    public interface IOfficerRepository
    {

        // this is the interface for Officer Repository 


        #region Get Details of All Assigned Varification 

        Task<ActionResult<IEnumerable<vw_VerificationDetails>>> GetDetailsToVerify(int staff_id);

        #endregion


        #region Submit a Verification Report 

        // this method is responsible to Submit the verification form 
        Task<ActionResult<int>> SubmitVerificationReport(vw_Dropdown report);


        #endregion


    }
}
