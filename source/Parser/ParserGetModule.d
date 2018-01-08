mixin template GetModule(){
	// get a module statement
	ModuleAST getModule(){
		// create it on the heap
		ModuleAST Module = new ModuleAST();
		// get the name of the module
		Module.name = getToken(TokenType.IDENTIFIER, "Error expected identifier");
		// if there is a submodule
		if(tokens.peek().value == "."){
			// pop the period
			verify(".", "Expected a '.' between statements");
			// recurse to get the submodule
			Module.subModule = getModule();
		}
		// return the module to the caller
		return Module;
	}
}
