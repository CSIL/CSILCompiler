mixin template GetImport(){
	// get an import
	ImportAST getImport(){
		// create it on the heap
		ImportAST Import = new ImportAST();
		// get rid of the keyword
		verify(TokenType.KEYWORD, "expected an \"import\" keyword");
		// get the module referenced
		Import.imodule = getModule();
		// get rid of the semicolon
		verify(";", "expected a semicolon to end statement");
		// return the import to the caller
		return Import;
	}
}
