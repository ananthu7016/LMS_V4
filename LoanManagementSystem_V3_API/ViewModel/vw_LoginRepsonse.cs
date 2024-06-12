namespace LoanManagementSystem_V3_API.ViewModel
{
    public class vw_LoginRepsonse
    {
        // this is a Structure of response that will be Send to the user after the user has entered the username and password 


        public int? RoleId { get; set; }
        // this is the getter and setter for the RoleId since This is a common response that will be send even if the Staff or the 
        // customer tries to login 


        public string? UserName { get; set; }
        // this is to store the Username of the Person who Has Currently logged in 


        public int? Id { get; set; }
                             // This property is to store the ID of staff if the credentials belong to  a staff or we can store 
                             // the Id of Customer if the credentials belong to a customer.

        public string? Password { get; set; }
        // this is a getter and setter for password;

    }
}
