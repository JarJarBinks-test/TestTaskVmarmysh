using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskVmarmysh.DataAccess.Entities.JournalEntities
{
    /// <summary>
    /// Remember me class.
    /// </summary>
    public class RememberMe
    {
        /// <summary>
        /// Journal item id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Code.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Code { get; set; } = string.Empty;
    }
}
