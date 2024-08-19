namespace TestTaskVmarmysh.Common.Exceptions
{
    /// <summary>
    /// Exception for prevent remove not empty parent node.
    /// </summary>
    public class SecureException: BaseTypedException
    {
        /// <summary>
        /// Constructor of <seealso cref="TestTaskVmarmysh.Common.Exceptions.SecureException"/>
        /// </summary>
        public SecureException():base("Secure", "You have to delete all children nodes first")
        {
        }
    }
}
