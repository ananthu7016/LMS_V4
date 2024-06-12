namespace LoanManagementSystem_V3_API.ViewModel
{
    public class vw_LoanDetailsOfCustomer
    {
        // this is a View model to represent all the details of loans that belong to a customer 

        public int? CustomerId { get; set; }
        // this is a getter and setter for customer Id

        public string? CustomerName { get; set; }
        // getter and setter to get the Name of the Customer 

        public string? LoanName { get; set; }
        // the getter and setter to store the Name of Loan Taken By Customer

        public decimal? LoanAmountTaken { get; set; }
        // the getter and setter for storing the Amount that the Customer has Taken 

        public decimal? AmountToPay { get; set; }
        // the getter and setter for the Amount to be Paid by the customer 

        public decimal? AmountPayed { get; set; }
        // this is the getter and setter for the Details of Amount That the Customer Has Paid

        public bool? LoanStatus { get; set; }
        // this is a getter and setter for Loan Status 
    }
}
