using System.IO;

namespace OpenIddictExample.IdP.Models
{
    public static class OpenIddictExampleClaims
    {
        private const string OPENIDDICTEXAMPLE_PREFIX = "es.laleynext";

        public const string UserId = $"{ OPENIDDICTEXAMPLE_PREFIX }.userguid";
        public const string UserEmail = $"{ OPENIDDICTEXAMPLE_PREFIX }.email";

        public const string UserFormalName = $"{OPENIDDICTEXAMPLE_PREFIX}.formalname";
    }
}
