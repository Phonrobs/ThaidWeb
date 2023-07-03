using ThaidWeb.Services.Thaid;

namespace ThaidWeb.Exceptions;

public class ThaidErrorException : Exception
{
    public ThaidErrorException(ThaidError error) : base($"เกิดข้อผิดพลาดในการขอ Token: {error.Error} {error.ErrorDescription} {error.ErrorUri}")
    {
    }
}
