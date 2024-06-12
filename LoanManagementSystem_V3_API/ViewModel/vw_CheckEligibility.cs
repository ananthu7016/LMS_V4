namespace LoanManagementSystem_V3_API.ViewModel
{
    public class vw_CheckEligibility
    {

        // this is a viewModel to store some properties which are required to check the eligibility of Loans

        public int? Cust_id { get; set; }
        // this is a getter and setter to store the customerId 

        public decimal? CheckAmount {  get; set; }
         // this is a getter and setter to store the details of Amount which the customer entered

        public int? LoanTerm { get; set; }
        // this is a getter and setter for storing the detail of the Loans term that the customer Want 

    }
}
