namespace Mango.Web.Utils
{
    public class StaticDetail
    {
        public static string CouponApiBaseUrl { get; set; }
        public static string AuthApiBaseUrl { get; set; }
        public static string ProductApiBaseUrl { get; set; }
        public enum ApiType { GET, POST, PUT, DELETE }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookie = "JWTToken";

    }
}
