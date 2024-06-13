namespace LoanManagementSystem_V3_API.ViewModel
{
    public class vw_LoanStatusUpdate
    {
        // this is a View model to represent all the details of loans that belong to a customer 

        public int? CustomerId { get; set; }
        // this is a getter and setter for customer Id

        public string? CustomerName { get; set; }
        // getter and setter to get the Name of the Customer 

        public string? LoanName { get; set; }
        // the getter and setter to store the Name of Loan Taken By Customer

        public string? LoanRequestStatus { get; set; }
        // this is a getter and setter to store the status updates of each Loan Requests

        public DateTime? StatusUpdatedDateTime {  get; set; }
        // this is a getter and setter for Status updated Data and time 
    }
}
