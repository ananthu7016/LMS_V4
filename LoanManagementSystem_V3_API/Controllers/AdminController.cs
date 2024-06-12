using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanManagementSystem_WebApi.Repository;
using LoanManagementSystem_V3_API.ViewModel;
using LoanManagementSystem_V3_API.Model;

namespace LoanManagementSystem_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        // first we need to get the instance of IAdminRepository through DI
        private readonly IAdminRepository _repository;

        public AdminController(IAdminRepository repository)
        {
            _repository = repository;
        }
        //--------------------------------------------------



        #region Add a New User 

        [HttpPost("NewStaff")]
        public async Task<ActionResult<int>> RegisterNewStaff(User user)
        {
            if (_repository != null && user != null)
                return await _repository.RegisterNewStaff(user);
            else
                return 0;
        }

        #endregion




        #region Get all Log Details 

        [HttpGet("Logs")]
        public async Task<ActionResult<IEnumerable<vw_LogDetails>>> GetAllLogDetails()
        {
            if (_repository != null)
                return await _repository.GetAllLogDetails();
            else
                return new List<vw_LogDetails>();
        }

        #endregion
    }
}
