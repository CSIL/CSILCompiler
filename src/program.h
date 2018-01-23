#pragma once

struct Module {
	enum { 
		procedure,
		function,
		use,
		module
	} tag;
	struct Parts {
		union {
			void* proc,
			void* func,
			void* use,
			void* module
		} part;
		void* next;
	} parts;
};


