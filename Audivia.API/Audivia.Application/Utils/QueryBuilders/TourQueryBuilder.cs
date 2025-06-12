using Audivia.Domain.ModelRequests.Tour;
using Audivia.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Application.Utils.QueryBuilders
{
    public static class TourQueryBuilder
    {
        public static FilterDefinition<Tour> BuildFilter(GetToursRequest request)
        {
            var filterBuilder = Builders<Tour>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(request.Title))
            {
                var regex = new BsonRegularExpression(request.Title, "i");
                var notNullFilter = filterBuilder.Ne(t => t.Title, null);
                var nameFilter = filterBuilder.Regex("title", regex);
                filter &= filterBuilder.And(notNullFilter, nameFilter);
            }

            if (!string.IsNullOrEmpty(request.TourTypeId))
            {
                filter &= filterBuilder.Eq(t => t.TypeId, request.TourTypeId);
            }

            if (!string.IsNullOrEmpty(request.TourTypeName))
            {
                var regex = new BsonRegularExpression(request.TourTypeName, "i");
                var notNullFilter = filterBuilder.Ne(t => t.TourType, null);
                var nameFilter = filterBuilder.Regex("tour_type.tour_type_name", regex);
                filter &= filterBuilder.And(notNullFilter, nameFilter);
            }

            return filter;
        }


        public static SortDefinition<Tour>? BuildSort(string? sortBy)
        {
            return sortBy switch
            {
                "ratingDesc" => Builders<Tour>.Sort.Descending(t => t.AvgRating),
                "createdAtDesc" => Builders<Tour>.Sort.Descending(t => t.CreatedAt),
                null or "" => null,
                _ => throw new KeyNotFoundException("Invalid Sorting Criteria!")
            };
        }
    }

}
