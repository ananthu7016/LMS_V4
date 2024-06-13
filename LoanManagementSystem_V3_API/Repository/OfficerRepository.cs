using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LoanManagementSystem_V3_API.Repository
{
    public class OfficerRepository : IOfficerRepository
    {


        // this is the implementation of the IOfficerInterface 
       
        //-------------------------
        // then through dependency injection we need intatiate the instance of Database context

        private readonly LmsV4DbContext _context;

        public OfficerRepository(LmsV4DbContext context)
        {
            _context = context;
        }


        //-------------------------


        #region Get Details of All Assigned Varification 

        public async Task<ActionResult<IEnumerable<vw_VerificationDetails>>> GetDetailsToVerify(int user_id)
        {
            if(_context != null && user_id !=0)
            {
                try
                {
             return await (from v in _context.LoanVerifications
                           from c in _context.Customers
                           from l in _context.LoanTypes
                           from r in _context.LoanRequests
                           where v.RequestId == r.RequestId && r.CustId == c.CustomerId && r.LoanTypeId == l.LoanTypeId && v.UserId == user_id && v.VerificationStatus == true
                           select new vw_VerificationDetails
                           {
                              VerificationId = v.VerificationId,
                              RequestId = r.RequestId,
                              LoanName = l.LoanTypeName,
                              CustomerName = c.FullName,
                              CustomerAddress= c.Address,
                              CustomerContact = c.Phone,
                              CustomerOccupation = c.Occupation,
                              LoanAmount = r.RequestedAmount,
                              LoanPurpose = r.LoanPurpose

                           }).ToListAsync();
                }
                catch { }
            }

            return new List<vw_VerificationDetails>();
        }

        #endregion



        #region Submit a Verification Report 

        // this method is responsible to Submit the verification form 
        async Task<ActionResult<int>> IOfficerRepository.SubmitVerificationReport(vw_Dropdown report)
        {
            if(_context != null)
            {
                LoanVerification oldReport = new LoanVerification();
                // defining an instance to store the existing report
                int temp=0;
                try
                {
                    oldReport = await _context.LoanVerifications.Where(v => v.VerificationId == report.Id).FirstAsync();
                    temp = 1;
                    
                }catch (Exception e) { }


                if(oldReport != null && temp ==1)
                {
                    oldReport.VerificationReview = report.Name;
                    oldReport.VerificationStatus = false;
                  
                    _context.LoanVerifications.Update(oldReport);
                    // then we need to save changes 

                    await _context.SaveChangesAsync();

                    //-----------------------------------------------------------------------------------------
                    // then we need to Update the Customer that his Loan is Send for Approval


                    // creating a new instance of verification summary to store the details to insert 
                    LoanVerificationSummary summary = new LoanVerificationSummary();

                    // then defining the instance 
                    summary.RequestId = oldReport.RequestId;
                    summary.Summary = "Send For Approval";
                    summary.StatusChangedDateTime = DateTime.Now;

                    //then we need to add instance to Db Set 
                    await _context.LoanVerificationSummaries.AddAsync(summary);


                    // then we need to save changed 

                    await _context.SaveChangesAsync();

                    //-----------------------------------------------------------------------------------------

                    // then we need to return 1 to show the success status
                    return 1;
                }



            }
            return 0;
        }

        #endregion





    }
}
