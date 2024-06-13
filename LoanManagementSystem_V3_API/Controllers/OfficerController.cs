using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.Repository;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Authorization;


namespace LoanManagementSystem_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = "Bearer")]
    public class OfficerController : ControllerBase
    {
        // this is the controller for Officer Module
        //-------------------------------------------

        // so we need to get the instance of IOfficer Repository Through constructor injection 
        private readonly IOfficerRepository _repository;

        public OfficerController(IOfficerRepository repository)
        {
            _repository = repository;
        }


        //-------------------------------------------



        #region Get Details of All Assigned Varification 

        [HttpGet("Details/{staff_id}")]
        public async Task<ActionResult<IEnumerable<vw_VerificationDetails>>> GetDetailsToVerify(int staff_id)
        {

            if(_repository !=null)
            {
                return await _repository.GetDetailsToVerify(staff_id);
            }

            return new List<vw_VerificationDetails>();
            // returning an empty List if Something went wrong.
        }

        #endregion


        #region Submit a Verification Report 

        // this method is responsible to Submit the verification form 
        [HttpPost("Report")]
        public async Task<ActionResult<int>>SubmitVerificationReport(vw_Dropdown report)
        {
            if(_repository!=null)
            {
                return await _repository.SubmitVerificationReport(report);
            }
            return 0;
        }

        #endregion


    }
}
