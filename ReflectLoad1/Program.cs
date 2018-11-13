using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectLoad1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    throw new Exception("You must supply at least one filename.");
                }

                foreach (var filename in args)
                {
                    if (!File.Exists(filename))
                    {
                        Console.WriteLine($"Error: file {filename} does not exist");
                    }
                    else
                    {
                        var assembly = Assembly.ReflectionOnlyLoadFrom(filename);
                        var rtv = assembly.ImageRuntimeVersion;
                        Console.WriteLine($"runtime version is {rtv}");
                        var atts = assembly.CustomAttributes;

                        foreach (var att in atts)
                        {
                            if (att.AttributeType.Name.Contains("TargetFrameworkAttribute"))
                            {
                                var cons = att.ConstructorArguments[0];
                                Console.WriteLine($"found a custom attribute: {cons}");
                            }
                            else
                            {
                                Console.WriteLine($"attribute name is {att.AttributeType.Name}");
                            }
                        }
                        Console.WriteLine();


                    }
                }
            }
            catch (Exception ex)
            {
                var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                var progname = Path.GetFileNameWithoutExtension(codeBase);
                Console.WriteLine(ex.Message);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }
    }
}
