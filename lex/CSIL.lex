%{
#include<stdio.h>
#include<string.h>
#include<stdlib.h>
char* copy(char* str);
struct current cur;
FILE* yyin;
#include"../source/tokens.d"
%}

%%
[0-9]+["u""U""s""S"]? { cur.str = copy(yytext); return INTEGER; }
[0-9]*"."?[0-9]+["f""F"]? { cur.str = copy(yytext); return FLOAT; }
[a-zA-Z_][a-zA-Z0-9_]* { cur.str = copy(yytext); return IDENTIFIER; }
"/*"((\*+[^/*])|([^*]))*\**"*/"
\"([^\"\\]*(\\.[^\"\\]*)*)\"|\'([^\'\\]*(\\.[^\'\\]*)*)\' { cur.str = copy(yytext); return STRING; } 
"++"|"--"|"+"|"-"|"*"|"/"|"%"|"&"|"^"|"|"|"||"|"^^"|"&&"|"+="|"-="|"*="|"/="|"%="|"^="|"&="|"|="|"=="|"==="|"<="|"<>"|">=" { cur.str = copy(yytext); return OPERATOR; }
[\t\n ]
. { cur.str = copy(yytext); return INVALID; }
%%

char* copy(char* str){
	free(cur.str);
	cur.str = 0;
	char* ret = (char*)malloc(strlen(str)+1);
	strcpy(ret, str);
	return ret;
}

int yywrap(){
	return 1;
}

void open_lexfile(char* fname){
	cur.str = 0;
	FILE* in = fopen(fname, "r");
	yyin = in;
}
