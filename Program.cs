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
        List<object> PreloadList = new List<object>()
        {
            typeof(System.Uri)
        };
        
        
        List<string> Ignore = new List<string>()
        {
            
        };

        new PhpSdkGenerator(new PhpSdkSettings()
        {
            OutputPath = "C:\\",
            LibsPath = null,
            IsViewMessageAboutLoaded = true,
            IsUppercaseNames = false,
            IgnoreList = Ignore,
            PreloadList = PreloadList
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