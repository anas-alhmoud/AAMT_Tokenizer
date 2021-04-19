using System;
using System.Collections.Generic;
using System.Text;

namespace AAMT_Tokenizer
{
    public class PHPVarTokenizer : Tokenizable
    {
        public override bool tokenizable(Tokenizer t)
        {
            return t.hasMore() &&   t.peek() == '$' && (Char.IsLetter(t.peek(2)) || t.peek(2) == '_');
        }

        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "PHP Variable";
            token.value = "";
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;

            token.value += t.next();
            while (t.hasMore() && (Char.IsLetterOrDigit(t.peek()) || t.peek() == '_'))
            {
                token.value += t.next();
            }

            return token;
        }
    }

    public class SymbolsTokenizer : Tokenizable
    {
        public override bool tokenizable(Tokenizer t)
        {
            return t.hasMore() && Char.IsSymbol(t.peek());
        }

        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "Symbol";
            token.value = "";
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;

            while (t.hasMore() && Char.IsSymbol(t.peek()))
            {
                token.value += t.next();
            }

            return token;
        }
    }
}
