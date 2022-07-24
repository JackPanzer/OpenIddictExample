namespace OpenIddictExample.IdP.Models
{
    public static class OpenIddictExampleClaimTypes
    {
        public const string UserId = "http://schemas.openiddictexample.io/ws/2022/01/identity/claims/guid";
        public const string UserFormalName = "http://schemas.openiddictexample.io/ws/2022/01/identity/claims/formal-name";
        public const string UserEmail = "http://schemas.openiddictexample.io/ws/2022/01/identity/claims/email";
        public const string UserRightsPresence = "http://schemas.openiddictexample.io/ws/2022/01/identity/claims/user-rights";

        public const string LaLeyDigital = "http://schemas.openiddictexample.io/ws/2022/01/identity/claims/user-rights/lld";
        public const string TiendaWke = "http://schemas.openiddictexample.io/ws/2022/01/identity/claims/user-rights/twke";
        public const string SmartecaES = "http://schemas.openiddictexample.io/ws/2022/01/identity/claims/user-rights/smt_es";
    }
}
