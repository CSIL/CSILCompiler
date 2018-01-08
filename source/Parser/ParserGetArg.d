mixin template GetArg(){
		FunArgAST getArg(){
		FunArgAST arg = new FunArgAST();
		// get type
		arg.type = tokens.pop();
		// get value
		arg.name = tokens.pop();
		return arg;
	}
}
