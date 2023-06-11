using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
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
            if (!Regex.IsMatch(PathRoot, "[\\/\\\\]SharpieSdk$"))
            {
                PathRoot += "/SharpieSdk";
            }
            if (File.Exists(PathRoot + "/.sdkignore"))
            {
                PathIgnore = File.ReadAllText(PathRoot + "/.sdkignore").ToReversSlash().Split("\r\n").ToList();
            }
            if (File.Exists(PathRoot + "/.sdkpath"))
            {
                PathCustom = File.ReadAllText(PathRoot + "/.sdkpath").ToReversSlash();
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


