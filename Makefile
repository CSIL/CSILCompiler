main: app

lex/lex.yy.c: lex/CSIL.lex
	lex --outfile=lex/lex.yy.c lex/CSIL.lex

lex/liblex.so: lex/lex.yy.c
	gcc -fPIC lex/lex.yy.c -o lex/liblex.so -shared

app: lex/liblex.so
	dub build

clean:
	rm -f cslc lex/lex.o lex/liblex.so lex.yy.c
	dub clean
