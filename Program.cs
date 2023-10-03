using System.IO;
using PhpieSdk.Library;
using PhpieSdk.Library.Service;

namespace PhpieSdk;

public class Program
{
    public static void Main(string[] args)
    {
        var test = new PhpieTest();
        
        
        new PhpScriptGenerator(new Settings()
        {
            currentPath = Directory.GetCurrentDirectory().ToReversSlash(),
            
            targetBuild = "Debug",
            targetFramework = "net7.0",
            
            sdkName = ".sdk",
            sdkPath = ".sdkpath",
            sdkIgnore = ".sdkignore",
        })
        .Execute();

        
    }
}

