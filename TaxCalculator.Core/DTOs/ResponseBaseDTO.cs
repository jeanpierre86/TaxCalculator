namespace TaxCalculator.Core.DTOs
{
    public class ResponseBaseDTO
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
