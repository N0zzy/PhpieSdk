using System;
using System.Collections.Generic;
using PhpieSdk.Library;

namespace PhpieSdk;

public class Program
{
    public static void Main(string[] args)
    {
        new PhpieSdkRunner().Run();
        //new PhpieSdkRunner().Benchmark();
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
        
        List<string> IgnoreList = new List<string>()
        {
            "System.Security.Cryptography",
            "System.Collections.Concurrent"
        };

        new PhpSdkGenerator(new PhpSdkSettings()
        {
            OutputPath = "C:\\",
            LibsPath = "E:\\git\\PhpieSdk\\bin\\Debug\\net7.0\\",
            IsCached = false,
            IsViewMessageAboutLoaded = true,
            IsUppercaseNames = false,
            IgnoreList = IgnoreList,
            PreloadList = PreloadList,
            EventType = "\\ClrEvent" 
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