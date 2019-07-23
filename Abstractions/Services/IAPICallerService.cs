using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Services
{
    /// <summary>
    /// Interface for class able to perform API call
    /// </summary>
    public interface IAPICallerService
    {
        /// <summary>
        /// Method able to make api call and return call's result as a specific type
        /// </summary>
        /// <typeparam name="T">Type the result will be deserialized as</typeparam>
        /// <param name="url">URL to make call to</param>
        /// <returns>Call's result as given type</returns>
        Task<T> CallAPI<T>(string url);
    }
}
