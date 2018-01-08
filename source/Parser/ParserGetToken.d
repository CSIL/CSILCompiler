mixin template GetToken(){
	Token getToken(TokenType type, string value, string message){
		// check both fields of the token
		if(tokens.peek().type != type && tokens.peek().value !=value){
			// if they are wrong throw exception
			throw new Exception(message);
		} else {
			// throw the correct token at the caller
			return tokens.pop();
		}
	}

	Token getToken(TokenType type, string message){
		// check the type
		if(tokens.peek().type != type){
			// if not equal throw exception
			throw new Exception(message);
		} else {
			// throw the token to the caller
			return tokens.pop();
		}	
	}

	Token getToken(string value, string message){
		// get the token 
		if(tokens.peek().value != value){
			// if wrong token throw exception
			throw new Exception(message);
		} else {
			// return the token
			return tokens.pop();
		}
	}
}
