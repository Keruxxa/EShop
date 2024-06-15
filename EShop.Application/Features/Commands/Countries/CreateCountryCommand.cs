using MediatR;

namespace EShop.Application.Features.Commands.Countries
{
    public class CreateCountryCommand : IRequest<int>
    {
        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }
    }
}
