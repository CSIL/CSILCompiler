import std.algorithm;
import std.stdio;
import tokens;
import Token;
import AST;

class ParserStack{
	Token[] Tokens;
	int cur = 0;
	// set up self with a list of tokens
	this(Token[] tokens){
		this.Tokens = tokens;
	}

	// peek at the next token
	Token peek(){
		// if we are in the list
		if(cur < Tokens.length){
			// get the token
			return this.Tokens[cur];
		} else {
			// otherwise return a default
			return pop();
		}
	}

	Token pop(){
		// if we are in the list
		if(cur < Tokens.length){
			// send back the token and increase the current
			return this.Tokens[cur++];
		} else {
			// return an end of file signal
			return new Token(cast(char*)"EOF", TokenType.eOF);
		}
	}
}
