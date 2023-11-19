namespace Mango.Web.Utils
{
    public class StaticDetail
    {
        public static string CouponApiBaseUrl { get; set; }
        public static string AuthApiBaseUrl { get; set; }
        public enum ApiType { GET, POST, PUT, DELETE }
    }
}
