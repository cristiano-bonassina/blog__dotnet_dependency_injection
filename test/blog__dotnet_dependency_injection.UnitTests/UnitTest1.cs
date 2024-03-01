using blog__dotnet_dependency_injection;
using Microsoft.Extensions.DependencyInjection;

namespace blog__dotnet_dependency_injection.UnitTests;

public class UnitTest1
{
    private readonly IServiceProvider _serviceProvider;

    public UnitTest1()
    {
        var services = new ServiceCollection();
        services.AddScoped<MyScopedService>();
        services.AddSingleton<MySingletonService>();
        services.AddTransient<MyTransientService>();

        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void Should_MyScopedService_Instances_Be_The_Same_When_Using_Single_Scope()
    {
        var myScopedServiceA = _serviceProvider.GetService<MyScopedService>();
        var myScopedServiceB = _serviceProvider.GetService<MyScopedService>();
        Assert.Same(myScopedServiceA, myScopedServiceB);
    }

    [Fact]
    public void Should_Not_MyScopedService_Instances_Be_The_Same_When_Using_Multiple_Scopes()
    {
        var scopeA = _serviceProvider.CreateScope();
        var myScopedServiceA = scopeA.ServiceProvider.GetService<MyScopedService>();

        var scopeB = _serviceProvider.CreateScope();
        var myScopedServiceB = scopeB.ServiceProvider.GetService<MyScopedService>();
        Assert.NotSame(myScopedServiceA, myScopedServiceB);
    }

    [Fact]
    public void Should_MySingletonService_Instances_Be_The_Same_When_Using_Single_Scope()
    {
        var mySingletonServiceA = _serviceProvider.GetService<MySingletonService>();
        var mySingletonServiceB = _serviceProvider.GetService<MySingletonService>();
        Assert.Same(mySingletonServiceA, mySingletonServiceB);
    }

    [Fact]
    public void Should_MySingletonService_Instances_Be_The_Same_When_Using_Multiple_Scopes()
    {
        var scopeA = _serviceProvider.CreateScope();
        var mySingletonServiceA = scopeA.ServiceProvider.GetService<MySingletonService>();

        var scopeB = _serviceProvider.CreateScope();
        var mySingletonServiceB = scopeB.ServiceProvider.GetService<MySingletonService>();
        Assert.Same(mySingletonServiceA, mySingletonServiceB);
    }

    [Fact]
    public void Should_MyTransientService_Instances_Always_Be_Unique()
    {
        var myTransientServiceA = _serviceProvider.GetService<MyTransientService>();
        var myTransientServiceB = _serviceProvider.GetService<MyTransientService>();
        Assert.NotSame(myTransientServiceA, myTransientServiceB);
    }
}