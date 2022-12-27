namespace POREG.Models
{
    public class SimCodeResponse
    {
        public int? ResponseCode { get; set; }
        public string? Msg { get; set; } = string.Empty;
        public dynamic? Result { get; set; } = null;
    }
}
