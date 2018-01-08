mixin template GetArgs(){
	FunArgAST[] getArgs(){
		FunArgAST[] args;
		// check for the end of the list
		while(tokens.peek().value != ")"){
			args ~= getArg();
			// get past comma
			if(tokens.peek().value == ",")
				tokens.pop();
		}
		return args;
	}
}
