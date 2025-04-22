using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class TourReviewRepository : BaseRepository<TourReview>, ITourReviewRepository
    {
        public TourReviewRepository(IMongoDatabase database) : base(database) { }

        public Task<int> CountTourReviews()
        {
            throw new NotImplementedException();
        }
    }

}
