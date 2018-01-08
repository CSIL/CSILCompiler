import Token;

class AST {
	string tostring(){
		// return a default string if not overridden
		return "AST Node";
	}
}

class ProgramAST:AST{
	// en entry point, may be null
	EntryAST entry;
	// a list of the files imported
	ImportAST[] imports;
	// a list of the top-level functions 
	FunctionAST[] functions;

	// get a stringified version of the AST
	override string tostring(){
		// create an empty string
		string ret = "";
		// if there is an entry
		if(this.entry !is null){
			// append it
			ret ~= "Entry Point: ";
			ret ~= this.entry.tostring();
		}
		// append each import
		foreach(ImportAST Import; this.imports){
			ret ~= Import.tostring();
		}
		// append each function
		foreach(FunctionAST Function; this.functions){
			ret ~= Function.tostring();
		}
		// return the whole string
		return ret;
	}
}

class EntryAST: AST{
	Token name;
	// return the name of the function to enter
	override string tostring(){
		return name.value ~ "\n";
	}
}

class ImportAST: AST{
	ModuleAST imodule;
	// return the name of the module imported
	override string tostring(){
		return "Import: " ~ imodule.tostring() ~ "\n";
	}
}

class StatementAST: AST{
	VariableAST lvalue;
	ExprAST expression;

	override string tostring(){
		return lvalue.tostring() ~ ": " ~ expression.tostring();
	}
}

class FunctionAST: AST{
	// the function name
	Token name;
	// a list of arguments
	FunArgAST[] args;
	StatementAST[] statements;
	// a return value
	FunReturnAST returns;
	override string tostring(){
		// get the name of the function
		string value = "Function: " ~ name.value ~ "\n\t";
		// add all the arguments
		foreach(FunArgAST Arg; args){
			value ~= Arg.tostring() ~ "\n\t";
		}
		foreach(StatementAST statement; statements){
			value ~= statement.tostring() ~ "\n\t";
		}
		// return the argumnets with the return value
	       	return value ~ returns.tostring() ~ "\n";
	}
}

class ExprAST: AST {
	// the left operand
	TermAST left;
	// the right operand
	ExprAST right;
	// the operator
	Token op;

	override string tostring(){
		// dont print right if it is null
		if(right is null){
			return left.tostring();
		}
		// return a postfix form of the expression
		return "(" ~ left.tostring() ~  " " ~ op.value ~ " " ~ right.tostring() ~ ")";
	}
}

class TermAST: AST {
	// the left operand
	FactorAST left;
	// the right operand
	TermAST right;
	// the operator
	Token op;

	override string tostring(){
		if(right is null){
			return left.tostring();
		}
		// return a postfix form of the term
		return  "(" ~ left.tostring() ~ " " ~ op.value ~ " " ~ right.tostring() ~ ")";
	}
}

class FactorAST: AST{
	// a numerical vlaue
	Token num;
	// a variable value
	VariableExpressionAST var;
	// an expression
	ExprAST expr;

	override string tostring(){
		// pick which to print
		if(num !is null){
			return num.value;
		} else if(var !is null){
			return var.tostring();
		} else if(expr !is null){
			return expr.tostring();
		}
		return "";
	}
}


		

class FunReturnAST:AST{
	// an expression to return
	ExprAST expr;

	override string tostring(){
		return "Returns: " ~ expr.tostring();
	}
}

class ModuleAST:AST {
	// the name of the module
	Token name;
	// the submodule, if any
	ModuleAST subModule;
	override string tostring(){
		// recurse into submodule to get fully qualified name
		if(subModule !is null){
			return name.value ~ "." ~ subModule.tostring();
		} else {
			return name.value;
		}
	}
}

class FunArgAST: AST{
	// name of the arugment
	Token name;
	// the type of the argumnet
	Token type;
	// return the name and type
	override string tostring(){
		return name.value ~ ": " ~ type.value;
	}
}

class FunctionCallAST: AST{
	Token name;
	ExprAST[] args;

	override string tostring(){
		string value;
		value ~= name.value ~ "(";
		foreach(ExprAST expr; args){
			value ~= expr.tostring() ~ ", ";
		}
		return value ~ ")";
	}
}

class VariableAST: AST{
	Token[] modifiers;
	Token type;
	Token name;

	override string tostring(){
		string value = "";
		if(name !is null){
			value ~= name.value ~ ": ";
		}
		if(type !is null){
			value ~= type.value;
		}
		foreach(Token modifier; modifiers){
			value ~= " " ~ modifier.value;
		}
		return value;
	}
}

class VariableExpressionAST: AST {
	FunctionCallAST func;
	VariableAST var;
	
	override string tostring(){
		if(func !is null){
			return func.tostring();
		} else {
			return var.name.value;
		}
	}
}
