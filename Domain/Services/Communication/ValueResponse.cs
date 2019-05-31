namespace OPS_API.Domain.Services.Communication
{
    public class ValueResponse<TEntity> : Response where TEntity : class
    {
        public TEntity Value;

        private ValueResponse(bool success, string message, TEntity value) : base(success, message)
        {
            Value = value;
        }

        public ValueResponse(TEntity value) : this(true, string.Empty, value) { }

        public ValueResponse(string errorMessage) : this(false, errorMessage, null) { }
    }
}