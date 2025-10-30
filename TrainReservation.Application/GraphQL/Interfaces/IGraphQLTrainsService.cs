using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainReservation.Application.GraphQL.Filters;
using TrainReservation.Core.Models;

namespace TrainReservation.Application.GraphQL.Interfaces
{
    public interface IGraphQLTrainsService : IAsyncDisposable
    {
        public Task<List<Train>> GetTrainsByDateAsync(DateFilterInput filter);
        public Task<Train> GetCarsByTypeAsync(CarTypeFilterInput filter);
        public Task<List<int>> GetSeatListAsync(SeatListFilterInput filter);
    }
}
