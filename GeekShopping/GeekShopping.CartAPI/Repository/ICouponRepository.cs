using GeekShopping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> getCoupon(string couponCode, string token);
    }
}
