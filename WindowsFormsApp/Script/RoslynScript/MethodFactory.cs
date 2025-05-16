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
            var swipeMethod = CreateMethodSwipe();
            var randomClick = CreateMethodRandomClick();
            var searchAndClick = CreateMethodSearchAndClick();
            var searchWaitClick = CreateMethodSearchWaitClick();

            return SyntaxFactory.ClassDeclaration("CommandExecutor")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(deviceIdField, clickMethod, WaitMethod, swipeMethod, randomClick, searchAndClick, searchWaitClick, runMethod);
        }

        // Phương thức ClickXY(int x, int y)
        public static MethodDeclarationSyntax CreateMethodClickXY()
        {
            var paramX = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "ClickXY")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                .AddParameterListParameters(paramX, paramY)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("Console.WriteLine($\"Click tại ({x},{y}) trên thiết bị {_deviceID}\");"),
                    SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input tap {x} {y}\");")
                ));
        }
        // wait 
        public static MethodDeclarationSyntax CreateMethodWait()
        {
            var paramMs = SyntaxFactory.Parameter(SyntaxFactory.Identifier("ms"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "Wait")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                .AddParameterListParameters(paramMs)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("Console.WriteLine($\"Sleep {ms} ms trên thiết bị {_deviceID}\");"),
                    SyntaxFactory.ParseStatement("Thread.Sleep(ms);")
                ));
        }
        // swipe 
        public static MethodDeclarationSyntax CreateMethodSwipe()
        {
            var paramX = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x1"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y1"))
               .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramX1 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x2"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY1 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y2"))
               .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramDuration = SyntaxFactory.Parameter(SyntaxFactory.Identifier("duration"))
               .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)))
               .WithDefault(SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(500)
             )
         )
     );

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "Swipe")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                .AddParameterListParameters(paramX, paramY, paramX1, paramY1, paramDuration)
                .WithBody(SyntaxFactory.Block(
                        SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input swipe {x1} {y1} {x2} {y2} {duration}\");")
                    ));
        }
        // random click 
        public static MethodDeclarationSyntax CreateMethodRandomClick()
        {
            var paramX = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramX1 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("x1"))
               .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y"))
               .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            var paramY1 = SyntaxFactory.Parameter(SyntaxFactory.Identifier("y1"))
               .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            return SyntaxFactory.MethodDeclaration(
               SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "RandomClick")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                .AddParameterListParameters(paramX, paramX1, paramY, paramY1)
                .WithBody(SyntaxFactory.Block(
                        SyntaxFactory.ParseStatement("Random rand = new Random();"),
                        SyntaxFactory.ParseStatement("int randX = rand.Next(x, x1);"),
                        SyntaxFactory.ParseStatement("int randY = rand.Next(y, y1);"),
                        SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input tap {randX} {randY}\");")
                    ));
        }
        // find text and click
        public static MethodDeclarationSyntax CreateMethodSearchAndClick()
        {
            var paramText = SyntaxFactory.Parameter(SyntaxFactory.Identifier("text"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)));

            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "SearchAndClick")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(paramText)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("System.IO.Directory.CreateDirectory($\"./{_deviceID}\");"),
                    SyntaxFactory.ParseStatement("ADBService.EnsureFileDeleted($\"./{_deviceID}/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell uiautomator dump /sdcard/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} pull /sdcard/window_dump.xml ./{_deviceID}/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement("ADBService.WaitFileExists($\"./{_deviceID}/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement("var bounds = ADBService.FindBoundsByText($\"./{_deviceID}/window_dump.xml\", text);"),
                    SyntaxFactory.ParseStatement("var (x, y) = ADBService.ParseCenter(bounds);"),
                    SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input tap {x} {y}\");")
                ));
        }

        public static MethodDeclarationSyntax CreateMethodSearchWaitClick()
        {
            var paramText = SyntaxFactory.Parameter(SyntaxFactory.Identifier("text"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)));
            var paramMs = SyntaxFactory.Parameter(SyntaxFactory.Identifier("ms"))
             .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            return SyntaxFactory.MethodDeclaration(
                  SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "SearchWaitClick")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                .AddParameterListParameters(paramText, paramMs)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("System.IO.Directory.CreateDirectory($\"./{_deviceID}\");"),
                    SyntaxFactory.ParseStatement("ADBService.EnsureFileDeleted($\"./{_deviceID}/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell uiautomator dump /sdcard/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} pull /sdcard/window_dump.xml ./{_deviceID}/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement($"ADBService.WaitFileExists($\"./{{_deviceID}}/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement("var bounds = ADBService.FindBoundsByText($\"./{_deviceID}/window_dump.xml\", text);"),
                    SyntaxFactory.ParseStatement("var (x, y) = ADBService.ParseCenter(bounds);"),
                    SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input tap {x} {y}\");"),
                    SyntaxFactory.ParseStatement("Thread.Sleep(ms);")
                    ));
        }
    }
}
