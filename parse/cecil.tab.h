/* A Bison parser, made by GNU Bison 3.0.4.  */

/* Bison interface for Yacc-like parsers in C

   Copyright (C) 1984, 1989-1990, 2000-2015 Free Software Foundation, Inc.

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.  */

/* As a special exception, you may create a larger work that contains
   part or all of the Bison parser skeleton and distribute that work
   under terms of your choice, so long as that work isn't itself a
   parser generator using the skeleton or a modified version thereof
   as a parser skeleton.  Alternatively, if you modify or redistribute
   the parser skeleton itself, you may (at your option) remove this
   special exception, which will cause the skeleton and the resulting
   Bison output files to be licensed under the GNU General Public
   License without this special exception.

   This special exception was added by the Free Software Foundation in
   version 2.2 of Bison.  */

#ifndef YY_YY_CECIL_TAB_H_INCLUDED
# define YY_YY_CECIL_TAB_H_INCLUDED
/* Debug traces.  */
#ifndef YYDEBUG
# define YYDEBUG 0
#endif
#if YYDEBUG
extern int yydebug;
#endif

/* Token type.  */
#ifndef YYTOKENTYPE
# define YYTOKENTYPE
  enum yytokentype
  {
    IDENTIFIER = 258,
    FLOATING = 259,
    INTEGER = 260,
    KW_MODULE = 261,
    KW_MODULES = 262,
    KW_FUNCTION = 263,
    KW_FUNCTIONS = 264,
    KW_PROCEDURE = 265,
    KW_PROCEDURES = 266,
    KW_WHERE = 267,
    KW_END = 268,
    KW_USE = 269,
    KW_IN = 270,
    KW_OUT = 271,
    KW_IS = 272,
    KW_OF = 273,
    KW_WHILE = 274,
    KW_FOR = 275,
    KW_LOOP = 276,
    KW_NOT = 277,
    KW_AND = 278,
    KW_OR = 279,
    KW_XOR = 280,
    KW_IF = 281,
    KW_DO = 282,
    KW_OTHERWISE = 283,
    KW_DONE = 284,
    KW_THEN = 285,
    KW_DEFAULTS = 286,
    KW_TO = 287,
    KW_VERSION = 288,
    RPAREN = 289,
    LPAREN = 290,
    ASSIGN = 291,
    DOT = 292,
    COMMA = 293
  };
#endif

/* Value type.  */
#if ! defined YYSTYPE && ! defined YYSTYPE_IS_DECLARED

union YYSTYPE
{
#line 27 "cecil.y" /* yacc.c:1909  */

	char* value;

#line 97 "cecil.tab.h" /* yacc.c:1909  */
};

typedef union YYSTYPE YYSTYPE;
# define YYSTYPE_IS_TRIVIAL 1
# define YYSTYPE_IS_DECLARED 1
#endif


extern YYSTYPE yylval;

int yyparse (void);

#endif /* !YY_YY_CECIL_TAB_H_INCLUDED  */
