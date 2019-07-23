using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Services
{
    public interface IAPICallerService
    {
        Task<T> CallAPI<T>(string url);
    }
}
