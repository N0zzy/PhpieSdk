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
        private readonly string _targetFramework = "net7.0";
        private readonly string _pathPhp = String.Empty;
        private readonly string _sdkPath = "/.sdkpath";
        private readonly string _sdkIgnore = "/.sdkignore";
        private readonly string _sharpieSdk = "SharpieSdk";
        
        public Sdk()
        {
            PathSdk = _pathPhp;
            PathRoot = GetPathUtil(Directory.GetCurrentDirectory().ToReversSlash());
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
            LoadLibrary();
            AssemblyIterator();
        }

        private void LoadLibrary()
        {
            string osName = Environment.OSVersion.ToString();
            string libName = osName.Contains("Windows") ? "dll" : "so";
            var path = $"{PathRoot}/bin/Debug/{_targetFramework}";
            "".WriteLn(path);
            foreach (var f in Directory.GetFiles(path, $"*.{libName}"))
            {
                try
                {
                    Assembly.LoadFile(f);
                }
                catch (Exception)
                {
                    //ignore
                }
            }
        }
        
        private void AssemblyIterator()
        {
            if (PathCustom == null)
            {
                disassembler.SetPath(PathRoot.Trim(), PathSdk.Trim());
            }
            else
            {
                disassembler.SetPath(PathCustom.Trim(), PathSdk.Trim());
            }

            "".WriteLn($"root path:  {disassembler._pathRoot}");
            "".WriteLn($"sdk path: {disassembler._pathRoot}{disassembler._pathSdk}");
            
            foreach (var assembly in GetAssemblies())
            {
                if (PathIgnore.IndexOf(assembly.GetName().Name?.Trim()) != -1)
                {
                    continue;
                }
                assembly.GetName().Name.WriteLn("add assembly:");
                try
                {
                    TypeIterator(assembly.GetTypes());
                }
                catch (Exception)
                {
                    "".WriteLn($"Unable to load types form {assembly.GetName().Name}");
                }
            }
        }
        
        private void TypeIterator(Type[] types)
        {
            foreach (Type type in types)
            {
                disassembler.Add(type);
            }
        }

        private String GetPathUtil(String current) {    
            string[] directories = Directory.GetDirectories(current, "*", SearchOption.AllDirectories);
            string separator = _sharpieSdk + "/"; 
            var s = String.Empty;
            foreach (string directory in directories)
            {
                s = directory.ToReversSlash();
                if(s.IndexOf(separator, StringComparison.Ordinal) != -1){
                    return s.ToReversSlash().Split(new string[] { separator }, StringSplitOptions.None)[0];   
                }
            }
            throw new Exception("Path not found");
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


