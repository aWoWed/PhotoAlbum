namespace Photo_album.BLL.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succeed, string message, string prop)
        {
            Succeed = succeed;
            Message = message;
            Property = prop;
        }
        public bool Succeed { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }

    }
}