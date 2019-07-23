using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Services
{
    public interface IInputValidationService
    {
        bool ValidateInput(string line, out long distance);
    }
}
