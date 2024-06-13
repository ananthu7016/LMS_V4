using System.ComponentModel.DataAnnotations;
using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementSystem_V3_API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {


        // this is the implementation of the ICustomer Repository all the Functionalities for the customer will be defined in here


        //first through DI we need to create a instance of the Database context for the Entity Framework

        //----------------------------------------------------------
        private readonly LmsV4DbContext _context;

        public CustomerRepository(LmsV4DbContext context)
        {
            _context = context;
        }



        //-----------------------------------------------------------



        #region Register a New Customer 
        public async Task<ActionResult<int>> RegisterNewCustomer(Customer customer)
        {
            if (_context != null)
            {//making sure that the DI is Proper 

                try
                {

                    // so the customer has only entered his information so we need to give him a temperory username and password 
                    // the username will be his first name and password will be phonenumber

                    customer.UserName = customer.FullName;
                    customer.Password = customer.Phone;
                    customer.RegisteredDateTime = DateTime.Now;
                 
                    // setting the username and password.


                    //making sure the customer status is set to true to show that its an active customer
                    customer.IsActive = true;
                    // then we need to insert the details of the customer 
                    await _context.Customers.AddAsync(customer);

                    // then we need to save the changes to Db 

                    await _context.SaveChangesAsync();

                    string description = "A new Customer has Register customer name is "+customer.FullName;
                    Log newLog = new Log
                    {
                        TimeStamp = DateTime.Now,
                        LogDescription = description,
                        EventType = "New Customer Registered"
                    };

                    // then we call method to save this to database 
                    await SaveDetailsToLog(newLog);

                    //if everything is Successfull we will return 1 
                    return 1;


                }
                catch (Exception e)
                {
                    return 0;
                }

            }

            return 0;
            // retuns zero as response if something went wrong.

        }
        #endregion


        #region Get Status Updates of Loan Applied by customer

        public async Task<ActionResult<IEnumerable<vw_LoanStatusUpdate>>> GetStatusUpdateOfLoans(int custId)
        {
            if (_context != null)
            {//making sure DI is Proper

                int? summaryId = 0;

                try
                {
                    // this will select the last SummaryId Added to the Summary Table
                       summaryId = await (
                                          from r in _context.LoanRequests
                                          join s in _context.LoanVerificationSummaries
                                          on r.RequestId equals s.RequestId
                                          select s.SummaryId).MaxAsync();
                                         
                      
                }
                catch (Exception e) { }

                try
                {
                    // using Linq to get all the Loans that the customer has Taken earlier
                    return await (from c in _context.Customers
                                  from l in _context.LoanTypes
                                  from r in _context.LoanRequests
                                  from s in _context.LoanVerificationSummaries
                                  where c.CustomerId == custId && r.CustId == custId && r.LoanTypeId == l.LoanTypeId && s.SummaryId == summaryId
                                  select new vw_LoanStatusUpdate
                                  {
                                      CustomerId = c.CustomerId,
                                      CustomerName = c.FullName,
                                      LoanName = l.LoanTypeName,
                                      LoanRequestStatus = s.Summary,
                                      StatusUpdatedDateTime = s.StatusChangedDateTime
                                  
                                  }).ToListAsync();

                }
                catch (Exception e) { }

            }


            return new List<vw_LoanStatusUpdate>();
            // if something wentwrong we retun a EmptyList

        }



        #endregion


        #region Get Details of All Available Loans

        public async Task<ActionResult<IEnumerable<LoanType>>> GetDetailsOfAllLoans()
        {
            if (_context != null)
            {
                //then we need to use Entity Framework to get all Details of Loans 
                try
                {
                    return await (from l in _context.LoanTypes
                                  where l.IsActive == true
                                  select new LoanType
                                  {
                                      LoanTypeId = l.LoanTypeId,
                                      LoanTypeName = l.LoanTypeName,
                                      LoanInterestRate = l.LoanInterestRate,
                                      LoanMinimumAmount = l.LoanMinimumAmount,
                                      LoanMaximumAmount = l.LoanMaximumAmount,
                                      LoanTerm = l.LoanTerm,
                                      LoanDescription = l.LoanDescription,
                                      ProcessingFee = l.ProcessingFee,
                                      TaxPercentage = l.TaxPercentage
                                  }).ToListAsync();

                    // this will create an List of Instance of Each loans and return it.
                }
                catch (Exception e) { }

            }

            return new List<LoanType>(); // we retrun an empty instance if something went wromng.
        }

        #endregion


        #region Apply for a Loan 

        public async Task<ActionResult<int>> ApplyForLoan(LoanRequest loan)
        {
            if (_context != null && loan != null)
            {
                try
                {
                    loan.LoanRequestDateTime = DateTime.Now.Date;
                    loan.IsActive = true;
                    await _context.LoanRequests.AddAsync(loan);

                    // then we need to save changes

                    await _context.SaveChangesAsync();


                    //------------------------------

                    // creating a new instance of verification summary to store the details to insert 
                    LoanVerificationSummary summary = new LoanVerificationSummary();

                    // then defining the instance 
                    summary.RequestId = loan.RequestId;
                    summary.Summary = "Request Submitted";
                    summary.StatusChangedDateTime = DateTime.Now;

                    //then we need to add instance to Db Set 
                    await _context.LoanVerificationSummaries.AddAsync(summary);


                    // then we need to save changed 

                    await _context.SaveChangesAsync();

                    //------------------------------

                    // if success we need to return 1

                    return 1;

                }
                catch (Exception e) { }
            }

            return 0;
        }


        #endregion


        #region Check Loan Eligibility for a Loan 

        public async Task<ActionResult<IEnumerable<LoanType>>> GetEligibleLoans(vw_CheckEligibility condition)
        {

            if(condition != null)
            {

                // first we need to get the details of the customer to check the Eligibility condition 
                Customer customer = new Customer(); // and instance to store the store the details of customer
                customer.CustomerId = 0;// setting the customer_id to 0 
                try
                {
                    customer = await _context.Customers.Where(c => c.CustomerId == condition.Cust_id).FirstAsync();
                    // this will get the details of all the customer who has the recieved customer_id
                }
                catch (Exception e) { } 


                if(customer.CustomerId != 0)
                {
                    // if the customer id is not equal to zero wehave a valid customer 

                    return await (from l in _context.LoanTypes
                                  where l.IsActive == true && l.IsEmploymentStatusRequired == customer.CustEmploymentStatus && l.LoanMinimumAmount <= condition.CheckAmount*l.LoanTerm && l.LoanTerm <= condition.LoanTerm
                                  select new LoanType
                                  {
                                      LoanTypeId = l.LoanTypeId,
                                      LoanTypeName = l.LoanTypeName,
                                      LoanInterestRate = l.LoanInterestRate,
                                      LoanMinimumAmount = l.LoanMinimumAmount,
                                      LoanMaximumAmount = l.LoanMaximumAmount,
                                      LoanTerm = l.LoanTerm,
                                      LoanDescription = l.LoanDescription,
                                      ProcessingFee = l.ProcessingFee,
                                      TaxPercentage = l.TaxPercentage
                                      
                                  }).ToListAsync();

                    // this will filter the Loans that a customer can apply based on his/her Terms provided.
                }

            }

            return new List<LoanType>();
        }

        #endregion


        #region Save Action Happend to Log File 

        async Task<ActionResult<int>> SaveDetailsToLog(Log logDetails)
        {
            if (_context != null)
            {
                try
                {
                    await _context.Logs.AddAsync(logDetails);
                    // then we need to save it to Database 

                    await _context.SaveChangesAsync();

                    // then we need to return Status as One 
                    return 1;

                }
                catch (Exception ex) { }

            }

            return 0;
            // if the operation failed
        }

        #endregion


    }
}
