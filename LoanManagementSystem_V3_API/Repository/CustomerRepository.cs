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
        private readonly LmsV3DbContext _context;

        public CustomerRepository(LmsV3DbContext context)
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

                    customer.UserName = customer.CustFirstName;
                    customer.Password = customer.CustPhone;
                    customer.RegistredDateTime = DateTime.Now;
                 
                    // setting the username and password.


                    //making sure the customer status is set to true to show that its an active customer
                    customer.CustStatus = true;
                    // then we need to insert the details of the customer 
                    await _context.Customers.AddAsync(customer);

                    // then we need to save the changes to Db 

                    await _context.SaveChangesAsync();

                    string description = "A new Customer has Register customer name is "+customer.CustFirstName;
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


        #region Get Details Of All Loans Taken By a Customer

        public async Task<ActionResult<IEnumerable<vw_LoanDetailsOfCustomer>>> GetAllLoansOfCustomer(int custId)
        {
            if (_context != null)
            {//making sure DI is Proper
                try
                {
                    // using Linq to get all the Loans that the customer has Taken earlier
                    return await (from c in _context.Customers
                                  from ld in _context.LoanDeatils
                                  from l in _context.LoanTypes
                                  where c.CustId == custId && ld.CustId == custId && ld.LoanId == l.LoanId 
                                  select new vw_LoanDetailsOfCustomer
                                  {
                                      CustomerId = c.CustId,
                                      CustomerName = c.CustFirstName,
                                      LoanName = l.LoanName,
                                      AmountPayed = ld.TotalAmountRepaid,
                                      AmountToPay = ld.OutstandingBalance,
                                      LoanAmountTaken = ld.LoanAmount,
                                      LoanStatus = ld.LoanStatus
                                  }).ToListAsync();

                }
                catch (Exception e) { }

            }


            return new List<vw_LoanDetailsOfCustomer>();
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
                                  where l.LoanStatus == true
                                  select new LoanType
                                  {
                                      LoanId = l.LoanId,
                                      LoanName = l.LoanName,
                                      LoanIntrestRate = l.LoanIntrestRate,
                                      LoanMinimumAmount = l.LoanMinimumAmount,
                                      LoanMaximumAmount = l.LoanMaximumAmount,
                                      LatePaymentPenalty = l.LatePaymentPenalty,
                                      LoanTerm = l.LoanTerm,
                                      LoanStatus = l.LoanStatus,
                                      LoanDescription = l.LoanDescription,
                                      CollateralRequired = l.CollateralRequired,
                                      ProcessingFee = l.ProcessingFee,
                                      GracePeriod = l.GracePeriod,
                                      TaxPercentage = l.TaxPercentage,
                                      EmployementStatusRequired = l.EmployementStatusRequired,
                                      RepaymentFrequency = l.RepaymentFrequency
                                  }).ToListAsync();

                    // this will create an List of Instance of Each loans and return it.
                }
                catch (Exception e) { }

            }

            return new List<LoanType>(); // we retrun an empty instance if something went wromng.
        }

        #endregion


        #region Get Details of Logged in Customer 


        public async Task<ActionResult<Customer>> GetCustomerDetails(int custId)
        {
            if (_context != null && custId != 0)
            {
                try
                {
                    return await (from c in _context.Customers
                                  where c.CustId == custId
                                  select new Customer
                                  {
                                      CustId = c.CustId,
                                      CustFirstName = c.CustFirstName,
                                      CustLastName = c.CustLastName,
                                      CustAadhar = c.CustAadhar,
                                      CustAddress = c.CustAddress,
                                      CustAnnualIncome = c.CustAnnualIncome,
                                      CustPhone = c.CustPhone
                                  }).FirstAsync();

                }
                catch (Exception) { }
            }

            return new Customer();
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
                    loan.RequestStatus = true;
                    await _context.LoanRequests.AddAsync(loan);

                    // then we need to save changes

                    await _context.SaveChangesAsync();

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
                customer.CustId = 0;// setting the customer_id to 0 
                try
                {
                    customer = await _context.Customers.Where(c => c.CustId == condition.Cust_id).FirstAsync();
                    // this will get the details of all the customer who has the recieved customer_id
                }
                catch (Exception e) { } 


                if(customer.CustId != 0)
                {
                    // if the customer id is not equal to zero wehave a valid customer 

                    return await (from l in _context.LoanTypes
                                  where l.LoanStatus == true && l.EmployementStatusRequired == customer.CustEmploymentStatus && l.LoanMinimumAmount <= condition.CheckAmount*l.LoanTerm && l.LoanTerm <= condition.LoanTerm
                                  select new LoanType
                                  {
                                      LoanId = l.LoanId,
                                      LoanName = l.LoanName,
                                      LoanIntrestRate = l.LoanIntrestRate,
                                      LoanMinimumAmount = l.LoanMinimumAmount,
                                      LoanMaximumAmount = l.LoanMaximumAmount,
                                      LatePaymentPenalty = l.LatePaymentPenalty,
                                      LoanTerm = l.LoanTerm,
                                      LoanStatus = l.LoanStatus,
                                      LoanDescription = l.LoanDescription,
                                      CollateralRequired = l.CollateralRequired,
                                      ProcessingFee = l.ProcessingFee,
                                      GracePeriod = l.GracePeriod,
                                      TaxPercentage =l.TaxPercentage
                                  }).ToListAsync();

                    // this will filter the Loans that a customer can apply based on his/her Terms provided.
                }

            }

            return new List<LoanType>();
        }

        #endregion


        #region Upload a Document for Verification 

        public async Task<ActionResult<int>> UploadADocument(UploadedDocument document)
        {
            if(_context != null && document!=null)
            {
                // so we need to insert the Document which the Customer send for verification When Applying for a Loan
                try
                {
                    await _context.UploadedDocuments.AddAsync(document);

                    await _context.SaveChangesAsync();
                    // then we need to send the success status as 1 
                    return 1;
                }
                catch (Exception ex) { 
                    // this will catch any exception that maybe raised
                                     }
            }

            return 0;
            // return response as zero to show that something went wrong.
        }

        #endregion


        #region Change Customer Credentials 

        public async Task<ActionResult<int>> UpdateUserCredentials(vw_LoginRepsonse credentials)
        {
            if (_context != null)
            {

                Customer customer = new Customer();
                // this is a instance to store the details of the Customer 
                bool isValidCustomer = false;
                try
                {
                    customer = await _context.Customers.Where(c => c.CustId == credentials.Id).FirstAsync();
                    isValidCustomer = true;
                }
                catch (Exception ex) { }

                if (isValidCustomer)
                {
                    // if he is a valid customer  we need to change his credentials 
                    customer.UserName = credentials.UserName;
                    customer.Password = credentials.Password;

                    // then we need to save changed to Database 
                    await _context.SaveChangesAsync();

                    // then we need to return 1 as Success status 
                    return 1;
                }
            }

            return 0;
            // return 0 if something went wrong .
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
