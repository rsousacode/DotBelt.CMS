using System.CommandLine.Binding;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlateSSR.Swagger;

internal static class Bind
{
    public static Bind.ServiceProviderBinder<T> FromServiceProvider<T>() where T : notnull
    {
        return Bind.ServiceProviderBinder<T>.Instance;
    }

    internal sealed class ServiceProviderBinder<T> : BinderBase<T> where T : notnull
    {
        public static Bind.ServiceProviderBinder<T> Instance { get; } = new Bind.ServiceProviderBinder<T>();

        protected override T GetBoundValue(BindingContext bindingContext)
        {
            return bindingContext.GetRequiredService<T>();
        }
    }
}
