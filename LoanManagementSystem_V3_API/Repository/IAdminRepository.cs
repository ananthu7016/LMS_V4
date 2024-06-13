using LoanManagementSystem_V3_API.ViewModel;
using LoanManagementSystem_V3_API.Model;

using Microsoft.AspNetCore.Mvc;
namespace LoanManagementSystem_V3_API
{
    public interface IAdminRepository
    {
        // this is the Interface for the IAdmin Repository 



        #region Add a New Staff 

        Task<ActionResult<int>> RegisterNewStaff(User user);

        #endregion



        #region Get all Log Details 

        Task<ActionResult<IEnumerable<vw_LogDetails>>> GetAllLogDetails();

        #endregion
    }
}
