enum TokenType{
	 eOF = 0,
	 INTEGER = 1,
	 FLOAT = 2,
	 IDENTIFIER = 3,
	 OPERATOR = 4,
	 STRING = 5,
	 DIVIDER = 6,
	 KEYWORD = 8,
	 INVALID = 7,
};

struct current{
	char* str;
	int val;
};

