mixin template GetExpr(){
	ExprAST getExpr(){
		ExprAST Expression = new ExprAST();
		// get a term
		Expression.left = getTerm();
		// recurse if it is plus or minus
		if(['+','-'].canFind(tokens.peek().value)){
			// get the operator
			Expression.op = getToken(TokenType.OPERATOR, "Error, expected operator");
			// get the next operand
			Expression.right = getExpr();
		}
		return Expression;
	}
}
