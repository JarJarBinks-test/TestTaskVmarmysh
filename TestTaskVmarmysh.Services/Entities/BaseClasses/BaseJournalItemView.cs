namespace TestTaskVmarmysh.Services.Entities
{
    /// <summary>
    /// Base class for journal item view.
    /// </summary>
    public class BaseJournalItemView
    {
        /// <summary>
        /// Id of journal item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Event id for track.
        /// </summary>
        public string EventId { get; set; } = string.Empty; // TODO: Prevent .nte core rounding.

        /// <summary>
        /// Date of creation.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
