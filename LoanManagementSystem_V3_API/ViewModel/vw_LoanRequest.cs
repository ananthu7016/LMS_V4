namespace LoanManagementSystem_V3_API.ViewModel
{
    public class vw_LoanRequest
    {
        // this is the viewmodel of loan request , for the admin to view the loan requests

        public int? LoanId { get; set; }
        // to store the Loan Id

        public int? RequestId { get; set; }
        // this is a getter and setter for the request Id

        public string? LoanName { get; set; }
        // getter and setter for the loan name 

        public int? CustomerId { get; set; }
        //getter and setter for the customer ID

        public string? CustomerName { get; set; }    
        //getter and setter for customer name 

        public DateTime? LoanRequestDate { get; set; }
        // getter and setter to store the loan request date 
        public string? LoanPurpose { get; set; }
        // getter and setter to store the Loan Purpose

        public decimal? RequestedAmount { get; set; }
        // getter and setter to store the Details of Requested amount

    }
}
