using System;

namespace OpenIddictExample.IdP.Domain 
{
    public interface IUserRights<TKey> where TKey : struct, IEquatable<TKey>
    {
        TKey Id { get; }
        string Platform { get; }
        string Xml { get; }
        int Licenses { get; }
        bool Enabled { get; }

        IUser<TKey> User { get; }
    }

}
