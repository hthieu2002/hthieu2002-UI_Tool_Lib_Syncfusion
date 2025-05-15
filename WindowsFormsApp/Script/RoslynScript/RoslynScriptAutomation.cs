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
        public static void Run(string commandsFile)
        {
            var commandLines = File.ReadAllLines(commandsFile)
                                   .Where(l => !string.IsNullOrWhiteSpace(l))
                                   .ToArray();

            var statements = CommandParser.ParseCommandsToStatements(commandLines);

            var runMethod = MethodFactory.CreateRunMethod(statements);
            var commandExecutorClass = MethodFactory.CreateCommandExecutorClass(runMethod);
            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers(commandExecutorClass)
                .AddUsings(
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Diagnostics"))
                )
                .NormalizeWhitespace();

            Console.WriteLine(compilationUnit.ToFullString());
            CompilerRunner.CompileAndExecute(compilationUnit);
        }
    }
}
