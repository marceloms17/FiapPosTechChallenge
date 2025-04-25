namespace Core.PosTech8Nett.Api.Infra.Logs.Models
{
    public class LogNumber
    {
        private LogNumber(decimal value) => NumberValue = value;

        public static LogNumber GetLog(decimal value) => new LogNumber(value);
        public decimal NumberValue { get; set; }
    }
}
