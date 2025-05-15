using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.Script.RoslynScript
{
    static class MethodFactory
    {
        // Phương thức tạo Run() chứa tất cả câu lệnh
        public static MethodDeclarationSyntax CreateRunMethod(List<StatementSyntax> statements)
        {
            return SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                    "Run")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block(statements));
        }

        // Tạo class CommandExecutor với các method thao tác và Run()
        public static ClassDeclarationSyntax CreateCommandExecutorClass(MethodDeclarationSyntax runMethod)
        {
            return SyntaxFactory.ClassDeclaration("CommandExecutor")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(
                    CreateRunAdbCommandMethod(),
                    CreateMethodClickXY(),
                    CreateMethodSwipe(),
                    CreateMethodRandomClick(),
                    CreateMethodWait(),
                    runMethod);
        }

        // Method RunAdbCommand dùng trong các method thao tác
        private static MethodDeclarationSyntax CreateRunAdbCommandMethod()
        {
            var param = SyntaxFactory.Parameter(SyntaxFactory.Identifier("args"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)));

            var objCreation = SyntaxFactory.ObjectCreationExpression(
                SyntaxFactory.IdentifierName("ProcessStartInfo"))
                .WithArgumentList(
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.SeparatedList<ArgumentSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                        SyntaxFactory.Argument(
                            SyntaxFactory.LiteralExpression(
                                SyntaxKind.StringLiteralExpression,
                                SyntaxFactory.Literal("adb"))),
                        SyntaxFactory.Token(SyntaxKind.CommaToken),
                        SyntaxFactory.Argument(SyntaxFactory.IdentifierName("args"))
                            }
                        )
                    )
                )
                .WithInitializer(
                    SyntaxFactory.InitializerExpression(SyntaxKind.ObjectInitializerExpression)
                        .AddExpressions(
                            SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.IdentifierName("RedirectStandardOutput"),
                                SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression)),
                            SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.IdentifierName("RedirectStandardError"),
                                SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression)),
                            SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.IdentifierName("UseShellExecute"),
                                SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)),
                            SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.IdentifierName("CreateNoWindow"),
                                SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression))
                        )
                );

            var psiDecl = SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(SyntaxFactory.IdentifierName("var"))
                    .AddVariables(
                        SyntaxFactory.VariableDeclarator("psi")
                            .WithInitializer(SyntaxFactory.EqualsValueClause(objCreation))
                    )
            );

            var processDecl = SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(SyntaxFactory.IdentifierName("var"))
                    .AddVariables(
                        SyntaxFactory.VariableDeclarator("process")
                            .WithInitializer(
                                SyntaxFactory.EqualsValueClause(
                                    SyntaxFactory.InvocationExpression(
                                        SyntaxFactory.IdentifierName("Process.Start"),
                                        SyntaxFactory.ArgumentList(
                                            SyntaxFactory.SingletonSeparatedList(
                                                SyntaxFactory.Argument(SyntaxFactory.IdentifierName("psi"))
                                            )
                                        )
                                    )
                                )
                            )
                    )
            );

            // if (process == null) throw new Exception("Không thể chạy adb");
            var ifProcessNull = SyntaxFactory.IfStatement(
                SyntaxFactory.BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    SyntaxFactory.IdentifierName("process"),
                    SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)
                ),
                SyntaxFactory.ThrowStatement(
                    SyntaxFactory.ObjectCreationExpression(
                        SyntaxFactory.IdentifierName("Exception"))
                    .WithArgumentList(
                        SyntaxFactory.ArgumentList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        SyntaxFactory.Literal("Không thể chạy adb")
                                    )
                                )
                            )
                        )
                    )
                )
            );

            // var output = process.StandardOutput.ReadToEnd();
            var outputDecl = SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(SyntaxFactory.IdentifierName("var"))
                    .AddVariables(
                        SyntaxFactory.VariableDeclarator("output")
                            .WithInitializer(
                                SyntaxFactory.EqualsValueClause(
                                    SyntaxFactory.InvocationExpression(
                                        SyntaxFactory.MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            SyntaxFactory.MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.IdentifierName("process"),
                                                SyntaxFactory.IdentifierName("StandardOutput")
                                            ),
                                            SyntaxFactory.IdentifierName("ReadToEnd")
                                        )
                                    )
                                )
                            )
                    )
            );

            // var error = process.StandardError.ReadToEnd();
            var errorDecl = SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(SyntaxFactory.IdentifierName("var"))
                    .AddVariables(
                        SyntaxFactory.VariableDeclarator("error")
                            .WithInitializer(
                                SyntaxFactory.EqualsValueClause(
                                    SyntaxFactory.InvocationExpression(
                                        SyntaxFactory.MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            SyntaxFactory.MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.IdentifierName("process"),
                                                SyntaxFactory.IdentifierName("StandardError")
                                            ),
                                            SyntaxFactory.IdentifierName("ReadToEnd")
                                        )
                                    )
                                )
                            )
                    )
            );

            // process.WaitForExit();
            var waitForExitExpr = SyntaxFactory.ExpressionStatement(
                SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("process"),
                        SyntaxFactory.IdentifierName("WaitForExit")
                    )
                )
            );

            // if (process.ExitCode != 0) throw new Exception(error);
            var ifExitCodeNotZero = SyntaxFactory.IfStatement(
                SyntaxFactory.BinaryExpression(
                    SyntaxKind.NotEqualsExpression,
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("process"),
                        SyntaxFactory.IdentifierName("ExitCode")
                    ),
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0))
                ),
                SyntaxFactory.ThrowStatement(
                    SyntaxFactory.ObjectCreationExpression(
                        SyntaxFactory.IdentifierName("Exception"))
                    .WithArgumentList(
                        SyntaxFactory.ArgumentList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Argument(
                                    SyntaxFactory.IdentifierName("error")
                                )
                            )
                        )
                    )
                )
            );

            var usingBlock = SyntaxFactory.UsingStatement(
                SyntaxFactory.Block(
                    processDecl,
                    ifProcessNull,
                    outputDecl,
                    errorDecl,
                    waitForExitExpr,
                    ifExitCodeNotZero
                )
            );

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "RunAdbCommand")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword))
                .AddParameterListParameters(param)
                .WithBody(SyntaxFactory.Block(psiDecl, usingBlock));
        }

        // Các method thao tác ADB cơ bản

        public static MethodDeclarationSyntax CreateMethodClickXY()
        {
            var paramX = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "ClickXY")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(paramX, paramY)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("Console.WriteLine($\"Click tại ({x},{y})\");"),
                    SyntaxFactory.ParseStatement("RunAdbCommand($\"shell input tap {x} {y}\");")
                ));
        }

        public static MethodDeclarationSyntax CreateMethodSwipe()
        {
            var paramX1 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x1"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY1 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y1"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramX2 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x2"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY2 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y2"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramDuration = SyntaxFactory.Parameter(SyntaxFactory.Identifier("duration"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "Swipe")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(paramX1, paramY1, paramX2, paramY2, paramDuration)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("Console.WriteLine($\"Swipe từ ({x1},{y1}) đến ({x2},{y2}) trong {duration} ms\");"),
                    SyntaxFactory.ParseStatement("RunAdbCommand($\"shell input swipe {x1} {y1} {x2} {y2} {duration}\");")
                ));
        }

        public static MethodDeclarationSyntax CreateMethodRandomClick()
        {
            var paramX1 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x1"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY1 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y1"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramX2 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x2"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY2 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y2"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "RandomClick")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(paramX1, paramY1, paramX2, paramY2)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("var rand = new Random();"),
                    SyntaxFactory.ParseStatement("int x = rand.Next(x1, x2 + 1);"),
                    SyntaxFactory.ParseStatement("int y = rand.Next(y1, y2 + 1);"),
                    SyntaxFactory.ParseStatement("Console.WriteLine($\"RandomClick tại ({x},{y})\");"),
                    SyntaxFactory.ParseStatement("RunAdbCommand($\"shell input tap {x} {y}\");")
                ));
        }

        public static MethodDeclarationSyntax CreateMethodWait()
        {
            var paramMs = SyntaxFactory.Parameter(SyntaxFactory.Identifier("ms"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "Wait")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(paramMs)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("Console.WriteLine($\"Chờ {ms} ms\");"),
                    SyntaxFactory.ParseStatement("Thread.Sleep(ms);")
                ));
        }
    }
}

