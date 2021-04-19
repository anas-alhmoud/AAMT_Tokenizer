using System;
using System.Collections.Generic;
using System.Text;

namespace AAMT_Tokenizer
{
    /*
        +=   -=   *=   /=   %=    ^=    ++    --
        ==   !=    <   >    &=   >=   <=
        ||   &&    >>    <<    ?:   
*/

    public class PythonPrivateVarTokenizer : Tokenizable
    {
        public override bool tokenizable(Tokenizer t)
        {
            return t.hasMore(2) && t.peek().Equals('_') && ( t.peek(2) == '_' || Char.IsLetterOrDigit(t.peek(2)) );
        }
        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "Python Private Variable";
            token.value = "";
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;
            // while (t.hasMore() && Char.IsLetterOr(t.peek()))
            while (t.hasMore() && (Char.IsLetterOrDigit(t.peek()) || t.peek() == '_'))
            {
                token.value += t.next();
            }
            return token;
        }
    }


    public class OperatorsTokenizer : Tokenizable
    {
        public List<string> arr =new List<string> {  "+=" ,   "-=" ,  "*=",   "/=" ,  "%=" ,   "^=",    "++" ,   "--",
           "!=", "=="    ,   "&="  , ">=" ,  "<=",
        "||" ,  "&&" ,   ">>" ,   "<<"  ,  "?:" };
        public override bool tokenizable( Tokenizer t)
        {
            
            // String.Concat(t.peek() , t.peek(2)) == arr.Contains()
          
            return t.hasMore() && arr.Contains(String.Concat(t.peek() , t.peek(2))); 
         }
        public override Token tokenize(Tokenizer t)
        {
            Token token = new Token();
            token.type = "Operator";
          
            token.value = "";
            token.position = t.currentPosition;
            token.lineNumber = t.lineNumber;
            
            token.value += String.Concat(t.next(), t.next());
            
           
            return token;
        }
    }

}
