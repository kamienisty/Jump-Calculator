using Abstractions.Services;
using System;

namespace Services.Services
{
    public class InputValidationService : IInputValidationService
    {
        private readonly IConsolePrintService _printService;

        public InputValidationService(IConsolePrintService printService)
        {
            _printService = printService;
        }
        public bool ValidateInput(string line, out long distance)
        {
            distance = 0;
            return !(EmptyValueCheck(line) || NotInt64Check(line, out distance) || distance < 0);
        }

        private bool EmptyValueCheck(string line)
        {
            if (String.IsNullOrEmpty(line))
            {
                _printService.PrintMessage("Value can't be empty.");
                return true;
            }

            return false;
        }

        private bool NotInt64Check(string line, out long distance)
        {
            if (!Int64.TryParse(line, out distance))
            {
                _printService.PrintMessage("Value isn't numerical or is too big.");
                return true;
            }

            return false;
        }
    }
}
