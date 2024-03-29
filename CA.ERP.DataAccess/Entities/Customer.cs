namespace CA.ERP.DataAccess.Entities
{
    public class Customer : EntityBase
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Employer { get; set; }
        public string EmployerAddress { get; set; }
        public string CoMaker { get; set; }
        public string CoMakerAddress { get; set; }
    }
}