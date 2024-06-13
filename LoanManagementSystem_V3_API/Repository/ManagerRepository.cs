using LoanManagementSystem_V3_API.Repository;
using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementSystem_V3_API
{
    // This is the Admin Repository
    public class ManagerRepository : IManagerRepository
    {

        //first through DI we need to create a instance of the Database context for the Entity Framework

        //----------------------------------------------------------
        private readonly LmsV4DbContext _context;

        public ManagerRepository(LmsV4DbContext context)
        {
            _context = context;
        }


        //-----------------------------------------------------------

        #region Get Details of Loans Applied By Every Customer

        public async Task<ActionResult<IEnumerable<vw_LoanRequest>>> GetAllRequestedLoans()
        {
            if(_context != null)
            {
               return await (from r in _context.LoanRequests
                       from c in _context.Customers
                       from l in _context.LoanTypes
                       where r.LoanTypeId == l.LoanTypeId && r.CustId == c.CustomerId && r.IsActive==true
                       select new vw_LoanRequest
                       {
                           CustomerId = c.CustomerId,
                           RequestId = r.RequestId,
                           LoanId = l.LoanTypeId,
                           LoanName = l.LoanTypeName,
                           CustomerName = c.FullName,
                           LoanPurpose = r.LoanPurpose,
                           LoanRequestDate = r.LoanRequestDateTime,
                           RequestedAmount = r.RequestedAmount
                       }).ToListAsync();
            }

            return new List<vw_LoanRequest>();
            // if something went wrong we return an empty list 
            
        }

        #endregion



        #region Get Details of All Loan Officers

        public async Task<ActionResult<IEnumerable<vw_Dropdown>>> GetDetailsOfOfficers()
        {
           if( _context != null)
            {

                try
                {

                    return await (from u in _context.Users
                                  where u.RoleId == 3
                                  select new vw_Dropdown
                                  {
                                      Id = u.UserId,
                                      Name = u.FullName
                                  }).ToListAsync();

                }
                catch(Exception ex) { }
         
            }

            return new List<vw_Dropdown>();// is something went wrong we return a Empty List
        }

        #endregion


        #region Assign a Officer for Verification

        public async Task<ActionResult<int>> AssignVerificationOfficer(LoanVerification detail)
        {
            if(_context != null)
            {
                try
                {
                    detail.VerificationReview = "";
                    detail.VerificationStatus = true;

                    try
                    {
                        // then we need to add the instance to DbContext
                        await _context.LoanVerifications.AddAsync(detail);
                    }
                    catch(Exception ex) { }
                    

                    // defing an instance to store the details of LoanRequest
                    LoanRequest request = new LoanRequest();
                    try
                    {
                        // then after we assign we need to set the status of request to false 
                        request = await _context.LoanRequests.Where(r => r.RequestId == detail.RequestId).FirstAsync();

                    }
                    catch(Exception) { }
                    
                    if(request != null)
                    {
                        request.IsActive= false;
                    }

                    try
                    {
                        // then we need to save changes to the Database.
                        await _context.SaveChangesAsync();

                    }
                    catch (Exception ex) { }


                    // then we need to Update the customer about the Action Happend ie Verification Officer Assigned 

                    // creating a new instance of verification summary to store the details to insert \
                    LoanVerificationSummary summary = new LoanVerificationSummary();

                    // then defining the instance 
                    summary.RequestId = detail.RequestId;
                    summary.Summary = "Under Verification";
                    summary.StatusChangedDateTime = DateTime.Now;

                    //then we need to add instance to Db Set 
                    await _context.LoanVerificationSummaries.AddAsync(summary);


                    // then we need to save changed 

                    await _context.SaveChangesAsync();

                    string description = "Manger has assigned a loan verification to " + detail.UserId;
                    Log newLog = new Log
                    {
                        TimeStamp = DateTime.Now,
                        LogDescription = description,
                        EventType = "Loan Rejected" // ie loan Rejected
                    };

                    // then we call method to save this to database 
                    await SaveDetailsToLog(newLog);

                    // then we need to return one to show that its a success 
                    return 1;
                    
                    
                }
                catch (Exception ex) { }

                
            }
            return 0;
        }

        #endregion


        #region Get all Details of Verified Loans for Approval
        public async Task<ActionResult<IEnumerable<vw_ApprovalDetails>>> GetDetailsOfLoanToApprove()
        {
            if( _context != null )
            {
                try
                {
             return await (from v in _context.LoanVerifications
                           from c in _context.Customers
                           from r in _context.LoanRequests
                           from l in _context.LoanTypes
                           where v.RequestId == r.RequestId && r.CustId == c.CustomerId && r.LoanTypeId == l.LoanTypeId && v.VerificationStatus == false
                           select new vw_ApprovalDetails
                           {
                               VerificationId = v.VerificationId,
                               CustomerId = c.CustomerId,
                               CustomerName = c.FullName,
                               CustomerPhone = c.Phone,
                               LoanTypeId = l.LoanTypeId,
                               LoanName = l.LoanTypeName,
                               LoanAmount = r.RequestedAmount,
                               LoanRequestDate = r.LoanRequestDateTime,
                               Review = v.VerificationReview,
                               LoanPurpose = r.LoanPurpose,
                               UserId = v.UserId
                           }).ToListAsync();
                }
                catch { }
            }

            return new List<vw_ApprovalDetails>();
            // returning an empty list if something went wrong.
        }




        #endregion


        #region Approve a Loan 
        public async Task<ActionResult<int>> ApproveALoan(vw_ApprovalDetails loan)
        {
           if(_context != null)
            {
                //first we need to create an instance for LoanDetails 
                LoanDetail loanDeatil = new LoanDetail()
                {
                    LoanTypeId= loan.LoanTypeId,
                    CustId = loan.CustomerId,
                    LoanAmount = loan.LoanAmount,
                    LoanRequestDateTime = loan.LoanRequestDate,
                    LoanSanctionDateTime = DateTime.Now.Date,
                    IsActive = true,
                    LoanPurpose = loan.LoanPurpose,
                    UserId = loan.UserId
                };



                // Then we need to get the details of Requested loan From LoanVerification Table to remove it if Loan is Sanctioned.
                LoanVerification verificationDetails = new LoanVerification();
                int? flag = 0;

                try
                {
                    verificationDetails = await _context.LoanVerifications.Where(l => l.VerificationId == loan.VerificationId).FirstAsync();
                    flag = 1;
                    // to show that there is details with that ID
                }
                catch (Exception e) { }


                try
                {
                    if (flag == 1)
                    {

                        await _context.LoanDetails.AddAsync(loanDeatil);

                        // then we need to Remove The Details From Loan Verification Details 
                        _context.LoanVerifications.Remove(verificationDetails);

                        // if this add and removal is suceess only the details will be saved 

                        // then we need to save changes
                        await _context.SaveChangesAsync();

                        //---------------------------------------------------------------------------------
                        // then we need to update the customer that his loan is approved

                        // creating a new instance of verification summary to store the details to insert 
                        LoanVerificationSummary summary = new LoanVerificationSummary();

                        // then defining the instance 
                        summary.RequestId = verificationDetails.RequestId;
                        summary.Summary = "Loan Approved";
                        summary.StatusChangedDateTime = DateTime.Now;

                        //then we need to add instance to Db Set 
                        await _context.LoanVerificationSummaries.AddAsync(summary);


                        // then we need to save changed 

                        await _context.SaveChangesAsync();
                        //-----------------------------------------------------------------------------------
                        
                        // then we need to Update the Log for the Admin to see 

                        string description = "Manger has approved a loan of " + loan.CustomerName + "who requested for a " + loan.LoanName;
                        Log newLog = new Log
                        {
                            TimeStamp = DateTime.Now,
                            LogDescription = description,
                            EventType = "Loan Approved" // ie loan Rejected
                        };

                        // then we call method to save this to database 
                        await SaveDetailsToLog(newLog);

                        // then we will return 1 to show success 
                        return 1;
                    }


                }
                catch(Exception ex) { }


                

            }
            return 0;
        }

        #endregion


        #region Reject a Loan 
        public async Task<ActionResult<int>> RejectALoan(vw_ApprovalDetails loan)
        {
            if(_context != null)
            {
                // Then we need to get the details of Requested loan From LoanVerification Table to remove it if Loan is Sanctioned.
                LoanVerification verificationDetails = new LoanVerification();
                int? flag = 0;
                try
                {
                    verificationDetails = await _context.LoanVerifications.Where(l => l.VerificationId == loan.VerificationId).FirstAsync();
                    flag = 1;
                    // to show that there is details with that ID
                }
                catch (Exception e) { }


                try
                {
                    // then we need to Remove The Details From Loan Verification Details 
                    _context.LoanVerifications.Remove(verificationDetails);

                    // if this add and removal is suceess only the details will be saved 

                    // then we need to save changes
                    await _context.SaveChangesAsync();



                    //---------------------------------------------------------------------------------
                    // creating a new instance of verification summary to store the details to insert 
                    LoanVerificationSummary summary = new LoanVerificationSummary();

                    // then defining the instance 
                    summary.RequestId = verificationDetails.RequestId;
                    summary.Summary = "Loan Approved";
                    summary.StatusChangedDateTime = DateTime.Now;

                    //then we need to add instance to Db Set 
                    await _context.LoanVerificationSummaries.AddAsync(summary);


                    // then we need to save changed 

                    await _context.SaveChangesAsync();
                    //-----------------------------------------------------------------------------------


                    // before that we need to save this details to log 
                    string description = "Manger has rejected a loan of "+loan.CustomerName+ "who requested for a "+loan.LoanName;
                    Log newLog = new Log
                    {
                        TimeStamp = DateTime.Now,
                        LogDescription = description,
                        EventType = "Loan Rejected" // ie loan Rejected
                    };

                    // then we call method to save this to database 
                    await SaveDetailsToLog(newLog);

                    // then we will return 1 to show success 
                    return 1;
                }
                catch (Exception e) { }
            }

            return 0;
        }


        #endregion


        #region Add Details of a new  Loan 

        public async Task<ActionResult<int>> AddNewLoanDetails(LoanType loan)
        {
            if(_context != null && loan != null)
            {

                //so to the recieved instance we need to add some extra details like The time stamp when it was created 
                loan.CreatedDateTime = DateTime.Now;
                try
                {
                    await _context.LoanTypes.AddAsync(loan);

                    // then we need to save it to database
                    await _context.SaveChangesAsync();

                    // before that we need to save this details to log 
                    string description = "Manger has added a new Loan  " + loan.LoanTypeName;
                    Log newLog = new Log
                    {
                        TimeStamp = DateTime.Now,
                        LogDescription = description,
                        EventType= "New Loan Added" // ie new Loan added
                    };

                    // then we call method to save this to database 
                    await SaveDetailsToLog(newLog);

                    // then as a sucess response we need to send 1 
                    return 1;
                }
                catch (Exception ex) { 
                    // to catch any exception that maybe raised.
                 }    

            }

            return 0;
            // return zero to show that there was some problem while inserting the new Loan.
        }

        #endregion


        #region Disable or Enable a Loan 

        public async Task<ActionResult<int>> ToggleLoanStatus(int loan_id)
        {
            // so we need to set the status of the Loan with the given Id to false / true 
            if(_context != null)
            {
                LoanType loanDetails = new LoanType();
                loanDetails.LoanTypeId = 0;
                // defining and instance to store the details of loan with this id

                try
                {
                    loanDetails = await _context.LoanTypes.Where(l=>l.LoanTypeId == loan_id).FirstAsync();
                    // this will get the detail of loan with this id 

                }catch(Exception e)
                {
                    // to catch any exception that maybe raised.
                }

                if(loanDetails.LoanTypeId != 0)
                {
                    // then we have a Loan with the matching ID
                    loanDetails.IsActive = !loanDetails.IsActive;
                    // this will toogle the loan status 

                    // then we need to save the change to database
                    await _context.SaveChangesAsync();
                    // then we need to return success status;

                    // before that we need to save this details to log 
                    string description = "Manger has set the Status of " + loanDetails.LoanTypeName + " to " + loanDetails.IsActive;
                    Log newLog = new Log
                    {
                        TimeStamp = DateTime.Now,
                        LogDescription = description,
                        EventType= "Loan Status Updated" // ie loan status updated
                    };

                    // then we call method to save this to database 
                    await SaveDetailsToLog(newLog);

                    return 1;
                }
            }

            return 0;
            // if something went wrong we need to return zero 
        }

        #endregion



        #region Save Action Happend to Log File 

       async Task<ActionResult<int>> SaveDetailsToLog(Log logDetails)
        {
            if (_context != null)
            {
                //setting the status of log to true
                logDetails.LogStatus = true;
                try
                {
                    await _context.Logs.AddAsync(logDetails);
                    // then we need to save it to Database 

                    await _context.SaveChangesAsync();

                    // then we need to return Status as One 
                    return 1;

                }
                catch(Exception ex) { }

            }

            return 0;
            // if the operation failed
        }

        #endregion


    }
}
