namespace TestTaskVmarmysh.Services.Entities
{
    /// <summary>
    /// Journal item view.
    /// <seealso cref="TestTaskVmarmysh.Services.Entities.BaseJournalItemView"/> successor.
    /// </summary>
    public class JournalItemView: BaseJournalItemView
    {
        /// <summary>
        /// Text of journal item.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}
