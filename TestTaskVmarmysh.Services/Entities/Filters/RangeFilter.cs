namespace TestTaskVmarmysh.Services.Entities.Filters
{
    /// <summary>
    /// The range filter.
    /// </summary>
    public class RangeFilter
    {
        /// <summary>
        /// Filter date from.
        /// </summary>
        public DateTime? From { get; set; }

        /// <summary>
        /// Filter date to.
        /// </summary>
        public DateTime? To { get; set; }

        /// <summary>
        /// Filter search string.
        /// </summary>
        public String Search { get; set; } = string.Empty;
    }
}
