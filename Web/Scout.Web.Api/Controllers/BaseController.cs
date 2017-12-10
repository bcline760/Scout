using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scout.Core.Contract;
using Scout.Core;

namespace Scout.Web.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Execute a service method
        /// </summary>
        /// <typeparam name="TOut">The service method return type</typeparam>
        /// <param name="serviceMethod">A delegate for the service method to be executed</param>
        /// <param name="svcFunction">The name of the service function being performed</param>
        /// <param name="code">The HTTP status code to return on success</param>
        /// <returns></returns>
        protected async Task<IActionResult> ExecuteServiceMethod<TOut>(Func<Task<TOut>> serviceMethod, string svcFunction, ApiStatusCode code)
        {
            IActionResult result = null;

            ApiResponse<TOut> apiResponse = new ApiResponse<TOut>
            {
                Result = Core.OperationResult.Unknown
            };

            try
            {
                var response = await serviceMethod();
                apiResponse.Result = Core.OperationResult.Success;
                apiResponse.ResponseBody = response;

                switch (code)
                {
                    case ApiStatusCode.OK:
                        result = Ok(apiResponse);
                        break;
                    case ApiStatusCode.Created:
                        result = CreatedAtAction(svcFunction, apiResponse);
                        break;
                }
            }
            catch (Exception ex)
            {
                apiResponse.Result = Core.OperationResult.Failure;
                apiResponse.Message = $"Failed to execute service operation {svcFunction}.";
                result = BadRequest(apiResponse);
            }

            return result;
        }

        /// <summary>
        /// Execute a service method
        /// </summary>
        /// <typeparam name="TIn">The service method input parameter</typeparam>
        /// <param name="serviceMethod">A delegate for the service method to be executed</param>
        /// <param name="svcParam">The input parameter for the service method</param>
        /// <param name="svcFunction">The name of the service function being performed</param>
        /// <param name="code">The HTTP status code to return on success</param>
        /// <returns>An HTTP action result with message and status code</returns>
        protected async Task<IActionResult> ExecuteServiceMethod<TIn>(Func<TIn, Task> serviceMethod, TIn svcParam, string svcFunction, ApiStatusCode code)
        {
            IActionResult result = null;

            ApiResponse<bool> apiResponse = new ApiResponse<bool>
            {
                Result = Core.OperationResult.Unknown
            };

            try
            {
                await serviceMethod(svcParam);
                apiResponse.Result = Core.OperationResult.Success;
                apiResponse.ResponseBody = true;

                switch (code)
                {
                    case ApiStatusCode.OK:
                        result = Ok(apiResponse);
                        break;
                    case ApiStatusCode.Created:
                        result = CreatedAtAction(svcFunction, apiResponse);
                        break;
                }
            }
            catch (Exception ex)
            {
                apiResponse.Result = Core.OperationResult.Failure;
                apiResponse.Message = $"Failed to execute service operation {svcFunction}.";
                result = BadRequest(apiResponse);
            }

            return result;
        }

        /// <summary>
        /// Execute a service method.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut">The service method return type</typeparam>
        /// <param name="serviceMethod">A delegate for the service method to be executed</param>
        /// <param name="svcParam">The input parameter for the service method</param>
        /// <param name="svcFunction">The name of the service function being performed</param>
        /// <param name="code">The HTTP status code to return on success</param>
        /// <returns>An HTTP action result with message and status code</returns>
        protected async Task<IActionResult> ExecuteServiceMethod<TIn, TOut>(Func<TIn, Task<TOut>> serviceMethod, TIn svcParam, string svcFunction, ApiStatusCode code)
        {
            IActionResult result = null;

            ApiResponse<TOut> apiResponse = new ApiResponse<TOut>
            {
                Result = Core.OperationResult.Unknown
            };

            try
            {
                var response = await serviceMethod(svcParam);
                apiResponse.Result = Core.OperationResult.Success;
                apiResponse.ResponseBody = response;

                switch (code)
                {
                    case ApiStatusCode.OK:
                        result = Ok(apiResponse);
                        break;
                    case ApiStatusCode.Created:
                        result = CreatedAtAction(svcFunction, apiResponse);
                        break;
                }
            }
            catch (Exception ex)
            {
                apiResponse.Result = Core.OperationResult.Failure;
                apiResponse.Message = $"Failed to execute service operation {svcFunction}.";
                result = BadRequest(apiResponse);
            }

            return result;
        }

        /// <summary>
        /// Execute a service method
        /// </summary>
        /// <typeparam name="TIn">The type for the first service method parameter</typeparam>
        /// <typeparam name="TIn2">The type for the second service method parameter</typeparam>
        /// <typeparam name="TOut">The service method return type</typeparam>
        /// <param name="serviceMethod">A delegate for the service method to be executed</param>
        /// <param name="svcParam">First input parameter of the service method</param>
        /// <param name="svcParam2">Second input parameter of the service method</param>
        /// <param name="svcFunction">The name of the service function being performed</param>
        /// <param name="code">The HTTP status code to return on success</param>
        /// <returns></returns>
        protected async Task<IActionResult> ExecuteServiceMethod<TIn, TIn2, TOut>(Func<TIn, TIn2, Task<TOut>> serviceMethod, TIn svcParam,
            TIn2 svcParam2, string svcFunction, ApiStatusCode code)
        {
            IActionResult result = null;

            ApiResponse<TOut> apiResponse = new ApiResponse<TOut>
            {
                Result = Core.OperationResult.Unknown
            };

            try
            {
                var response = await serviceMethod(svcParam, svcParam2);
                apiResponse.Result = Core.OperationResult.Success;
                apiResponse.ResponseBody = response;

                switch (code)
                {
                    case ApiStatusCode.OK:
                        result = Ok(apiResponse);
                        break;
                    case ApiStatusCode.Created:
                        result= CreatedAtAction(svcFunction, apiResponse);
                        break;
                }

                result = Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Result = Core.OperationResult.Failure;
                apiResponse.Message = $"Failed to execute service operation {svcFunction}.";
                result = BadRequest(apiResponse);
            }

            return result;
        }
    }
}
