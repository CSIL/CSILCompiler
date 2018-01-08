import AST;
import Token;
import tokens;
import ParserStack;
import std.algorithm;
import std.stdio;

// import mixins
import ParserVerify;
import ParserGetToken;
import ParserParse;
import ParserGetImport;
import ParserGetModule;
import ParserGetFunction;
import ParserGetReturn;
import ParserGetExpr;
import ParserGetTerm;
import ParserGetFactor;
import ParserGetArgs;
import ParserGetArg;
import ParserGetEntry;
import ParserGetStatement;
import ParserGetVariable;
import ParserGetVariableExpression;

class Parser{
	ParserStack tokens;
	this (Token[] Tokens){
		// initialize the stack with the list of tokens
		this.tokens = new ParserStack(Tokens);
	}

	mixin Verify;
	mixin GetToken;
	mixin Parse;
	mixin GetImport;
	mixin GetModule;
	mixin GetFunction;
	mixin GetReturn;
	mixin GetExpr;
	mixin GetTerm;
	mixin GetFactor;
	mixin GetArgs;
	mixin GetArg;	
	mixin GetEntry;
	mixin GetVariableExpression;
	mixin GetStatement;
	mixin GetVariable;
}
