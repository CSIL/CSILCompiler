import std.conv;
import tokens;
import std.algorithm;

string[] keywords =  [
	"import", "entry", "function", "return"
];

class Token{
	TokenType type;
	string value;

	this(char* value, int type){
		this.type = cast(TokenType)type;
		this.value = value.to!string();
		if(keywords.canFind(this.value)){
			this.type = TokenType.KEYWORD;
		}
	}
}
