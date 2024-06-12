namespace LoanManagementSystem_V3_API.ViewModel
{
    public class vw_ApprovalDetails
    {
        // this is a model to Map the details for the Approval list 

        public int? VerificationId { get; set; }
        // this is a getter and setter for the verification Id. 

        public int? CustomerId { get; set; }
        //this is a getter and setter for Customer Id
        public string? CustomerName { get; set; }
        // this is a getter and setter to store the details of a Customer. 

        public string? CustomerPhone { get; set; }
        // this is a getter and setter to store the Phone number of Customer

        public int? LoanId { get; set; }
        // this is a getter and setter for Loan Id 

        public string? LoanName { get; set; }
        // this is a getter and setter to store the details of LoanName. 

        public decimal? LoanAmount { get; set; }
        // this is a getter and setter for Loan Amount. 

        public string? Review {  get; set; }
        // this is a getter and setter to store the verification review.


        public DateTime? LoanRequestDate { get; set; }
        // this is a getter and setter for Loan Request date 

        public int? RepaymentFrequency { get; set; }
        // this is a getter and setter to store the Repayment Frequnecy Selected by the customer



    }

}
