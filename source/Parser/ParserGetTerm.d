mixin template GetTerm(){
	TermAST getTerm(){
		TermAST Term = new TermAST();
		// get a factor
		Term.left= getFactor();
		// recurse if it is a multiplication operator
		if(['%','/','*'].canFind(tokens.peek().value)){
			// get the operator
			Term.op = getToken(TokenType.OPERATOR, "Error, expected a mulop");
			// get the next operand
			Term.right = getTerm();
		}
		return Term;
	}
}
