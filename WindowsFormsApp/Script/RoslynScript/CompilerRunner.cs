using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp.Script.RoslynScript
{
    static class CompilerRunner
    {
        public static void CompileAndExecute(CompilationUnitSyntax compilationUnit)
        {
            var syntaxTree = compilationUnit.SyntaxTree;
            string relativePath = @"./WindowsFormsApp.exe";
            string absolutePath = Path.GetFullPath(relativePath);

            Console.WriteLine(syntaxTree);

            var references = new MetadataReference[]
            {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Thread).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Process).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Services.ADBService).Assembly.Location),
            MetadataReference.CreateFromFile(absolutePath),
            MetadataReference.CreateFromFile(typeof(Form).Assembly.Location)
        };

            var compilation = CSharpCompilation.Create(
      assemblyName: "DynamicAssembly",
      syntaxTrees: new[] { syntaxTree },
      references: references,
      options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));


            using (var ms = new MemoryStream())
            {
                try
                {
                    var result = compilation.Emit(ms);

                    if (!result.Success)
                    {
                        Console.WriteLine("Biên dịch lỗi:");
                        foreach (var diagnostic in result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error))
                        {
                            Console.WriteLine(diagnostic.GetMessage());
                        }
                        return;
                    }

                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    var type = assembly.GetType("CommandExecutor");
                    var instance = Activator.CreateInstance(type);
                    var method = type.GetMethod("Run");

                    method.Invoke(instance, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
