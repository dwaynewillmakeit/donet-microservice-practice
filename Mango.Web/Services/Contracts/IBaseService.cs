using Mango.Web.Models.Dto;

namespace Mango.Web.Services.Contracts
{
    public interface IBaseService
    {

        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
