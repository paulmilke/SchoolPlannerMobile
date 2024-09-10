

namespace MobileApp_C971_LAP2_PaulMilke
{
    public class HttpClientService
    {
        public HttpMessageHandler GetPlatformSpecificHttpClient()
        {
#if ANDROID
            var handler = new Xamarin.Android.Net.AndroidMessageHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            }
            };
            return handler;
#else
     throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
        }
    }
}
