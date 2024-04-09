namespace ASPNETPlus.Shared.DataTransferObjects
{
    public record EmployeeDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Position { get; init; }
        public int Age { get; init; }
    }
}
