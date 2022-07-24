using System;
using System.Collections.Generic;

namespace OpenIddictExample.IdP.Domain 
{
    public interface IUser<TKey> where TKey : struct, IEquatable<TKey>
    {
        TKey Id { get; }
        string Guid { get; }
        string Name { get; }
        string LastName { get; }
        string Password { get; }
        string Salt { get; }
        string Email { get; }
        string BoClientId { get; }
        string BoUserLogin { get; }
        bool Enabled { get; }
        bool Deleted { get; }
        DateTime RegistrationDate { get; }
        //IEnumerable<IUserRights<TKey>> UserRights { get; }
    }

}

