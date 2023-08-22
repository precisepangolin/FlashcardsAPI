using System;
using System.Net;

namespace FlashcardsAPI
{
    public class Karol
    {



        public static void isProxyUsed()
        {
            WebProxy defaultProxy = WebRequest.DefaultWebProxy as WebProxy;
            if (defaultProxy != null)
            {
                Uri proxyUri = defaultProxy.Address;
                int port = proxyUri.Port;
                Console.WriteLine("The proxy is using port {0}.", port);
            }
            else
            {
                Console.WriteLine("No proxy is being used.");
            }
            
        }
        
        public static void isProxyUsedTwo()
        {
            IWebProxy defaultProxy = WebRequest.DefaultWebProxy;
            if (defaultProxy != null)
            {
                Console.WriteLine("A proxy is being used.");
            }
            else
            {
                Console.WriteLine("No proxy is being used.");
            }
        }
    }
    
} 

