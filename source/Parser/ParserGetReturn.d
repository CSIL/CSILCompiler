mixin template GetReturn(){
	FunReturnAST getReturn(){
		FunReturnAST returns = new FunReturnAST();
		// get past "return"
		verify("return", "expected return value for function");
		// get the return value
		returns.expr = getExpr();
		// get past ';'
		verify(";", "expected semicolon to terminate statement");
		return returns;
	}
}
