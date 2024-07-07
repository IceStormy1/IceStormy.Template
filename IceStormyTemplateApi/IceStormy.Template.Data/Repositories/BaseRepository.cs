using AutoMapper;
using IceStormy.Template.Data.Entities;
using IceStormy.Template.Data.Interfaces;

namespace IceStormy.Template.Data.Repositories;

public sealed class BaseRepository<TEntity>(TemplateDbContext dbContext, IMapper mapper) : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
}