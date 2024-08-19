using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskVmarmysh.DataAccess.Entities.JournalEntities
{
    /// <summary>
    /// Journal item class.
    /// </summary>
    public class Journal
    {
        /// <summary>
        /// Journal item id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Journal item event id.
        /// </summary>
        public long EventId { get; set; }

        /// <summary>
        /// Journal item created at.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Text of journal item.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Text { get; set; } = string.Empty;
    }
}
