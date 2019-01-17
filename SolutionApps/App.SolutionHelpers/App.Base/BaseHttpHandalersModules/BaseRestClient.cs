using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Base.BaseHttpWebClient
{
   
    /// <summary>
    /// The REST client.
    /// </summary>
    public class BaseRestClient
    {
        //
        // Summary:
        //     Initializes a new instance of the System.Net.Http.HttpClient class with a specific
        //     handler.
        //
        // Parameters:
        //   handler:
        //     The HTTP handler stack to use for sending requests.
        public BaseRestClient(HttpMessageHandler handler) { }
        //
        // Summary:
        //     Initializes a new instance of the System.Net.Http.HttpClient class with a specific
        //     handler.
        //
        // Parameters:
        //   handler:
        //     The System.Net.Http.HttpMessageHandler responsible for processing the HTTP response
        //     messages.
        //
        //   disposeHandler:
        //     true if the inner handler should be disposed of by Dispose(),false if you intend
        //     to reuse the inner handler.
        public BaseRestClient(HttpMessageHandler handler, bool disposeHandler) { }

        //
        // Summary:
        //     Gets the headers which should be sent with each request.
        //
        // Returns:
        //     Returns System.Net.Http.Headers.HttpRequestHeaders.The headers which should be
        //     sent with each request.
        public HttpRequestHeaders DefaultRequestHeaders { get; }
        //
        // Summary:
        //     Gets or sets the maximum number of bytes to buffer when reading the response
        //     content.
        //
        // Returns:
        //     Returns System.Int32.The maximum number of bytes to buffer when reading the response
        //     content. The default value for this property is 2 gigabytes.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     The size specified is less than or equal to zero.
        //
        //   T:System.InvalidOperationException:
        //     An operation has already been started on the current instance.
        //
        //   T:System.ObjectDisposedException:
        //     The current instance has been disposed.
        public long MaxResponseContentBufferSize { get; set; }
        //
        // Summary:
        //     Gets or sets the timespan to wait before the request times out.
        //
        // Returns:
        //     Returns System.TimeSpan.The timespan to wait before the request times out.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     The timeout specified is less than or equal to zero and is not System.Threading.Timeout.InfiniteTimeSpan.
        //
        //   T:System.InvalidOperationException:
        //     An operation has already been started on the current instance.
        //
        //   T:System.ObjectDisposedException:
        //     The current instance has been disposed.
        public TimeSpan Timeout { get; set; }

        //
        // Summary:
        //     Cancel all pending requests on this instance.
        public async Task<App.Common.CommonUtility> DeleteAsync(string requestUri)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.DeleteAsync(requestUri);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a DELETE request to the specified Uri with a cancellation token as an asynchronous
        //     operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        public async Task<App.Common.CommonUtility> DeleteAsync(string requestUri, CancellationToken cancellationToken)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.DeleteAsync(requestUri, cancellationToken);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a GET request to the specified Uri as an asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> GetAsync(string requestUri)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.GetAsync(requestUri);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a GET request to the specified Uri with a cancellation token as an asynchronous
        //     operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> GetAsync(string requestUri, CancellationToken cancellationToken)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.GetAsync(requestUri, cancellationToken);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a GET request to the specified Uri with an HTTP completion option as an
        //     asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   completionOption:
        //     An HTTP completion option value that indicates when the operation should be considered
        //     completed.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> GetAsync(string requestUri, HttpCompletionOption completionOption)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.GetAsync(requestUri, completionOption);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a GET request to the specified Uri with an HTTP completion option and a
        //     cancellation token as an asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   completionOption:
        //     An HTTP completion option value that indicates when the operation should be considered
        //     completed.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.GetAsync(requestUri, completionOption, cancellationToken);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a GET request to the specified Uri and return the response body as a byte
        //     array in an asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> GetByteArrayAsync(string requestUri)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.GetByteArrayAsync(requestUri);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a GET request to the specified Uri and return the response body as a stream
        //     in an asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> GetStreamAsync(string requestUri)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.GetStreamAsync(requestUri);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a GET request to the specified Uri and return the response body as a string
        //     in an asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> GetStringAsync(string requestUri)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.GetStringAsync(requestUri);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a POST request to the specified Uri as an asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> PostAsync(string requestUri, HttpContent content)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.PostAsync(requestUri, content);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a POST request with a cancellation token as an asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.PostAsync(requestUri, content, cancellationToken);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a PUT request to the specified Uri as an asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> PutAsync(string requestUri, HttpContent content)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.PutAsync(requestUri, content);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send a PUT request with a cancellation token as an asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        public async Task<App.Common.CommonUtility> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.PutAsync(requestUri, content, cancellationToken);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send an HTTP request as an asynchronous operation.
        //
        // Parameters:
        //   request:
        //     The HTTP request message to send.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The request was null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        public async Task<App.Common.CommonUtility> SendAsync(HttpRequestMessage request)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.SendAsync(request);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send an HTTP request as an asynchronous operation.
        //
        // Parameters:
        //   request:
        //     The HTTP request message to send.
        //
        //   completionOption:
        //     When the operation should complete (as soon as a response is available or after
        //     reading the whole response content).
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The request was null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        public async Task<App.Common.CommonUtility> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.SendAsync(request, completionOption);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        //
        // Summary:
        //     Send an HTTP request as an asynchronous operation.
        //
        // Parameters:
        //   request:
        //     The HTTP request message to send.
        //
        //   completionOption:
        //     When the operation should complete (as soon as a response is available or after
        //     reading the whole response content).
        //
        //   cancellationToken:
        //     The cancellation token to cancel operation.
        //
        // Returns:
        //     Returns System.Threading.Tasks.Task`1.The task object representing the asynchronous
        //     operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The request was null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        public async Task<App.Common.CommonUtility> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                dynamic Mydata;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                    Mydata = await client.SendAsync(request, completionOption, cancellationToken);
                }
                Utility = new App.Common.CommonUtility()
                {
                    Data = Mydata,
                    Message = "Sucess Full Respance",
                    Status = true,
                    ErrorCode = "0",
                };
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }

    }
}