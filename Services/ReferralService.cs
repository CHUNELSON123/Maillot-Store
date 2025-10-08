using Microsoft.AspNetCore.Http;

namespace MaillotStore.Services
{
    public class ReferralService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string ReferralCookieKey = "MaillotStoreReferralCode";

        public ReferralService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Call this when the app loads to check the URL
        public void SetReferralCodeFromQuery(string? refCode)
        {
            if (!string.IsNullOrEmpty(refCode))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30), // Cookie lasts for 30 days
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true
                };
                _httpContextAccessor.HttpContext?.Response.Cookies.Append(ReferralCookieKey, refCode, cookieOptions);
            }
        }

        // Call this during checkout to get the stored code
        public string? GetReferralCode()
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies[ReferralCookieKey];
        }
    }
}