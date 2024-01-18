using System;
using System.IO;
using PhpieSdk.Library;
using PhpieSdk.Library.Service;

namespace PhpieSdk;

public class Program
{
    public static void Main(string[] args)
    {
        new PhpieSdkRunner().Benchmark();
    }
    
}
public class PhpieSdkRunner
{
    public void Run()
    {
        new PhpScriptGenerator(new Settings() 
        {
            currentPath = Directory.GetCurrentDirectory().ToReversSlash(),
        
            targetBuild = "Debug",
            targetFramework = "net7.0",
        
            sdkName = ".sdk",
            sdkPath = ".sdkpath",
            sdkIgnore = ".sdkignore",
        
            isMakeSdkList = true,
            isMakeSdkFiles = true,
            
            isViewLibsIgnore = true,
            isViewOutputPath = true,
            isViewLibsLoaded = true
            
        }).Execute();
    }

    public void Benchmark()
    {
        var t0 = new DateTimeOffset(DateTime.Now);
        Run();
        var t1 = new DateTimeOffset(DateTime.Now);
        "".BenchmarkWriteLn("Start:  " + t0.Hour + ":" + t0.Minute + ":" + t0.Second + "." + t0.Millisecond);
        "".BenchmarkWriteLn("Finish: " + t1.Hour + ":" + t1.Minute + ":" + t1.Second + "." + t1.Millisecond);
        var diff = t1 - t0;
        "".BenchmarkWriteLn("Total:  " + Math.Round(diff.TotalMilliseconds / 1000, 2) + " sec. [" +  diff.TotalMilliseconds + " ms.]");
    }
}