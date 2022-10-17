namespace Core;

public class Result<T> where T : class
{
    public bool Succeed { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }

    public static Result<T> Success(T data, string message)
        => new()
        {
            Succeed = true,
            Data = data,
            Message = message
        };


    public static Result<T> Failed(string message)
        => new()
        {
            Succeed = false,
            Data = null,
            Message = message
        };
}