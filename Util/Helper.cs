using Guest.Models;

namespace Guest.Util
{
    public class Helper
    {
        public static APIResponse BadRequestObject(object error)
        {
            return new APIResponse()
            {
                statusCode = StatusCodes.Status400BadRequest,
                error = error,
                message = "Bad Request",
            };
        }
        public static APIResponse NotFoundObject(object error)
        {
            return new APIResponse()
            {
                statusCode = StatusCodes.Status404NotFound,
                error = error,
                message = "Resource Not Found",
            };
        }
        public static APIResponse AlreadyFoundObject(object error)
        {
            return new APIResponse()
            {
                statusCode = StatusCodes.Status409Conflict,
                error = error,
                message = "Resource Already Exists",
            };
        }
    }
}
