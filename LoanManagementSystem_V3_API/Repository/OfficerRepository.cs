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

        private readonly LmsV3DbContext _context;

        public OfficerRepository(LmsV3DbContext context)
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
                           where v.RequestId == r.RequestId && r.CustId == c.CustId && r.LoanId == l.LoanId && v.VerifiedBy == user_id && v.VerificationStatus == true
                           select new vw_VerificationDetails
                           {
                              VerificationId = v.VerificationId,
                              RequestId = r.RequestId,
                              LoanName = l.LoanName,
                              CustomerName = c.CustFirstName+" "+c.CustLastName,
                              CustomerAddress= c.CustAddress,
                              CustomerAnnualIncome = c.CustAnnualIncome,
                              CustomerContact = c.CustPhone,
                              CustomerGender = c.CustGender,
                              CustomerNationality = c.CustNationality,
                              CustomerOccupation = c.CustOccupation,
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
                    oldReport.StatusChangedDateTime = DateTime.Now;
                  
                    _context.LoanVerifications.Update(oldReport);
                    // then we need to save changes 

                    await _context.SaveChangesAsync();

                    // then we need to return 1 to show the success status
                    return 1;
                }



            }
            return 0;
        }

        #endregion



        #region Get Details of all Documents Uploaded by a Customer 

        public async Task<ActionResult<IEnumerable<vw_Documents>>> GetDocumentOfCustomer(int customer_id)
        {
            if(_context != null && customer_id !=0)
            {
                try
                {
                     return await (from d in _context.UploadedDocuments
                                   from t in _context.DocumentTypes
                                   where d.DocTypeId == t.DocTypeId && d.CustId == customer_id
                                   select new vw_Documents
                                   {
                                       DocPath = d.DocPath,
                                       DocType = t.DocTypeName,
                                       UploadId = d.UploadId
                    
                                   }).ToListAsync();
                }
                catch { }
            }

            // so if something went wrong we need to return an empty List
            return new List<vw_Documents>();
        }

        #endregion


    }
}
