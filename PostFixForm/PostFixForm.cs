using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFixForm
{
    class PostFixForm
    {
        Lexer.Token[] tokens;
        int curindex = 0;

        public PostFixForm(Lexer.Token[] tokens)
        {
            this.tokens = tokens;
            try
            {
                System.Windows.Forms.MessageBox.Show(Expression().ToString(), "Postfix Expression");
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void Eat(string type)
        {
            if (curindex < tokens.Length)
            {
                if (tokens[curindex].GetTokenType() == type)
                {
                    curindex++;
                }
                else
                {
                    throw new Exception("Error, Invalid Token");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("Error, tried to eat nonexistant token");
            }

        }

        public AST.AST Expression()
        {
            AST.AST node = Term();
            while(curindex < tokens.Length && (tokens[curindex].GetTokenType() == "add" || tokens[curindex].GetTokenType() == "sub"))
            {
                Lexer.Token token = tokens[curindex];
                if (token.GetTokenType() == "add") {
                    Eat("add");
                }
                else if(token.GetTokenType() == "sub")
                {
                    Eat("sub");
                }
                node = new AST.BinOpAST(node, Term(), token);
            }
            return node;
        }

        public AST.AST Term()
        {
            AST.AST node = Factor();
            while(curindex < tokens.Length && ( tokens[curindex].GetTokenType() == "mul" || tokens[curindex].GetTokenType() == "div"))
            {
                Lexer.Token token = tokens[curindex];
                if(tokens[curindex].GetTokenType() == "mul")
                {
                    Eat("mul");
                } else if(tokens[curindex].GetTokenType() == "div")
                {
                    Eat("div");
                }
                node = new AST.BinOpAST(node, Factor(), token);
            }
            return node;
        }

        public AST.AST Factor()
        {
            if (curindex >= tokens.Length)
            {
                throw new FormatException("Error, Not enough tokens to parse");
            }
            if (tokens[curindex].GetTokenType() == "integer_constant" || tokens[curindex].GetTokenType() == "floating_constant")
            {
                Eat("integer_constant");
                return new AST.NumAST(tokens[curindex - 1]);
            } else if(tokens[curindex].GetTokenType() == "lparen")
            {
                Eat("lparen");
                AST.AST node = Expression();
                Eat("rparen");
                return node;
            }
            throw new ArgumentException("Error, incorrect token: " + tokens[curindex].GetValue());
        }
    }
}
