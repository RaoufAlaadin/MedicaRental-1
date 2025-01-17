﻿using MedicaRental.BLL.Dtos;
using MedicaRental.BLL.Dtos.RentOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicaRental.BLL.Managers
{
    public interface IRentOperationsManager
    {
        public Task<PageDto<RentOperationDto>?> GetRentedItemsAsync(string userId, int page, string? orderBy, string? searchText);
        public Task<PageDto<RentOperationDto>?> GetRentedItemsHistoryAsync(string userId, int page, string? orderBy, string? searchText);
        public Task<PageDto<RentOperationDto>?> GetOnRentItemsAsync(string userId, int page, string? orderBy, string? searchText);
        public Task<PageDto<RentOperationDto>?> GetOnRentItemsHistoryAsync(string userId, int page, string? orderBy, string? searchText);
        public Task<ItemHasBeenRentedToUserDto> GetRentingStatus(string usreId, Guid ItemId);
        
        public Task<Guid?> AddRentOperation(InsertRentOperationDto rentOperationDto);

        public Task<IEnumerable<GetRentedItemsDto>?> GetRentedItemsAsync();

        public Task<StatusDto> AcceptReturnAsync(Guid rentOperationId);
    }
}
