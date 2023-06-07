using System;
using System.IO;
using System.Linq;
using System.Reflection;
using PchpSdkLibrary;
using PchpSdkLibrary.Service;

namespace SharpieSdk;

class Program 
{
    public static void Main(string[] args)
    {
        new Sdk();
    }

    public class Sdk: AssemblyIterator
    {
        /// settings - will need to create a configuration to get the sdk root
        private readonly string pathPhp = "";
        
        public Sdk()
        {
            PathSdk = pathPhp;
            var dir = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName.ToReversSlash();
            PathRoot = dir.Split("/bin/Debug")[0];
            PathRoot.WriteLn("project path:");
            AssemblyIterator();
        }

        private void AssemblyIterator()
        {
            disassembler.SetPath(PathRoot, PathSdk);
            
            "".WriteLn($"root path {disassembler._pathRoot}");
            "".WriteLn($"sdk path {disassembler._pathRoot}{disassembler._pathSdk}");

            ExtractPackages();
            
            foreach (var assembly in GetAssemblies())
            {
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

            manager = new Manager(filename = 
                (PathRoot + "/obj/SharpieSdk.csproj.nuget.dgspec.json")
                .ToReversSlash()
            );

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
        Console.WriteLine($"[SDK] {v} " + s);
    }
        
    public static void WriteLn(this object s, string v = "")
    {
        Console.WriteLine($"[SDK] {v} " + s.ToString());
    }
}


