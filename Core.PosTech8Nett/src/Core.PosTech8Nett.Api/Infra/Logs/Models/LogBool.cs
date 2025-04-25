namespace Core.PosTech8Nett.Api.Infra.Logs.Models
{
    public class LogBool
    {
        private LogBool(bool value) => BoolValue = value;

        public static LogBool GetLog(bool value) => new LogBool(value);
        public bool BoolValue { get; set; }
    }
}
