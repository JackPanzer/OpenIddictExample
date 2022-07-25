using OpenIddictExample.IdP.Domain;
using System;

namespace OpenIddictExample.IdP.Infrastructure.Models
{
    public class UserEntity<TKey> : BaseEntity<TKey>, IUser<TKey> where TKey : struct, IEquatable<TKey>
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
