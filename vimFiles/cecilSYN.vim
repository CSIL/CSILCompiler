if exists("b:current_syntax")
	finish
endif

syn keyword basicLanguageKeywords module modules function functions procedure procedures where end use in out is of defaults to version equals
syn keyword basicBlockKeywords do done if otherwise then
syn keyword booleanKeywords true false and or not xor True False

syn region block start="do" end="done" fold transparent

syn region celComment start="\/\*" end="\*\/"
syn match celComment "//.*$"

syn match syntaxDecType "[a-zA-Z][a-zA-Z]*"
syn match syntaxNum "[0-9][0-9]*"

hi def link basicLanguageKeywords Statement
hi def link basicBlockKeywords PreProc

hi def link celComment Comment

hi def link syntaxDecType Type
hi def link syntaxNum Constant
hi def link booleanKeywords Constant
