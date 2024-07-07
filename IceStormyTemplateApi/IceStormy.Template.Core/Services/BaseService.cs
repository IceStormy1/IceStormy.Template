using AutoMapper;
using Microsoft.Extensions.Logging;

namespace IceStormy.Template.Core.Services;

public abstract class BaseService(IMapper mapper, ILogger<BaseService> logger)
{
    protected readonly IMapper Mapper = mapper;
    protected readonly ILogger<BaseService> Logger = logger;
}