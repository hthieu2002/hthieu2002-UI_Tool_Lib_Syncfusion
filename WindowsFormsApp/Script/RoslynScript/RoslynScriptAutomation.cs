using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.Script.RoslynScript
{
    public static class RoslynScriptAutomation
    {
        public static void Run(string commandsFile, string deviceID)
        {
            var commandLines = File.ReadAllLines(commandsFile)
                                   .Where(l => !string.IsNullOrWhiteSpace(l))
                                   .ToArray();

            var statements = CommandParser.ParseCommandsToStatements(commandLines);

            var runMethod = MethodFactory.CreateRunMethod(statements);
            var commandExecutorClass = MethodFactory.CreateCommandExecutorClass(runMethod, deviceID);
            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers(commandExecutorClass)
                .AddUsings(
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Diagnostics")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Services")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("WindowsFormsApp")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks"))
                )
                .NormalizeWhitespace();

            Console.WriteLine(compilationUnit.ToFullString());
            CompilerRunner.CompileAndExecute(compilationUnit);
        }
    }
}
