using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services.Contracts;
using Mango.Web.Utils;

namespace Mango.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.POST,
                Data = productDto,
                Url = StaticDetail.ProductApiBaseUrl + "/api/product"
            });
        }

        public async Task<ResponseDto?> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.DELETE,
                Url = StaticDetail.ProductApiBaseUrl + "/api/product/" + id
            });
        }

        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.GET,
                Url = StaticDetail.ProductApiBaseUrl + "/api/product"
            });
        }

        public async Task<ResponseDto?> GetProductAsync(string productCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.GET,
                Url = StaticDetail.ProductApiBaseUrl + "/api/product/get-by-code/" + productCode
            });
        }

        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.GET,
                Url = StaticDetail.ProductApiBaseUrl + "/api/product/" + id
            });
        }

        public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.PUT,
                Data = productDto,
                Url = StaticDetail.ProductApiBaseUrl + "/api/product"
            });
        }
    }
}
