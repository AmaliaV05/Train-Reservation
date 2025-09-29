using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Train_Reservation_Application.GraphQL.Filters;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.GraphQL.Interfaces
{
    public interface IGraphQLTrainsService : IAsyncDisposable
    {
        public Task<List<Train>> GetTrainsByDateAsync(DateFilterInput filter);
        public Task<Train> GetCarsByTypeAsync(CarTypeFilterInput filter);
        public Task<List<int>> GetSeatListAsync(SeatListFilterInput filter);
    }
}
