mixin template Verify(){
	import std.conv;

	void verify(string value, string message){
		// check that we have the correct value
		if(tokens.peek().value != value){
			// we do not, so throw an errror
			throw new Exception(message);
		} else {
			// get rid of the token
			tokens.pop();
		}
	}

	void verify(TokenType type, string message){
		// check that the type is correct
		if(tokens.peek().type != type){
			// if not throw an error
			throw new Exception(message);
		} else {
			// otherwise throw the token
			tokens.pop();
		}
	}
}
