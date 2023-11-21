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

        private readonly ITokenProvider _tokenProvider;

        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                //Add token
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.Data != null)
                {
                    var jsonData = JsonConvert.SerializeObject(requestDto.Data);
                    message.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

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

                apiResponse = await httpClient.SendAsync(message);

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

