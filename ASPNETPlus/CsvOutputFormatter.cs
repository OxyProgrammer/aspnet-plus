using ASPNETPlus.Shared.DataTransferObjects;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace ASPNETPlus
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(CompanyDto).IsAssignableFrom(type) || typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type) ||
                typeof(EmployeeDto).IsAssignableFrom(type) || typeof(IEnumerable<EmployeeDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<CompanyDto>)
            {
                foreach (var company in (IEnumerable<CompanyDto>)context.Object)
                {
                    FormatCsvForCompany(buffer, company);
                }
            }
            else if (context.Object is CompanyDto)
            {
                if (context.Object is CompanyDto company)
                {
                    FormatCsvForCompany(buffer, company);
                }
            }
            else if (context.Object is IEnumerable<EmployeeDto>)
            {
                foreach (var employee in (IEnumerable<EmployeeDto>)context.Object)
                {
                    FormatCsvForEmployee(buffer, employee);
                }
            }
            else if (context.Object is EmployeeDto)
            {
                if (context.Object is EmployeeDto employee)
                {
                    FormatCsvForEmployee(buffer, employee);
                }
            }
            else
            {
                throw new Exception("Unknown type requested for serialization");
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsvForCompany(StringBuilder buffer, CompanyDto company)
        {
            buffer.AppendLine($"{company.Id},\"{company.Name},\"{company.FullAddress}\"");
        }

        private static void FormatCsvForEmployee(StringBuilder buffer, EmployeeDto employee)
        {
            buffer.AppendLine($"{employee.Id},\"{employee.Name},\"{employee.Age},\"{employee.Position}\"");
        }
    }
}

