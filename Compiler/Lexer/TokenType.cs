using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lexer
{
    /// <summary>
    /// the syntactical type of the token
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// Incorrect token
        /// </summary>
        invalid,
        /// <summary>
        /// An integer constant
        /// </summary>
        integer_constant,
        /// <summary>
        /// A floating point constant
        /// </summary>
        floating_constant,
        /// <summary>
        /// A string constant
        /// </summary>
        string_constant,
        /// <summary>
        /// A character constant
        /// </summary>
        character_constant,
        /// <summary>
        /// A text string representing something
        /// </summary>
        identifier,
        /// <summary>
        /// An assignment statement
        /// </summary>
        assign,
        /// <summary>
        /// A language keyword
        /// </summary>
        keyword,
        /// <summary>
        /// Addition operator
        /// </summary>
        add,
        /// <summary>
        /// Subtraction operator
        /// </summary>
        sub,
        /// <summary>
        /// Multiplication operator
        /// </summary>
        mul,
        /// <summary>
        /// Division operator
        /// </summary>
        div,
        /// <summary>
        /// The modulus operator
        /// </summary>
        mod,
        /// <summary>
        /// Exclusive or operator
        /// </summary>
        exor,
        /// <summary>
        /// Inclusive or operator
        /// </summary>
        orop,
        /// <summary>
        /// And operator
        /// </summary>
        and,
        /// <summary>
        /// Increment operator
        /// </summary>
        inc,
        /// <summary>
        /// Decrement operator
        /// </summary>
        dec,
        /// <summary>
        /// += operator
        /// </summary>
        assignplus,
        /// <summary>
        /// -= operator
        /// </summary>
        assignminus,
        /// <summary>
        /// *= operator
        /// </summary>
        assigntimes,
        /// <summary>
        /// /= operator
        /// </summary>
        assigndiv,
        /// <summary>
        /// |= operator
        /// </summary>
        assignor,
        /// <summary>
        /// ^= operator
        /// </summary>
        assignexor,
        /// <summary>
        /// and equal operator
        /// </summary>
        assignand,
        /// <summary>
        /// Shift right
        /// </summary>
        shr,
        /// <summary>
        /// Shift left
        /// </summary>
        shl,
        /// <summary>
        /// End of statement
        /// </summary>
        eos,
        /// <summary>
        /// Dot operator
        /// </summary>
        dot,
        /// <summary>
        /// Left Parenthesis
        /// </summary>
        lparen,
        /// <summary>
        /// Right Parenthesis
        /// </summary>
        rparen,
        /// <summary>
        /// Left square bracket
        /// </summary>
        lsqare,
        /// <summary>
        /// Right square bracket
        /// </summary>
        rsqare,
        /// <summary>
        /// Left curly braces
        /// </summary>
        lbracket,
        /// <summary>
        /// Right curly brace
        /// </summary>
        rbracket,
        /// <summary>
        /// End of File
        /// </summary>
        eof
    }
}
