namespace Portolo.Framework.Data
{
    public enum FilterOperation
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }

    public class Filter
    {
        public string PropertyName { get; set; }
        public FilterOperation Operation { get; set; }
        public object Value { get; set; }
    }
}