using ThaidWeb.Services.Thaid;

namespace ThaidWeb.Exceptions;

public class ThaidTokenResponseExeption : Exception
{
    public ThaidTokenResponseExeption(ThaidTokenResponse response) : base($"เกิดข้อผิดพลาดในการขอ Token: {response.Error} {response.ErrorDescription} {response.ErrorUri}")
    {
    }
}
