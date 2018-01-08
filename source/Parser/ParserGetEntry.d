mixin template GetEntry(){
	EntryAST getEntry(){
		EntryAST Entry = new EntryAST();
		// remove the keyword
		verify(TokenType.KEYWORD, "Error, expected 'entry' statement");
		// get the name of the entry function
		Entry.name = tokens.pop();
		// get it off the input
		verify(";", "expected a semicolon to end the statement");
		return Entry;
	}
}
