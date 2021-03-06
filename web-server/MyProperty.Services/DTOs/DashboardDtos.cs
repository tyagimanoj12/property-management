namespace MyProperty.Services.DTOs
{
    public class DashboardDataDto : BaseResponseDto
    {
        public int TotalProperties { get; set; }
        public int AssignedProperties { get; set; }
        public int VacantProperties { get; set; }
        public double TotalPayments { get; set; }
        public double DebitPayments { get; set; }
        public double CreditPayments { get; set; }
    }

    //public class TotalPropertiesDto : BaseResponseDto
    //{
    //    public int Total { get; set; }
    //    public int Assigned { get; set; }
    //    public int Vacant { get; set; }
    //}

    //public class TotalPaymentsDto : BaseResponseDto
    //{
    //    public double Total { get; set; }
    //    public double Debit { get; set; }
    //    public double Credit { get; set; }
    //}

}
