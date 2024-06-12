using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanManagementSystem_V3_API.Repository;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using LoanManagementSystem_V3_API.Model;

namespace LoanManagementSystem_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        //This is the Controller for the Customer Functionalities 

        //first we need to get the instance of the ICustomer Repository through DI 

        //-------------------------------
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        //-------------------------------


        #region Register a New Customer 

        [HttpPost("Register")]
        public async Task<ActionResult<int>> RegisterNewCustomer(Customer customer)
        {
            if(_repository !=null)
            {
                return await _repository.RegisterNewCustomer(customer);
            }

            return 0;
        }

        #endregion


        #region Get Details Of All Loans Taken By a Customer
     //   [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{custId}")]
        public async Task<ActionResult<IEnumerable<vw_LoanDetailsOfCustomer>>> GetAllLoansOfCustomer(int custId)
        {
            if(_repository !=null)
            {
                return await _repository.GetAllLoansOfCustomer(custId);
            }
            else
            {
                return null;
            }
        }

        #endregion


        #region Get Details of All Available Loans
      //  [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("Loans")]
        public async Task<ActionResult<IEnumerable<LoanType>>> GetDetailsOfAllLoans()
        {
            if (_repository != null)
                return await _repository.GetDetailsOfAllLoans();
            else
                return null;
        }

        #endregion


        #region Apply for a Loan 
      //  [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("Apply")]
        public async Task<ActionResult<int>> ApplyForLoan(LoanRequest loan)
        {
            if (_repository != null)
            {
               return await _repository.ApplyForLoan(loan);
            }

            return 0;
        }

        #endregion


        #region Check Loan Eligibility for a Loan 

        [HttpPost("Eligible")]
        public async Task<ActionResult<IEnumerable<LoanType>>> GetEligibleLoans(vw_CheckEligibility condition)
        {
            if (_repository != null)
                return await _repository.GetEligibleLoans(condition);
            else
                return new List<LoanType>(); 
        }

        #endregion


        #region Upload a Document for Verification 
        [HttpPost("Document")]
        public async Task<ActionResult<int>> UploadADocument(UploadedDocument document)
        {
            if (_repository != null && document != null)
                return await _repository.UploadADocument(document);
            else
                return 0;
        }

        #endregion


        #region Change User Credentials 
        [HttpPut("Credentials")]
        public async Task<ActionResult<int>> UpdateUserCredentials(vw_LoginRepsonse credentials)
        {
            if (_repository != null)
            {
                return await _repository.UpdateUserCredentials(credentials);
            }
            return 0;
        }

        #endregion

    }
}
