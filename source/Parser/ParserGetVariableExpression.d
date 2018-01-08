mixin template GetVariableExpression(){
	VariableExpressionAST getVariableExpression(){
		VariableExpressionAST expression = new VariableExpressionAST();
		Token name = getToken(TokenType.IDENTIFIER, " Error, expected identifier");
		if(tokens.peek().value == "("){
			expression.func = new FunctionCallAST();
			expression.func.name = name;
			verify("(", "Expected '(' to begin argument list");
			while(tokens.peek().value != ")"){
				expression.func.args ~= getExpr();
				if(tokens.peek().value == ","){
					verify(",", "expected seperator to seperate arguments");
				}
			}
			verify(")", "error, expected ')' to end function call");
		} else {
			expression.var = new VariableAST();
			expression.var.name = name;
		}
		return expression;
	}
}
