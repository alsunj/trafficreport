﻿using DALDTO = App.DAL.DTO;

using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IViolationRepository :  IEntityRepository<DALDTO.Violation>, IViolationRepositoryCustom<DALDTO.Violation>
{
   // public Task<IEnumerable<DALDTO.Violation>> GetAllWithViolationTypesAsync();
}

public interface IViolationRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}