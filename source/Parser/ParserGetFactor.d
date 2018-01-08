mixin template GetFactor(){
	FactorAST getFactor(){
		FactorAST factor = new FactorAST();
		// if it is a parenthesis
		if(tokens.peek().value == "("){
			// get rid of it
			verify("(", "error, there has to be a '('");
			// get an expression
			factor.expr = getExpr();
			// get the closing parenthesis
			verify(")", "error, there has to be a ')' to end the expression");
		// if it is an identifier
		} else if(tokens.peek().type == TokenType.IDENTIFIER){
			// get the variable
			factor.var = getVariableExpression();
		// if it is a number
		} else if(tokens.peek().type == TokenType.INTEGER){
			// just get it
			factor.num = getToken(TokenType.INTEGER, "Error, expected a number");
		}
		return factor;
	}
}
