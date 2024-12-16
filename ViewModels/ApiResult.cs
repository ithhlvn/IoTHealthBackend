/********************************************************************************/
/* COPYRIGHT 										                            */
/* Authors reserve all rights even in the event of industrial property rights.  */
/* We reserve all rights of disposal such as copying and passing			    */
/* on to third parties. 										                */
/*													                            */
/* Description : Create EmrApi.Controllers                                      */
/*                                                                              */
/* Developers : LanHH, Vietnam                                                  */
/* -----------------------------------------------------------------------------*/
/* History 											                            */
/*													                            */
/* Started on : 06 May 2024							                            */
/* Revision : 1.0.0.0 									  	                    */
/* Changed by :     									                        */
/* Change date :                                                                */
/* Changes :                                                                    */
/* Reasons :   										                            */
/********************************************************************************/
namespace IOT.ViewModels
{
    public enum ApiCodes : byte
    {
        Ok = 0,
        Error = 1,
        InvalidData = 2,
        NotFound = 3,
        UpsertNone = 6,
        RemoveNone = 7,
        Ignore = 8,
        Duplication = 9,
        NotAllowable = 10
    }
    public class ApiResult
    {
        public ApiCodes Code { get; set; }
        public string Message { get; set; }
        public long RefId { get; set; }

        public ApiResult() { }
        public ApiResult(ApiCodes code)
        {
            this.Code = code;
        }
        public ApiResult(ApiCodes code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }
    public class ApiResult<T> : ApiResult
    {
        public T Value { get; set; }

        public ApiResult() { }
        public ApiResult(T value)
        {
            this.Code = ApiCodes.Ok;
            this.Value = value;
        }
        public ApiResult(ApiCodes result)
        {
            this.Code = result;
        }
        public ApiResult(ApiCodes code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
        public ApiResult(T value, string message)
        {
            this.Code = ApiCodes.Ok;
            this.Value = value;
            this.Message = message;
        }
        public ApiResult(System.Exception exception)
        {
            this.Code = ApiCodes.Error;
            this.Message = string.IsNullOrEmpty(exception.Message) ? "Internal server error" : exception.Message;
        }
    }
    public class ApiResult<T1, T2> : ApiResult
    {
        public T1 Value { get; set; }
        public T2 Error { get; set; }
    }
}