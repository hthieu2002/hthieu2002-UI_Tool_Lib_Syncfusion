using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.Script.RoslynScript
{
    static class CommandParser
    {
        public static List<StatementSyntax> ParseCommandsToStatements(string[] commandLines)
        {
            var statements = new List<StatementSyntax>();
            foreach (var line in commandLines)
            {
                var exprStmt = ParseLineToExpressionStatement(line);
                if (exprStmt != null)
                    statements.Add(exprStmt);
            }
            return statements;
        }

        private static ExpressionStatementSyntax ParseLineToExpressionStatement(string line)
        {
            int idxOpen = line.IndexOf('(');
            int idxClose = line.IndexOf(')');
            if (idxOpen < 0 || idxClose < 0) return null;

            string methodName = line.Substring(0, idxOpen);
            string argsStr = line.Substring(idxOpen + 1, idxClose - idxOpen - 1);

            // Xử lý args: thay dấu phẩy thành dấu cách, tách ra mảng
            string[] argsParts = argsStr.Replace(",", " ").Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            var arguments = new List<ArgumentSyntax>();
            foreach (var arg in argsParts)
            {
                if (int.TryParse(arg, out int val))
                {
                    arguments.Add(
                        SyntaxFactory.Argument(
                            SyntaxFactory.LiteralExpression(
                                SyntaxKind.NumericLiteralExpression,
                                SyntaxFactory.Literal(val))));
                }
                else
                {
                    // Nếu muốn hỗ trợ kiểu khác thì bổ sung xử lý ở đây
                }
            }

            var invocation = SyntaxFactory.InvocationExpression(
                SyntaxFactory.IdentifierName(methodName),
                SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(arguments)));

            return SyntaxFactory.ExpressionStatement(invocation);
        }
    }
}
