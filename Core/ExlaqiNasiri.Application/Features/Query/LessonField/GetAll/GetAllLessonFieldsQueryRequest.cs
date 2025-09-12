using ExlaqiNasiri.Application.DTOs.LessonField;
using MediatR;

namespace ExlaqiNasiri.Application.Features.Query.GetAll
{
    public class GetAllLessonFieldsQueryRequest : IRequest<GetAllQueryResponse<LessonFieldGetDto>>
    {
        public LessonFieldFilterDto? dto { get; set; }
    }
}
