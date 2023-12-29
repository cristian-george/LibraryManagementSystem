// <copyright file="IPropertiesService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IPropertiesService
    /// Implements the <see cref="IService{Properties}" />.
    /// </summary>
    /// <seealso cref="IService{Properties}" />
    public interface IPropertiesService : IService<Properties>
    {
    }
}