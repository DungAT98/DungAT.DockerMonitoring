using DungAT.DockerMonitoring.Application;

namespace DungAT.DockerMonitoring.UnitTest;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var testClass = new Class1();
        await testClass.Data();
    }
}