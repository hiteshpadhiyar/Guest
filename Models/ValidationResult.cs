namespace Guest.Models
{
    [Flags]
    public enum GuestsValidationResult
    {
        Default = 0,
        GuestNotExists = 1,
        GuestExists = 2,
        NotValid = 3,
        Ok = 4
    }

    [Flags]
    public enum RequestType
    {
        Default = 0,
        GuestNotExists = 1,
        GuestExists = 2,
        Ok = 3
    }
}

