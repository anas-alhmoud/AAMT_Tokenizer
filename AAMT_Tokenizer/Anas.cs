using System;
using System.Collections.Generic;
using System.Text;

namespace AAMT_Tokenizer
{
    public class InlineCommentTokenizer : Tokenizable
    {
        public override bool tokenizable(Tokenizer t)
        {
            return t.hasMore(2) && t.peek() == '/' && t.peek(2) == '/';
        }
        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "inline-comment";
            token.value = "";
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;
            while (t.hasMore() && t.peek() != '\n')
            {
                token.value += t.next();
            }
            return token;
        }
    }

    public class MultipleLineCommentTokenizer : Tokenizable
    {
        // check for /*
        public override bool tokenizable(Tokenizer t)
        {
            return t.hasMore(2) && String.Concat(t.peek(), t.peek(2)) == "/*";
        }
        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "multiple-comment";
            token.value = String.Concat(t.next(), t.next());
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;
            while (t.hasMore() &&  String.Concat(t.backwardPeek(2), t.backwardPeek()) != "*/" )
            {
                token.value += t.next();
            }

            if(String.Concat(t.backwardPeek(2), t.backwardPeek()) == "*/") return token;
            return null;
        }
    }
}
