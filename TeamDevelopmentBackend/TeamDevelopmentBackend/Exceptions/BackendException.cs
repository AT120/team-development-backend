[System.Serializable]
public class BackendException : System.Exception
{

    public int HttpCode { get; private set; }
    public BackendException(string message, int httpCode = 400) : base(message)
    {
        HttpCode = httpCode;    
    }

    // private BackendException() { }
    // public BackendException(string message) : base(message) { }
    // public BackendException(string message, System.Exception inner) : base(message, inner) { }
    // protected BackendException(
    //     System.Runtime.Serialization.SerializationInfo info,
    //     System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}