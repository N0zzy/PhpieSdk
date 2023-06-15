using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SharpieSdk.Library;
using SharpieSdk.Library.Service;

namespace SharpieSdk;

class Program
{
    public static void Main(string[] args)
    {
        new Sdk();
    }

    private class Sdk: AssemblyIterator
    {
        /// settings - will need to create a configuration to get the sdk root
        private readonly string _pathPhp = String.Empty;
        private readonly string _pathBinDebug = "/bin/Debug";
        private readonly string _sdkPath = "/.sdkpath";
        private readonly string _sdkIgnore = "/.sdkignore";
        private readonly string _sharpieSdk = "/SharpieSdk";
        private readonly string _dgspec = "/obj/SharpieSdk.csproj.nuget.dgspec.json";
        
        public Sdk()
        {
            PathSdk = _pathPhp;
            var dir = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName.ToReversSlash();
            PathRoot = dir.Split(_pathBinDebug)[0];
            if (!Regex.IsMatch(PathRoot.ToReversSlash(), $"{_sharpieSdk}$"))
            {
                PathRoot += _sharpieSdk;
            }
            if (File.Exists(PathRoot + _sdkIgnore))
            {
                PathIgnore = File.ReadAllText(PathRoot + _sdkIgnore).ToReversSlash().Split("\r\n").ToList();
            }
            if (File.Exists(PathRoot + _sdkPath))
            {
                PathCustom = File.ReadAllText(PathRoot + _sdkPath).ToReversSlash();
            }
            PathRoot.WriteLn("project path:");
            AssemblyIterator();
        }

        private void AssemblyIterator()
        {
            if (PathCustom == null)
            {
                disassembler.SetPath(PathRoot, PathSdk);
            }
            else
            {
                disassembler.SetPath(PathCustom, PathSdk);
            }
            
            "".WriteLn($"root path {disassembler._pathRoot}");
            "".WriteLn($"sdk path {disassembler._pathRoot}{disassembler._pathSdk}");

            ExtractPackages();
            
            foreach (var assembly in GetAssemblies())
            {
                if (PathIgnore.IndexOf(assembly.GetName().Name?.Trim()) != -1)
                {
                    continue;
                }
                assembly.GetName().Name.WriteLn("add assembly:");
                TypeIterator(assembly.GetTypes());
            }
        }
        
        private void TypeIterator(Type[] types)
        {
            foreach (Type type in types)
            {
                disassembler.Add(type);
            }
        }
        
        private void ExtractPackages()
        {
            string filename;
            manager = new Manager(filename = (PathRoot + _dgspec).ToReversSlash());
            "".WriteLn("open nuget props: " + filename);
            NugetPackagesAssemblyLoader();
        }
        
        private void NugetPackagesAssemblyLoader()
        {
            foreach (var nuget in manager.NugetCollection.Distinct().ToList())
            {
                var name = "";
                try
                {
                    name = nuget.Split("|")[0];
                    Assembly.Load(name);
                    "".WriteLn("add nuget package: " + nuget);
                }
                catch (Exception e)
                {
                    "".WriteLn($"failed nuget package: {name}");
                   Console.WriteLine($"[Error] {e.Message}]");
                }
                
            }
        }
    }
}

public static class SdkExtension
{
    public static void WriteLn(this string s, string v = "") {
        Console.WriteLine($"[SDK] {V(v)}" + s);
    }
        
    public static void WriteLn(this object s, string v = "")
    {
        Console.WriteLine($"[SDK] {V(v)}" + s.ToString());
    }
    
    public static void WriteLn(this string[] s, string v = "")
    {
        Console.WriteLine($"[SDK] {V(v)}" + string.Join(", ", s));
    }

    private static string V(string v)
    {
        return v.Length <= 0 ? "" : $"{v} ";
    }
}


