namespace TestTaskVmarmysh.Common.Pagging
{
    /// <summary>
    /// Class represents paged items result.
    /// </summary>
    /// <typeparam name="T">Type of items.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Skipped items count.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Total items count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Items in current page.
        /// </summary>
        public List<T> Items { get; set; } = new List<T>();
    }
}