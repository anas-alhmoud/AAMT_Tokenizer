using System;
using System.Collections.Generic;
using System.Text;

namespace AAMT_Tokenizer
{

    public class SeperatorTokenizer : Tokenizable
    {
        //[ ]   ( )   { }   ,   :   ;

        //static char[] seperators = {'[',']', '(', ')','{', '}',',', ':',';'};
        string seperators = "[](){},:;";

        public override bool tokenizable(Tokenizer t)
        {
            return t.hasMore() && seperators.Contains(t.peek().ToString());
        }

        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "seperator";
            token.value = "";
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;

            while (t.hasMore() && seperators.Contains(t.peek().ToString()))
            {
                token.value += t.next();
            }

            return token;
        }
    }
    public class Hexadecimal_Notation : Tokenizable
    {
        string hex = "1234567890abcdefABCDEF";
        public override bool tokenizable(Tokenizer t)
        {
            return t.hasMore(3) && hex.Contains(t.peek(3).ToString()) && (t.peek() == '0'&& (t.peek(2)=='X' || t.peek(2) == 'x'));
        }

        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "Hexadecimal Notation";
            token.value = "";
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;

            token.value += t.peek();
            t.next();
            token.value += t.peek();
            t.next();

            while (t.hasMore() && hex.Contains(t.peek().ToString()))
            {
                token.value += t.next();
            }

            return token;
        }
    }

}
