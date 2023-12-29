// <copyright file="Injector.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.Injection
{
    using System.Reflection;
    using Ninject;

    /// <summary>
    /// Class Injector.
    /// </summary>
    public class Injector
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private static StandardKernel kernel;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <typeparam name="T"> Template parameter.
        /// It can be whether a repository interface or a service interface. </typeparam>
        /// <returns> An instance of a repository or a service. </returns>
        public static T Create<T>()
        {
            return kernel.Get<T>();
        }
    }
}