using System;
using System.Collections.Generic;

namespace AAMT_Tokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string testCase = "$_mmm_ $idid++ $$ &=// this is   comment \n  + | £ $ ><^  55: _jdjdj $_ $$ $ _ >>/////// \n <<(___kdkdkd)  {name}  0x42DE 0xEgoogle : 0X990A  /* this is a comment *//**hello**  /*****\n***** fdgsdgs*/  / /***************8";
            /*
             List<string> testcases = new List<string>();

            testcases.Add(testCase);
            testcases.Add(testCase2);
            
            foreach(var item in testcases){
                
                Tokenizer t = new Tokenizer(item);
                Tokenizable[] handlers = new Tokenizable[] { new Hexadecimal_Notation(), new InlineCommentTokenizer(), new OperatorsTokenizer() , new MultipleLineCommentTokenizer(), new PHPVarTokenizer() , new WhiteSpaceTokenizer(), new PythonPrivateVarTokenizer(), new IdTokenizer(), new NumberTokenizer(),   new SeperatorTokenizer(), new SymbolsTokenizer() };
                Token token = t.tokenize(handlers);
                
                while (token != null)
                {
                    Console.WriteLine(token.value + " ==> " + token.type +"\n");
                    token = t.tokenize(handlers);
                }
            }

            */
            Tokenizer t = new Tokenizer(testCase);
            Tokenizable[] handlers = new Tokenizable[] { new OperatorsTokenizer(), new InlineCommentTokenizer(), new SeperatorTokenizer(), new MultipleLineCommentTokenizer(), new Hexadecimal_Notation(), new OperatorsTokenizer(),  new PHPVarTokenizer(), new WhiteSpaceTokenizer(), new PythonPrivateVarTokenizer(), new IdTokenizer(), new NumberTokenizer(), new SymbolsTokenizer() };
            Token token = t.tokenize(handlers);

            while (token != null)
            {
                Console.WriteLine(token.value + " ==> " + token.type + "\n");
                token = t.tokenize(handlers);
            }
        }
    }

    public class Token
    {
        public string type; // id | number
        public string value;
        public int position;
        public int lineNumber;
    }
    public abstract class Tokenizable
    {
        public abstract bool tokenizable(Tokenizer t);
        public abstract Token tokenize(Tokenizer t);
    }
    public class Tokenizer
    {
        public string input;
        public int currentPosition;
        public int lineNumber;
        public Tokenizer(string input)
        {
            this.input = input;
            this.currentPosition = -1;
            this.lineNumber = 1;
        }
        public char peek(int def = 1)
        {
            if (this.hasMore(def))
            {
                return this.input[this.currentPosition + def];
            }
            else
            {
                return '\0';
            }
        }

        public char backwardPeek(int def = 1)
        {
           if( (this.currentPosition - def + 1) > -1) {
                return this.input[this.currentPosition - def + 1];
            }

            return '\0';
        }
        public char next()
        {
            char currentChar = this.input[++this.currentPosition];
            if (currentChar == '\n')
            {
                this.lineNumber++;
            }
            return currentChar;
        }
        public bool hasMore(int def = 1)
        {
            return (this.currentPosition + def) < this.input.Length;
        }
        public Token tokenize(Tokenizable[] handlers)
        {
            foreach (var t in handlers)
            {
                if (t.tokenizable(this))
                {
                    return t.tokenize(this);
                }
            }
            //throw new Exception("Unexpected token");
            return null;
        }
    }
    public class NumberTokenizer : Tokenizable
    {
        public override bool tokenizable(Tokenizer t)
        {
            return t.hasMore() && Char.IsDigit(t.peek());
        }
        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "number";
            token.value = "";
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;
            while (t.hasMore() && Char.IsDigit(t.peek()))
            {
                token.value += t.next();
            }
            return token;
        }
    }
    public class WhiteSpaceTokenizer : Tokenizable
    {
        public override bool tokenizable(Tokenizer t)
        {
            return t.hasMore() && Char.IsWhiteSpace(t.peek());
        }
        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "whitespace";
            token.value = "";
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;
            while (t.hasMore() && Char.IsWhiteSpace(t.peek()))
            {
                token.value += t.next();
            }
            return token;
        }
    }
    public class IdTokenizer : Tokenizable
    {
        public override bool tokenizable(Tokenizer t)
        {
            return t.hasMore() && (Char.IsLetter(t.peek()) || t.peek() == '_');
        }
        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "id";
            token.value = "";
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;
            while (t.hasMore() && (Char.IsLetterOrDigit(t.peek()) || t.peek() == '_'))
            {
                token.value += t.next();
            }
            return token;
        }
    }
}
