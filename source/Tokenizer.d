import Token;
import BufferedReader;
import std.ascii;
import std.algorithm: canFind;

class Tokenizer{
	BufferedReader reader;

	string[] keywords = [
		"entry", "return",
	];

	this(BufferedReader reader){
		this.reader = reader;
	}

	string getName(){

		string s = "";
	       	s ~= reader.read();
		char c;
		while((c=reader.read()).isAlphaNum()){
			s ~= c;
		}
		if(c!= cast(char)0){
			reader.put(c);
		}
		return s;
	}

	string getNum(){
		string s = "";
		s ~= reader.read();
		char c = cast(char)0;
		while((c=reader.read()).isDigit() || c == '_'){
			if(c != '_')
				s ~= c;
		} 
		if(c!= cast(char)0){
			reader.put(c);
		}
		return s;
	}

	string getOp(){
		string s = "";
		char c = reader.read();
		switch(c){
			case '>':
			case '<':
				char next = reader.read();
				if(next == c){
					char third = reader.read();
					if(third == '='){
						s ~= c;
						s ~= next;
						s ~= third;
					} else {
						reader.put(third);
					}
				} else if(next == '='){
					s ~= c;
					s ~= next;
				} else {
					s ~= c;
					reader.put(next);
				}
				break;
			case '+':
			case '-':
				char next = reader.read();
				if(next == c){
					s ~= c;
					s ~= next;
				} else {
					s ~= c;
					reader.put(next);
				}
				break;
			default:
				char next = reader.read();
				if(next == '='){
					s ~= c;
					s ~= next;	
				} else if(c == '<' && next == '>'){
					s ~= c;
					s ~= next;
				} else {
					reader.put(next);
					s ~= c;
				}
				break;
		}		
		return s;
	}

	bool isKeyword(string value){
		return keywords.canFind(value);
	}

	Token getToken(){
		char cur = reader.read();
		while(cur.isWhite()){
			cur = reader.read();
		}
		if(cur.isAlpha()){
			reader.put(cur);
			string value = getName();
			if (isKeyword(value))
				return new Token(TokenType.KEYWORD, value);
			return new Token(TokenType.IDENTIFIER, value);
		} else if(cur.isDigit()){
			reader.put(cur);
			return new Token(TokenType.INTEGER, getNum());
		} else if(cur == cast(char)-1){
			return new Token(TokenType.EOF, "eof");
		} else if(['+', '-', '*', '/', '>', '<', '=', '&', '^', '~', '!', '%'].canFind(cur)){
			reader.put(cur);
			return new Token(TokenType.OPERATOR, getOp());
		} else if(['(', ')', '{', '}', '[', ']'].canFind(cur)){
			string value = "";
			value ~= cur;
			return new Token(TokenType.DIVIDER, value);
		} else if(cur == ';'){
			return new Token(TokenType.EOS, ";");
		}
		string s = "";
		s ~= cur;
		return new Token(TokenType.INVALID, s);
	}
}
