using ExlaqiNasiri.Application.ResultPattern;

namespace ExlaqiNasiri.Application.Abstraction.Base
{
    public interface IUserDtoService<TRegisterDto, TPatchDto, TGetAllDto, TGetSingleDto>
    {
        public Task<Result<bool>> RegisterAsync(TRegisterDto registerDto);
        public Task<Result<bool>> UpdateUserAsync(string UserIdOrEmail, TPatchDto dto);
        public Task<Result<List<TGetAllDto>>> GetAllUserAsync(DateTime? lastCreadetAt, int size);
        public Task<Result<TGetSingleDto>> GetSingleUserAsync(string UserIdOrEmail);
    }
}
