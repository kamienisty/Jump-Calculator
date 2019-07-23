using Abstractions.Services;
using CommonResources;
using System;

namespace Services.Services
{
    /// <summary>
    /// Validator for user inputed distance value
    /// </summary>
    public class InputValidationService : IInputValidationService
    {
        private readonly IConsolePrintService _printService;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="printService">Injected instance of IConsolePrintService implementation</param>
        public InputValidationService(IConsolePrintService printService)
        {
            _printService = printService;
        }

        /// <summary>
        /// Main validation metchod for checking the distance value
        /// </summary>
        /// <param name="line">String input representing numerical distance</param>
        /// <param name="distance">Output paramether for Int64 value from line</param>
        /// <returns>Method returns true if input value was succesfully validated and parsed</returns>
        public bool ValidateDistance(string line, out long distance)
        {
            distance = 0;
            var result = EmptyValueCheck(line);
            result &= Int64Check(line, out distance);
            result &= distance > 0;
            return result;
        }

        /// <summary>
        /// Validator checking if the input value is an empty string
        /// </summary>
        /// <param name="line">String input representing numerical distance</param>
        /// <returns>Method returns true if input value is not empty</returns>
        private bool EmptyValueCheck(string line)
        {
            if (String.IsNullOrEmpty(line))
            {
                _printService.PrintMessage(StringResources.EMPTY_VALUE_MESSAGE);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validator checking if the input value is a proper Int64 value
        /// </summary>
        /// <param name="line">String input representing numerical distance</param>
        /// <param name="distance">Output paramether for Int64 value from line</param>
        /// <returns>Method returns true if input value is an Int64</returns>
        private bool Int64Check(string line, out long distance)
        {
            if (!Int64.TryParse(line, out distance))
            {
                _printService.PrintMessage(StringResources.VALUE_NOT_NUMERICAL);
                return false;
            }

            return true;
        }
    }
}
