// <copyright file="DomainRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Concretes
{
    using Library.DataLayer.Repository;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DomainLayer;

    /// <summary>
    /// DomainRepository class.
    /// </summary>
    public class DomainRepository : BaseRepository<Domain>, IDomainRepository
    {
    }
}