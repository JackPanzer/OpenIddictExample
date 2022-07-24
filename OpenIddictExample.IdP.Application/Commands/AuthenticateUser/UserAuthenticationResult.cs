namespace OpenIddictExample.IdP.Commands.AuthenticateUser
{
    public enum UserAuthenticationResult
    {
        Success = 0,
        WrongPassword = -4,
        NotFound = -3,
        Deleted = -2,
        Disabled = -1
    }
}
