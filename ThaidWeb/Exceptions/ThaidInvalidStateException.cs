namespace ThaidWeb.Exceptions;

public class ThaidInvalidStateException : Exception
{
    public ThaidInvalidStateException() : base("รหัส State ไม่ถูกต้อง!")
    {
    }
}
