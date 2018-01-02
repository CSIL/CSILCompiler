enum TokenType {
	INVALID,
	INTEGER,
	KEYWORD,
	STRING,
	IDENTIFIER,
	OPERATOR,
	DIVIDER,
	EOS,
	EOF

};

class Token {
	TokenType type;
	string value;

	this(TokenType type, string value){
		this.type = type;
		this.value = value;
	}
}
