﻿using Microsoft.AspNetCore.Identity;

namespace Base.Contracts.Domain;

public interface IDomainAppUser<TUser> : IDomainAppUser<Guid, TUser>
where TUser: IdentityUser<Guid>
{
    
}

public interface IDomainAppUser<TKey, TUser>
    where TKey : IEquatable<TKey>
    where TUser : IdentityUser<TKey>
{
    public TUser? AppUser { get; set; }
    public TKey AppUserId { get; set; }
}