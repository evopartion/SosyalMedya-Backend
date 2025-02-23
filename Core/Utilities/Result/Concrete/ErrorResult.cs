namespace Core.Utilities.Result.Concrete
{
    public class ErrorResult : Result
    {
        // başarılı olan durum için
        public ErrorResult() : base(false)
        {

        }

        public ErrorResult(string message) : base(false, message)
        {

        }
    }


}
