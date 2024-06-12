namespace LoanManagementSystem_V3_API.ViewModel
{
    public class vw_VerificationDetails
    {
        // this is a model class to define the Propeties to list all the Details for Verification

        public int? VerificationId { get; set; }
        // this is a getter and setter for verification Id

        public int? RequestId { get; set; }
        // this is a getter and setter for Request Id

        public string? CustomerName {  get; set; }
        // this is a getter and setter for Customer Name 

        public string? LoanName { get; set; }
        // this is a getter and setter for Loan Name 

        public string? CustomerAddress { get; set; }
        // this is a getter and setter for Customer Address 
        public string? CustomerContact {  get; set; }
        // this is a getter and setter for Customer Contacts 

        public string? CustomerGender { get; set; }
        // this is a getter and setter for Customer Gender 

        public string? CustomerNationality { get; set; }
        // this is a getter and setter for customer nationality 

        public decimal? CustomerAnnualIncome { get; set; }
        // this is a getter and setter for storing the annual income of the customer 

        public string? CustomerOccupation { get; set; }
        // this is a getter and setter for the Occupation of the Customer 

        public decimal? LoanAmount { get; set; }
        // this is a getter and setter for the Loan Amount

        public string? LoanPurpose { get; set; }
        // this is a getter and setter to store the Purpose of Loan

    }
}
