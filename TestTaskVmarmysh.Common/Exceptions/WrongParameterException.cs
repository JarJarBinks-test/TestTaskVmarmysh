namespace TestTaskVmarmysh.Common.Exceptions
{
    /// <summary>
    /// Exception for wrong parameter.
    /// </summary>
    public class WrongParameterException : BaseTypedException
    {
        /// <summary>
        /// Constructor of <seealso cref="TestTaskVmarmysh.Common.Exceptions.WrongParameterException"/>
        /// </summary>
        public WrongParameterException(string wrongParameterName):base("Wrong parameter", $"Wrong parameter '{wrongParameterName}'.")
        {
        }
    }
}
