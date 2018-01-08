import std.stdio;
import Parser;
import Token;

void main(string[] args)
{
	if(args.length > 1){
		import Lexer;
		Lexer lexer = new Lexer(args[1]);
		Token[] tokens = lexer.getTokens();
		Parser parser = new Parser(tokens);
		writeln(parser.parse().tostring());
	} else {
		writeln("Error: USAGE: ", args[0], " filename");
	}
}
