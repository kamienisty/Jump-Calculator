using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Services
{
    /// <summary>
    /// Interface for classes that are able to valudate user input
    /// </summary>
    public interface IInputValidationService
    {
        /// <summary>
        /// Method able to validate user given distance
        /// </summary>
        /// <param name="line">User input to validate</param>
        /// <param name="distance">Output paramether for Int64 representation of input</param>
        /// <returns>Returns true when input is valid</returns>
        bool ValidateDistance(string line, out long distance);
    }
}
