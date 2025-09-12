using ExlaqiNasiri.Application.DTOs.LessonField;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.LessonField.GetSingle
{
    public class GetSingleLessonFieldQueryRequest : IRequest<GetSingleQueryResponse<LessonFieldGetDto>>
    {
        public Guid Id { get; set; }
    }
}
