import tokens;
extern (C) void open_lexfile(immutable char* array);
extern (C) int yylex();
extern (C) __gshared current cur;

class Lexer{
	this(string filename){
		import std.string;
		open_lexfile(std.string.toStringz(filename));
	}

	import Token;
	Token getToken(){
		int val = yylex();
		if(val == TokenType.eOF){
			import std.string;
			return new Token(cast(char *)"EOF".toStringz(), val);
		}
		return new Token(cur.str, val);
	}

	Token[] getTokens(){
		Token[] tokens;
		do {
			tokens ~= getToken();
		} while(tokens[tokens.length-1].type != TokenType.eOF);
		return tokens;
	}

}
