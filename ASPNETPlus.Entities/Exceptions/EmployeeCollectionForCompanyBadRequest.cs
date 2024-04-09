namespace ASPNETPlus.Entities.Exceptions
{
    public sealed class EmployeeCollectionForCompanyBadRequest : BadRequestException
    {
        public EmployeeCollectionForCompanyBadRequest()
        : base("Employee collection for Company sent from a client is null.")
        {
        }
    }

}
