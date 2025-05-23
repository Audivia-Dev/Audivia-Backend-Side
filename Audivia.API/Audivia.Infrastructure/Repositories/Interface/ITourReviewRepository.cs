﻿using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;
namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface ITourReviewRepository : IBaseRepository<TourReview>, IDisposable
    {
        Task<List<TourReview>> GetReviewsByTourId(string tourId); 
    }
}
