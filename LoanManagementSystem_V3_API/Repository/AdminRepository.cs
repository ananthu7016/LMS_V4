using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementSystem_V3_API
{
    public class AdminRepository:IAdminRepository
    {
        // this is the Admin Repository Which Implements the IAdminRepository

        // first through DI we need to get the instance of Database context

        private readonly LmsV4DbContext _context;

        public AdminRepository(LmsV4DbContext context)
        {
            _context = context;
        }
        //-----------------------------------------------------------------



        #region Add a New Staff 

        public async Task<ActionResult<int>> RegisterNewStaff(User user)
        {
           if(_context != null && user != null)
            {
             
                user.IsActive = true;
                user.CreatedDateTime = DateTime.Now;
               

                try
                {
                    // then we need to insert the Staff into UserTable First to get the User ID
                    await _context.Users.AddAsync(user);
                    // then we need to save it to database 
                    await _context.SaveChangesAsync();

                    // then we need to return one as response to indicate success 

                    return 1;

                }
                catch(Exception e) { }

                }
            // then we need to return zero if something went wrong 
            return 0;
        }
         
        #endregion


        #region Get all Log Details 

        public async Task<ActionResult<IEnumerable<vw_LogDetails>>> GetAllLogDetails()
        {
            if(_context != null)
            {
                try
                {
                    return await (from l in _context.Logs
                                  select new vw_LogDetails
                                  {
                                      LogId = l.LogId,
                                      EventName = l.EventType,
                                      TimeStamp = l.TimeStamp,
                                      LogDescription = l.LogDescription

                                  }).ToListAsync(); 

                }
                catch (Exception) { }

            }

            return new List<vw_LogDetails>();
            // we return an empty List
        }

        #endregion

    }

}
