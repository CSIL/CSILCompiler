mixin template GetFunction(){
	// get a function
	FunctionAST getFunction(){
		// create it on the heap
		FunctionAST Function = new FunctionAST();
		verify("function", "expected function declaration");
		// get the name from the token list
		Function.name = getToken(TokenType.IDENTIFIER, "Expected the name of the function");
		// check if there is a argument list
		verify("(", "expected '('");
		// get arguments of the function
		Function.args = getArgs();
		// get past ')'
		verify(")", "expected ')'");
		// get past '{'
		verify("{", "expected '{' to begin block");
		// get the return expression
		while(tokens.peek().type == TokenType.IDENTIFIER){
			Function.statements ~= getStatement();
		}
		if(tokens.peek().value == "return"){
			Function.returns = getReturn();
		}
		// get past '}'
		verify("}", "expected '}' to end function");
		return Function;
	}
}
