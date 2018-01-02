import std.stdio;
import std.ascii;

import BufferedReader;
import Token;
import Tokenizer;

void main()
{
	BufferedReader reader = new BufferedReader("test.rbc");
	Tokenizer tokenizer = new Tokenizer(reader);

	Token t;
	while((t=tokenizer.getToken()).type != TokenType.EOF){
		writeln(t.type," ", t.value);
	}

	delete tokenizer;
	delete reader;
}
