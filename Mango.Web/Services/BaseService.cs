using Mango.Web.Models.Dto;
using Mango.Web.Services.Contracts;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "appilcation.json");

                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDto.ApiType)
                {

                    case Utils.StaticDetail.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case Utils.StaticDetail.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    case Utils.StaticDetail.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;

                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception e)
            {

                var dto = new ResponseDto
                {

                    Message = e.Message,
                    IsSuccess = false
                };

                return dto;

            }
        }
    }
}

