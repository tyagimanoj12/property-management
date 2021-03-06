namespace MyProperty.Web.Models
{

    public class DashboardDataModel
    {
        public int TotalProperties { get; set; }
        public int AssignedProperties { get; set; }
        public int VacantProperties { get; set; }
        public double TotalPayments { get; set; }
        public double DebitPayments { get; set; }
        public double CreditPayments { get; set; }
    }

    //public class TotalPropertiesModel
    //{
    //    public int Total { get; set; }
    //    public int Assigned { get; set; }
    //    public int Vacant { get; set; }
    //}

    //public class TotalPaymentsModel
    //{
    //    public double Total { get; set; }
    //    public double Debit { get; set; }
    //    public double Credit { get; set; }
    //}
}
