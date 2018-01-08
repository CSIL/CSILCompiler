mixin template Parse(){
	// parse a complete pgoram
	ProgramAST parse(){
		// create a program in memory
		ProgramAST program = new ProgramAST();
		// check if there is an custom entry defined
		if(tokens.peek().value == "entry"){
			// since there is, get it
			program.entry = getEntry();
		}

		// check if there is an import, function, or variable
		while(tokens.peek().value == "import" || tokens.peek().value == "function"){
			// if it is an import
			if(tokens.peek().value == "import"){
				// get the import
				program.imports ~= getImport();
			} else {
				// get a function
				program.functions ~= getFunction();
			}
		}
		// return the program to the caller;
		return program;
	}
}
