using System.Collections.Generic;
using PhpieSdk.Library;

namespace PhpieSdk;

public class Program
{
    public static void Main(string[] args)
    {
        new PhpieSdkRunner().Run();
    }
    
}
public class PhpieSdkRunner
{
    public void Run()
    {
        List<string> Ignore = new List<string>()
        {
            
        };
        
        new PhpSdkGenerator(new PhpSdkSettings()
        {
            OutputPath = "C:\\",
            LibsPath = null,
            IsViewMessageAboutLoaded = true,
            IgnoreList = Ignore
        }).Execute();
    }

    public void Benchmark()
    {
        PhpSdkBenchmark.Start();
        Run();
        PhpSdkBenchmark.Finish();
        PhpSdkBenchmark.Result();
    }
}