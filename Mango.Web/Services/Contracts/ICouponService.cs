using Mango.Web.Models.Dto;

namespace Mango.Web.Services.Contracts
{
    public interface ICouponService
    {

        Task<ResponseDto?> GetAllCoupons();
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCoupon(int id);
    }
}
