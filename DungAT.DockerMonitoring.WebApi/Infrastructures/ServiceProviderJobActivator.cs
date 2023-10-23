using Hangfire;

namespace DungAT.DockerMonitoring.WebApi.Infrastructures;

/// <summary>
/// <see href="https://docs.hangfire.io/en/latest/background-methods/using-ioc-containers.html"/>
/// <see href="https://stackoverflow.com/questions/30036242/no-parameterless-constructor-defined-for-this-object-hangfire-scheduler"/>
/// <see href="https://discuss.hangfire.io/t/no-parameterless-constructor-defined-for-this-object/925"/>
/// <see href="https://discuss.hangfire.io/t/problem-to-setup-dependency-injection-for-recurringjob/7252/2"/>
/// <see href="https://github.com/jhaygood86/Hangfire.MicrosoftDependencyInjection"/>
/// </summary>
public class ServiceProviderJobActivator : JobActivator
{
    private readonly IServiceProvider _serviceProvider;

    public ServiceProviderJobActivator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override object ActivateJob(Type type)
    {
        return _serviceProvider.GetRequiredService(type);
    }
}