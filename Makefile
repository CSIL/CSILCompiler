
all: parse lex cecil

.PHONY:lex
lex: lex/cecil.l
	make -C lex

.PHONY:parse
parse: parse/cecil.y
	make -C parse

cecil: parse/cecil.tab.c lex/lex.yy.c
	gcc $^ -o $@

vim:
	cp vimFiles/cecilFT.vim ~/.vim/ftdetect/cecil.vim
	cp vimFiles/cecilSYN.vim ~/.vim/syntax/cecil.vim

make test: all
	./cecil tests/test.clc

clean: 
	rm -f cecil
	make -C parse clean
	make -C lex clean

