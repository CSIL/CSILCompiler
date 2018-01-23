%{
#include<stdlib.h>
#include<stdio.h>
#include<string.h>
int yyerror(char* s);
int yylex();
#define YYDEBUG 1
%}

%token <value> IDENTIFIER
%token <value> FLOATING INTEGER
%token KW_MODULE KW_MODULES
%token KW_FUNCTION KW_FUNCTIONS
%token KW_PROCEDURE KW_PROCEDURES
%token KW_WHERE KW_END
%token KW_USE
%token KW_IN KW_OUT
%token KW_IS KW_OF
%token KW_WHILE KW_FOR KW_LOOP
%token KW_NOT KW_AND KW_OR KW_XOR
%token KW_IF KW_DO KW_OTHERWISE KW_DONE KW_THEN
%token KW_DEFAULTS KW_TO
%token KW_VERSION
%token RPAREN LPAREN
%token ASSIGN DOT COMMA

%union {
	char* value;
}

%%

start: module |;

module: KW_MODULE IDENTIFIER KW_WHERE module_parts KW_END KW_MODULE;

module_parts:  
	module_part 
	| module_parts module_part
	;

module_part: 
	procedure 
	| function 
	| KW_USE useBlock KW_END KW_USE 
	| module
	;

procedure: KW_PROCEDURE ident KW_WHERE fvars block KW_END KW_PROCEDURE;

function: KW_FUNCTION ident KW_WHERE fvars block KW_END KW_FUNCTION;

ident: IDENTIFIER { printf("IDENTIFIER: %s\n", $1); };

fvars: 
	KW_IN vars KW_OUT vars 
	| KW_IN vars 
	| KW_OUT vars 
	| 
	;

vars: 
	var 
	| var COMMA vars
	;

var: 
	IDENTIFIER 
	| IDENTIFIER KW_IS type 
	| type IDENTIFIER
	;

type: 
	qualifiedTypeName 
	| qualifiedTypeName KW_OF qualifiedTypeName
	;

useBlock: KW_MODULES modules;

modules: 
	moduleDec
	| moduleDec modules
	;
moduleDec: 
	qualifiedTypeName KW_VERSION version 
	| qualifiedTypeName KW_IS IDENTIFIER KW_VERSION version
	;

qualifiedTypeName: 
	IDENTIFIER
	| qualifiedTypeName DOT IDENTIFIER
	;

version: INTEGER DOT INTEGER DOT INTEGER;

block: KW_DO statements KW_DONE;

statements: 
	statement
	| statements statement
	;

statement: 
	expression
	| expression ASSIGN var
	| conditional
	| loop
	;

loop:
	KW_WHILE expression block
	| KW_FOR expression COMMA expression COMMA expression block
	| KW_LOOP block
	;

expression: 
	RPAREN IDENTIFIER args LPAREN
	| INTEGER
	| IDENTIFIER
	| KW_NOT expression
	;

args: 
	expression
	| args expression

conditional: 
	if
	| if else
	| if elif
	| if elif else
	;

if: KW_IF expression KW_THEN block;
elif: KW_OTHERWISE if;
else: KW_OTHERWISE block;


%%

int main(int argc, char *argv[]){
	if(argc == 2){
		FILE* code = fopen(argv[1], "r");
		extern FILE* yyin;
		yyin = code;
	}
	yydebug = 1;
	yyparse();
}

int yyerror(char* s){
	fprintf(stderr, "Error: %s\n", s);
}


