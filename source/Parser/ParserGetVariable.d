mixin template GetVariable(){
	VariableAST getVariable(){
		VariableAST var = new VariableAST();
		Token[] temp;
		while(tokens.peek().type == TokenType.IDENTIFIER){
			temp ~= getToken(TokenType.IDENTIFIER, "This message should not appear in normal use of the program");
		}
		if(temp.length >= 1){
			var.name = temp[temp.length-1];
		}
		if(temp.length >= 2){
			var.type = temp[temp.length-2];
		}
		if(temp.length > 2){
			var.modifiers = temp[0..temp.length-2];
		}		
		return var;
	}
}
