using TectProject.API.DTO;
using TectProject.API.DTO.Bpkb.Request;
using TectProject.API.DTO.StorageLocation.Response;

namespace TectProject.API.Interface
{
    public interface IBpkbService
    {
        Task<BaseResponse<string>> InsertBpkb(CreateBpkbRequest request, string userId);
        Task<List<StorageLocationResponse>> GetStorageLocations();
    }
}