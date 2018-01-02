import std.file;

class BufferedReader{
	int curchar = 0;
	char[] buffer;

	this(string fname){
		this.buffer = readText(fname).dup;
	}

	char EOF = cast(char)-1;
	char read(){
		if(this.curchar < this.buffer.length){
			return this.buffer[this.curchar++];
		} else {
			return this.EOF;
		}
	}
	void put(char c){
		if(this.curchar > 0){
			this.buffer[--this.curchar] = c;
		}
	}

}
