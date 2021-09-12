namespace Photo_album.BLL.Infrastructure
{
    /// <summary>
    ///     Represents operation details
    /// </summary>
    public class OperationDetails
    {
        /// <summary>
        ///     Creates instance of operation details
        /// </summary>
        /// <param name="succeed"></param>
        /// <param name="message"></param>
        /// <param name="prop"></param>
        public OperationDetails(bool succeed, string message, string prop)
        {
            Succeed = succeed;
            Message = message;
            Property = prop;
        }

        /// <summary>
        ///      Gets or private sets succeed
        /// </summary>
        public bool Succeed { get; private set; }

        /// <summary>
        ///      Gets or private sets message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        ///      Gets or private sets property
        /// </summary>
        public string Property { get; private set; }

    }
}