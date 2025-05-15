using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO.Ports;

namespace WindowsFormsApp.Script.RoslynScript
{
    static class MethodFactory
    {
        // Tạo phương thức Run() với các câu lệnh truyền vào
        public static MethodDeclarationSyntax CreateRunMethod(List<StatementSyntax> statements)
        {
            return SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                    "Run")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block(statements));
        }

        // Tạo class CommandExecutor với ClickXY và Run()
        public static ClassDeclarationSyntax CreateCommandExecutorClass(MethodDeclarationSyntax runMethod, string deviceID)
        {
            // Tạo field deviceID
            var deviceIdField = SyntaxFactory.FieldDeclaration(
                SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)))
                .AddVariables(
                    SyntaxFactory.VariableDeclarator("_deviceID")
                        .WithInitializer(
                            SyntaxFactory.EqualsValueClause(
                                SyntaxFactory.LiteralExpression(
                                    SyntaxKind.StringLiteralExpression,
                                    SyntaxFactory.Literal(deviceID)
                                )
                            )
                        )
                    )
                )
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword), SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));
           
            var clickMethod = CreateMethodClickXY();
            var WaitMethod = CreateMethodWait();

            return SyntaxFactory.ClassDeclaration("CommandExecutor")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(deviceIdField, clickMethod, WaitMethod, runMethod);
        }

        // Phương thức ClickXY(int x, int y)
        public static MethodDeclarationSyntax CreateMethodClickXY()
        {
            var paramX = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.ParseTypeName("System.Threading.Tasks.Task"),
                "ClickXY")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                .AddParameterListParameters(paramX, paramY)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("Console.WriteLine($\"Click tại ({x},{y}) trên thiết bị {_deviceID}\");"),
                    SyntaxFactory.ParseStatement("await ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input tap {x} {y}\");"),
                    SyntaxFactory.ParseStatement("await ViewAutomation.Instance.UpdateProgressGridView(_deviceID, $\"Click tại ({x},{y})\");\r\n")
                ));
        }
        public static MethodDeclarationSyntax CreateMethodWait()
        {
            var paramMs = SyntaxFactory.Parameter(SyntaxFactory.Identifier("ms"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.ParseTypeName("System.Threading.Tasks.Task"),
                "Wait")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                .AddParameterListParameters(paramMs)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("Console.WriteLine($\"Sleep {ms} ms trên thiết bị {_deviceID}\");"),
                    SyntaxFactory.ParseStatement("Thread.Sleep(ms);")
                ));
        }

    }
}
