mixin template GetStatement(){
	auto getStatement(){
		StatementAST statement = new StatementAST();
		statement.lvalue = getVariable();
		verify("=", "Error, expected an assignment");
		statement.expression = getExpr();
		verify(";", "error, expected a semicolon ending the statement");
		return statement;
	}
}
