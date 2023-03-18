using AutoMapper;
using Grpc.Core;
using PlatformService.Interfaces;

namespace PlatformService.SyncDataServices.Grpc;

public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
{
    private readonly IPlatformRepo _repo;
    private readonly IMapper _mapper;

    public GrpcPlatformService(IPlatformRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
        var response = new PlatformResponse();
        var platforms = _repo.GetAll();

        foreach (var platform in platforms)
        {
            response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform));
        }

        return Task.FromResult(response);
    }
}