using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;

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
        public static ClassDeclarationSyntax CreateCommandExecutorClass(MethodDeclarationSyntax runMethod, string deviceID, string viewTypeName)
        {
            // Field _deviceID
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

            var viewField = SyntaxFactory.FieldDeclaration(
                SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.IdentifierName(viewTypeName))
                .AddVariables(
                    SyntaxFactory.VariableDeclarator("_view")))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword), SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));

            var deviceIdParam = SyntaxFactory.Parameter(SyntaxFactory.Identifier("deviceID"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)));

            var viewParam = SyntaxFactory.Parameter(SyntaxFactory.Identifier("view"))
                .WithType(SyntaxFactory.IdentifierName(viewTypeName));

            var constructor = SyntaxFactory.ConstructorDeclaration("CommandExecutor")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(deviceIdParam, viewParam)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("_deviceID = deviceID;"),
                    SyntaxFactory.ParseStatement("_view = view;")
                ));

            var clickMethod = CreateMethodClickXY();
            var waitMethod = CreateMethodWait();
            var swipeMethod = CreateMethodSwipe();
            var randomClickMethod = CreateMethodRandomClick();
            var searchAndClickMethod = CreateMethodSearchAndClick();
            var searchWaitClickMethod = CreateMethodSearchWaitClick();
            var searchAndContinueMethod = CreateMethodSearchAndContinue();
            var stopScriptMethod = CretateMethodStopScrit();
            var logMessageMethod = CretateMethodLogMessage();
            var sendText = CreateMethodSendText();
            var randomTextAndSend = CreateMethodRandomTextAndSend();
            var sendTextFromFileDel = CreateMethodSendTextFromFileDel();
            var delTextChar = CreateMethodDelTextChar();
            var delAllText = CreateMethodDelAllText();

            return SyntaxFactory.ClassDeclaration("CommandExecutor")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(
                    deviceIdField,
                    viewField,
                    constructor,
                    clickMethod,
                    waitMethod,
                    swipeMethod,
                    randomClickMethod,
                    searchAndClickMethod,
                    searchWaitClickMethod,
                    searchAndContinueMethod,
                    stopScriptMethod,
                    logMessageMethod,
                    sendText,
                    randomTextAndSend,
                    sendTextFromFileDel,
                    delTextChar,
                    delAllText,
                    runMethod
                );
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
        // method bool Search and continue
        public static MethodDeclarationSyntax CreateMethodSearchAndContinue()
        {
            var paramText = SyntaxFactory.Parameter(SyntaxFactory.Identifier("text"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)));

            return SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.BoolKeyword)),
                    "SearchAndContinue")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(paramText)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("System.IO.Directory.CreateDirectory($\"./{_deviceID}\");"),
                    SyntaxFactory.ParseStatement("ADBService.EnsureFileDeleted($\"./{_deviceID}/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell uiautomator dump /sdcard/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} pull /sdcard/window_dump.xml ./{_deviceID}/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement($"ADBService.WaitFileExists($\"./{{_deviceID}}/window_dump.xml\");"),
                    SyntaxFactory.ParseStatement("var bounds = ADBService.FindBoundsByText($\"./{_deviceID}/window_dump.xml\", text);"),
                    SyntaxFactory.ParseStatement(
                        @"if (bounds == null)
                        {
                            Console.WriteLine($""Text '{text}' không tìm thấy trên thiết bị {_deviceID}."");
                            return false;
                        }"
                    ),
                    SyntaxFactory.ParseStatement("var (x, y) = ADBService.ParseCenter(bounds);"),
                    SyntaxFactory.ParseStatement("return true;")
                ));
        }
        // OCR hình ảnh
        /*
         * 
         */

        // Các vòng lặp điều kiện lệnh 

        // stop script 
        public static MethodDeclarationSyntax CretateMethodStopScrit()
        {
            return SyntaxFactory.MethodDeclaration(
                   SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                   "StopScript")
               .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
               .AddParameterListParameters()
               .WithBody(SyntaxFactory.Block(
                   SyntaxFactory.ParseStatement("return;")
               ));
        }
        // log message 
        public static MethodDeclarationSyntax CretateMethodLogMessage()
        {
            var paramText = SyntaxFactory.Parameter(SyntaxFactory.Identifier("text"))
               .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)));

            return SyntaxFactory.MethodDeclaration(
                   SyntaxFactory.IdentifierName("Task"), // Trả về Task cho async method
                   "Log")
               .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
               .AddParameterListParameters(paramText)
               .WithBody(SyntaxFactory.Block(
                   SyntaxFactory.ParseStatement(@"await _view.UpdateProgressGridView($""{_deviceID}"", text, 5);")
               ));
        }

        // send text 
        public static MethodDeclarationSyntax CreateMethodSendText()
        {
            var paramText = SyntaxFactory.Parameter(SyntaxFactory.Identifier("text"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)));


            return SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                    "SendText")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(paramText)
                .WithBody(SyntaxFactory.Block(
                    SyntaxFactory.ParseStatement("var input = ADBService.EscapeAdbInputText(text);"),
                    SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input text {input}\");")
                    ));
        }
        // random text and send text 15 
        public static MethodDeclarationSyntax CreateMethodRandomTextAndSend()
        {
            var paramRandomCount = SyntaxFactory.Parameter(SyntaxFactory.Identifier("count"))
             .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)))
             .WithDefault(SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(15)
             )));

            return SyntaxFactory.MethodDeclaration(
                  SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                  "RandomTextAndSend")
              .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
              .AddParameterListParameters(paramRandomCount)
              .WithBody(SyntaxFactory.Block(
                  SyntaxFactory.ParseStatement("var input = ADBService.RandomString(count);"),
                  SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input text {input}\");")
                  ));
        }

        // send text file and delete
        public static MethodDeclarationSyntax CreateMethodSendTextFromFileDel()
        {
            var paramUrlFile = SyntaxFactory.Parameter(SyntaxFactory.Identifier("url"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)));

            var statements = new List<StatementSyntax>();

            statements.Add(SyntaxFactory.ParseStatement("System.IO.Directory.CreateDirectory(\"./Resources/script/file\");"));
            statements.Add(SyntaxFactory.ParseStatement("if (!System.IO.File.Exists($\"./Resources/script/file/{url}\")) { Console.WriteLine($\"[Log] File không tồn tại: {url}\"); return; }"));

            statements.Add(SyntaxFactory.ParseStatement("var lines = System.IO.File.ReadAllLines($\"./Resources/script/file/{url}\");"));
            statements.Add(SyntaxFactory.ParseStatement("Console.WriteLine($\"[Log] Đã đọc {lines.Length} dòng từ file.\");"));

            statements.Add(SyntaxFactory.ParseStatement("if (lines.Length == 0) { Console.WriteLine(\"[Log] File rỗng, không có dòng nào để gửi.\"); return; }"));

            statements.Add(SyntaxFactory.ParseStatement("var firstLine = lines[0];"));
            statements.Add(SyntaxFactory.ParseStatement("Console.WriteLine($\"[Log] Dòng đầu tiên sẽ gửi: {firstLine}\");"));

            statements.Add(SyntaxFactory.ParseStatement("SendText(firstLine);"));
            statements.Add(SyntaxFactory.ParseStatement("Console.WriteLine(\"[Log] Đã gửi dòng đầu tiên.\");"));

            statements.Add(SyntaxFactory.ParseStatement("var remainingLines = lines.Skip(1).ToArray();"));
            statements.Add(SyntaxFactory.ParseStatement("System.IO.File.WriteAllLines($\"./Resources/script/file/{url}\", remainingLines);"));
            statements.Add(SyntaxFactory.ParseStatement("Console.WriteLine($\"[Log] Đã xóa dòng đầu và ghi lại file. Còn lại {remainingLines.Length} dòng.\");"));

            return SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                    "SendTextFromFileDel")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(paramUrlFile)
                .WithBody(SyntaxFactory.Block(statements));
        }
        // xóa 1 ký tự
        public static MethodDeclarationSyntax CreateMethodDelTextChar()
        {
            var paramCount = SyntaxFactory.Parameter(SyntaxFactory.Identifier("count"))
                .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            var statements = new List<StatementSyntax>();

            var forStatement = SyntaxFactory.ForStatement(
                    SyntaxFactory.Block(
                        SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input keyevent 67\");")
                    ))
                .WithDeclaration(
                    SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)))
                    .AddVariables(SyntaxFactory.VariableDeclarator("i")
                        .WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0))))))
                .WithCondition(
                    SyntaxFactory.BinaryExpression(SyntaxKind.LessThanExpression,
                        SyntaxFactory.IdentifierName("i"),
                        SyntaxFactory.IdentifierName("count")))
                .WithIncrementors(
                    SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(
                        SyntaxFactory.PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, SyntaxFactory.IdentifierName("i"))));

            statements.Add(forStatement);

            return SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                    "DelTextChar")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(paramCount)
                .WithBody(SyntaxFactory.Block(statements));
        }

        // xóa toàn bộ
        public static MethodDeclarationSyntax CreateMethodDelAllText()
        {
            return SyntaxFactory.MethodDeclaration(
                  SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                  "DelAllText")
              .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
              .AddParameterListParameters()
              .WithBody(SyntaxFactory.Block(
                  SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input keycombination 113 29\");"),
                  SyntaxFactory.ParseStatement("ADBService.ExecuteAdbCommand($\"adb -s {_deviceID} shell input keyevent 67\");")
                  ));
        }
        //
    }
}
