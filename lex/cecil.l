%{
#include<stdio.h>
#include<string.h>
#include"../parse/cecil.tab.h"
%}

%%

"module" { return KW_MODULE; }
"modules" { return KW_MODULES; }
"function" { return KW_FUNCTION; }
"functions" { return KW_FUNCTIONS; }
"procedure" { return KW_PROCEDURE; }
"procedures" { return KW_PROCEDURES; }

"where" { return KW_WHERE; }
"end" { return KW_END; }

"use" { return KW_USE; }

"in" { return KW_IN; }
"out" { return KW_OUT; }

"is" { return KW_IS; }
"of" { return KW_OF; }
"not" { return KW_NOT; }

"while" { return KW_WHILE; }
"loop" { return KW_LOOP; }
"for" { return KW_FOR; }

"if" { return KW_IF; }
"do" { return KW_DO; }
"otherwise" { return KW_OTHERWISE; }
"done" { return KW_DONE; }
"then" { return KW_THEN; }

"defaults" { return KW_DEFAULTS; }
"to" { return KW_TO; }

"version" { return KW_VERSION; }


"(" { return RPAREN; }
")" { return LPAREN; }

"->" { return ASSIGN; }

"." { return DOT; }

"," { return COMMA; }

[0-9]+ { yylval.value = strdup(yytext); return INTEGER; }
[a-z][a-zA-Z0-9]* { yylval.value = strdup(yytext); return IDENTIFIER; }
[A-Z][a-zA-Z0-9]* { yylval.value = strdup(yytext); return IDENTIFIER; }

[\n\t\r ]
%%

int yywrap(){
	return 1;
}
