using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TectProject.API.Data;
using TectProject.API.DTO;
using TectProject.API.DTO.Bpkb.Request;
using TectProject.API.DTO.StorageLocation.Response;
using TectProject.API.Interface;
using TectProject.API.Models;

namespace TectProject.API.Service.Bpkb
{
	public class BpkbService : IBpkbService
	{
		private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BpkbService(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<List<StorageLocationResponse>> GetStorageLocations()
		{
			var locations = await context.MsStorageLocations
            .Select(l => new StorageLocationResponse
            {
                LocationId = l.LocationId,
                LocationName = l.LocationName
            })
            .ToListAsync();

        	return locations;
		}

		public async Task<BaseResponse<string>> InsertBpkb(CreateBpkbRequest request, string userId)
		{
			var response = new BaseResponse<string>();

			// Check if LocationId exists
			var locationExists = await context.MsStorageLocations.AnyAsync(l => l.LocationId == request.Location.Value);
			if (!locationExists)
			{
				response.Success = false;
				response.Message = "Invalid LocationId. The specified location does not exist.";
				return response;
			}

			var bpkb = mapper.Map<TrBpkb>(request);
			bpkb.CreatedBy = userId;
			bpkb.CreatedOn = DateTime.Now;
			bpkb.LastUpdatedBy = userId;
			bpkb.LastUpdatedOn = DateTime.Now;

			context.TrBpkbs.Add(bpkb);
			await context.SaveChangesAsync();

			response.Success = true;
			response.Message = "BPKB data inserted successfully";
			response.Data = bpkb.AgreementNumber;

			return response;
		}
	}
}