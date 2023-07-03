using System.Security.Cryptography;

namespace ThaidWeb.Helpers;

public class RandomString
{
    public const string NoSymbolCharactors = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    public static string GetRandomString(int length, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_")
    {
        var data = new byte[length];
        byte[] buffer = null;
        var maxRandom = byte.MaxValue - (byte.MaxValue + 1) % chars.Length;

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(data);

        var result = new char[length];

        for (var i = 0; i < length; i++)
        {
            var value = data[i];

            while (value > maxRandom)
            {
                if (buffer == null)
                {
                    buffer = new byte[1];
                }

                rng.GetBytes(buffer);
                value = buffer[0];
            }

            result[i] = chars[value % chars.Length];
        }

        return new string(result);
    }
}
