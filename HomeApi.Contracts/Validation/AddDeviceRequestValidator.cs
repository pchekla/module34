using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using HomeApi.Contracts.Models.Devices;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов подключения
    /// </summary>
    public class AddDeviceRequestValidator : AbstractValidator<AddDeviceRequest>
    {
        /// <summary>
        /// Метод, конструктор, устанавливающий правила
        /// </summary>
        public AddDeviceRequestValidator() 
        {
            /* Зададим правила валидации */ 
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("'Name' must not be empty.");
                
            RuleFor(x => x.Manufacturer)
                .NotEmpty().WithMessage("'Manufacturer' must not be empty.");
                
            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("'Model' must not be empty.");
                
            RuleFor(x => x.SerialNumber)
                .NotEmpty().WithMessage("'Serial Number' must not be empty.");
                
            RuleFor(x => x.CurrentVolts)
                .NotEmpty().WithMessage("'Current Volts' must not be empty.")
                .InclusiveBetween(120, 220).WithMessage("'Current Volts' must be between 120 and 220. You entered {PropertyValue}.");
                
            RuleFor(x => x.GasUsage)
                .NotNull().WithMessage("'Gas Usage' must be specified.");
                
            RuleFor(x => x.RoomLocation)
                .NotEmpty().WithMessage("'Room Location' must not be empty.")
                .Must(BeSupported).WithMessage($"Please choose one of the following locations: {string.Join(", ", Values.ValidRooms)}");
        }
        
        /// <summary>
        ///  Метод кастомной валидации для свойства location
        /// </summary>
        private bool BeSupported(string location)
        {
            // Проверим, содержится ли значение в списке допустимых
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}