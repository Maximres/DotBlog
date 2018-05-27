using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JustBlog.Core.Abstract;
using JustBlog.Core.Concrete;
using Ninject;

namespace JustBlog.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        IKernel _kernel;

        /// <summary>
        /// Pass to param StandardKernel() object
        /// </summary>
        /// <param name="kernel"></param>
        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            _kernel.Unbind<ModelValidatorProvider>();
            AddBindings();
        }

        /// <summary>
        /// Init IoC with StandardKernel class
        /// </summary>
        public NinjectDependencyResolver() : this(new StandardKernel())
        {

        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        /// <summary>
        /// Add bindings in this class
        /// </summary>
        private void AddBindings()
        {
            _kernel.Bind<IBlogRepository>().To<BlogRepository>();
            _kernel.Bind<ICategoryRepository>().To<BlogRepository>();
            _kernel.Bind<IPostRepository>().To<BlogRepository>();
            _kernel.Bind<ITagRepository>().To<BlogRepository>();
        }
    }
}