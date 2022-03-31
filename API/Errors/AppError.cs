namespace API.Errors;

class AppError : Exception
{

    public Int32 statusCode;
    public string message;

    public AppError(string message, Int32 statusCode)
    {
        this.message = message;
        this.statusCode = statusCode;
    }

}