using Mango.Web.Models;
using Mango.Web.Models.Dto;

namespace Mango.Web.Services.Contracts
{
	public interface IProductService
	{

		Task<ResponseDto?> GetAllProductsAsync();
		Task<ResponseDto?> GetProductAsync(string couponCode);
		Task<ResponseDto?> GetProductByIdAsync(int id);
		Task<ResponseDto?> CreateProductAsync(ProductDto couponDto);
		Task<ResponseDto?> UpdateProductAsync(ProductDto couponDto);
		Task<ResponseDto?> DeleteProductAsync(int id);
	}
}
