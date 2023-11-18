using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services.Contracts;
using Mango.Web.Utils;

namespace Mango.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.POST,
                Data = couponDto,
                Url = StaticDetail.BaseUrl + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.DELETE,
                Url = StaticDetail.BaseUrl + "/api/coupon/get-by-code/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.GET,
                Url = StaticDetail.BaseUrl + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.GET,
                Url = StaticDetail.BaseUrl + "/api/coupon/get-by-code/" + couponCode
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.GET,
                Url = StaticDetail.BaseUrl + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.PUT,
                Data = couponDto,
                Url = StaticDetail.BaseUrl + "/api/coupon"
            });
        }
    }
}
