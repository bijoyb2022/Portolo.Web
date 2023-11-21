namespace Portolo.Primary.Request
{
    public class LookupCodeRequestDTO
    {
        public int? LookupCodeKey { get; set; }

        public string LookupCodeType { get; set; }

        public string CodeDesc { get; set; }

        public string DisplayCodeDesc { get; set; }

        public string OptType { get; set; }

    }
}
