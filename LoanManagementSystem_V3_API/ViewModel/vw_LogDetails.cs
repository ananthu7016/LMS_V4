namespace LoanManagementSystem_V3_API.ViewModel
{
    public class vw_LogDetails
    {
        // this is a model to map the Log Details of every thing Happening to Keep track of Every Events 

        public int LogId { get; set; }
        // this is a getter and setter for LogId

        public string? EventName { get; set; }
        // this is a getter and setter for Event Name

        public DateTime? TimeStamp { get; set; }
        // this is a getter and setter for the Time Stamp 

        public string? LogDescription { get; set; }
        // this is the Description of the Event 
    }
}
