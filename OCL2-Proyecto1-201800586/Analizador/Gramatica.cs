using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace OCL2_Proyecto1_201800586.Analizador
{
    class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: false)
        {
            #region ER
            StringLiteral CADENA = new StringLiteral("Cadena", "'");
            var ENTERO = new NumberLiteral("Entero");
            var DECIMAL = new RegexBasedTerminal("Decimal", "[0-9]+'.'[0-9]+");
            IdentifierTerminal IDENTIFICADOR = new IdentifierTerminal("ID");
            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n");
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "(*", "*)");
            CommentTerminal comentarioBloque2 = new CommentTerminal("comentarioBloque", "{", "}");
            #endregion

            #region Terminales

            var PTCOMA = ToTerm(";");
            var DOSPT = ToTerm(":");
            var COMA = ToTerm(",");
            var PUNTO = ToTerm(".");

            var PARIZQ = ToTerm("(");
            var PARDER = ToTerm(")");

            var MAS = ToTerm("+");
            var MENOS = ToTerm("-");
            var POR = ToTerm("*");
            var DIV = ToTerm("/");
            var MOD = ToTerm("%");

            var AND = ToTerm("and");
            var OR = ToTerm("or");
            var NOT = ToTerm("not");

            var MAYOR = ToTerm(">");
            var MENOR = ToTerm("<");
            var MAYORIGUAL = ToTerm(">=");
            var MENORIGUAL = ToTerm("<=");
            var IGUAL = ToTerm("=");
            var DIFERENTE = ToTerm("<>");

            var PR_PRINT = ToTerm("write");
            var PR_PRINTLN = ToTerm("writeln");

            var TRUE = ToTerm("true");
            var FALSE = ToTerm("false");

            var PR_IF = ToTerm("if");
            var PR_ELSE = ToTerm("else");
            var THEN = ToTerm("then");

            var PR_DO = ToTerm("do");
            var PR_WHILE = ToTerm("while");

            var PR_CASE = ToTerm("case");
            var PR_OF = ToTerm("of");

            var PR_FOR = ToTerm("for");
            var PR_TO = ToTerm("to");
            var PR_DOWNTO = ToTerm("downto");

            var PR_REPEAT = ToTerm("repeat");
            var PR_UNTIL = ToTerm("until");

            var PR_BREAK = ToTerm("break");
            var PR_CONTINUE = ToTerm("continue");

            var VAR = ToTerm("var");
            var BEGIN = ToTerm("begin");
            var END = ToTerm("end");
            var PROGRAM = ToTerm("program");

            var STRING = ToTerm("string");
            var INTEGER = ToTerm("integer");
            var REAL = ToTerm("real");
            var BOOLEAN = ToTerm("boolean");
            var VOID = ToTerm("void");

            var CONST = ToTerm("const");

            var TYPE = ToTerm("type");
            var PR_OBJECT = ToTerm("object");

            var PR_PROCEDURE = ToTerm("procedure");
            var PR_FUNCTION = ToTerm("function");

            var graficar_ts = ToTerm("graficar_ts");
            var exit = ToTerm("Exit");

            RegisterOperators(1, Associativity.Left, OR);
            RegisterOperators(2, Associativity.Left, AND);
            RegisterOperators(3, Associativity.Left, IGUAL, DIFERENTE);
            RegisterOperators(4, Associativity.Neutral, MAYOR, MENOR, MAYORIGUAL, MENORIGUAL);
            RegisterOperators(5, Associativity.Left, MAS, MENOS);
            RegisterOperators(6, Associativity.Left, POR, DIV, MOD);
            RegisterOperators(7, Associativity.Right, NOT);

            NonGrammarTerminals.Add(comentarioLinea);
            NonGrammarTerminals.Add(comentarioBloque);
            NonGrammarTerminals.Add(comentarioBloque2);

            #endregion

            #region No Terminales
            NonTerminal ini = new NonTerminal("Ini");
            NonTerminal programa = new NonTerminal("Programa");
            NonTerminal instruccion = new NonTerminal("Instruccion");
            NonTerminal instrucciones = new NonTerminal("Instrucciones");
            NonTerminal expresion = new NonTerminal("Expresion");
            NonTerminal expresion_numerica = new NonTerminal("Expresion_Numerica");
            NonTerminal expresion_unaria = new NonTerminal("Expresion_Unaria");
            NonTerminal expresion_logica = new NonTerminal("Expresion_Logica");
            NonTerminal expresion_relacional = new NonTerminal("Expresion_Relacional");
            NonTerminal lista_expresion = new NonTerminal("Lista_Expresion");
            NonTerminal primitiva = new NonTerminal("Primitiva");
            NonTerminal imprimir = new NonTerminal("Imprimir");
            NonTerminal declaracion = new NonTerminal("Declaracion");
            NonTerminal declaracion_compuesta = new NonTerminal("Declaracion_compuesta");
            NonTerminal tipo = new NonTerminal("Tipo");
            NonTerminal lista_id = new NonTerminal("Lista_Id");
            NonTerminal asignacion = new NonTerminal("Asignacion");
            NonTerminal sentencia = new NonTerminal("Sentencia");
            NonTerminal bloque_sentencia = new NonTerminal("Bloque_Sentencia");
            NonTerminal sentencias = new NonTerminal("Sentencias");
            NonTerminal main = new NonTerminal("Main");
            NonTerminal IF = new NonTerminal("If");
            NonTerminal ELSE_IF = new NonTerminal("Lista_if");
            NonTerminal ELSEIF = new NonTerminal("Else_If");
            NonTerminal WHILE = new NonTerminal("While");
            NonTerminal FOR = new NonTerminal("For");
            NonTerminal SWITCH = new NonTerminal("Switch");
            NonTerminal CASOS = new NonTerminal("Casos");
            NonTerminal CASO = new NonTerminal("Caso");
            NonTerminal DEFAULT = new NonTerminal("Default");
            NonTerminal DOWHILE = new NonTerminal("Repeat_Until");
            NonTerminal OBJETO = new NonTerminal("Objeto");
            NonTerminal ATRIBUTO = new NonTerminal("Atributo");
            NonTerminal ATRIBUTOS = new NonTerminal("Atributos");
            NonTerminal CALLOBJETO = new NonTerminal("Call_Objeto");
            NonTerminal CALLFuncion = new NonTerminal("Call_Funcion");
            NonTerminal ASIGNARATRIBUTO = new NonTerminal("Asignar_Atributo");
            NonTerminal procedimiento = new NonTerminal("Procedimiento");
            NonTerminal parametros = new NonTerminal("Parametros");
            NonTerminal parametro = new NonTerminal("Parametro");
            NonTerminal callparametro = new NonTerminal("Call_Parametro");
            NonTerminal funcion = new NonTerminal("Funcion");
            NonTerminal instruccioneslocales = new NonTerminal("instrucciones_locales");
            NonTerminal instruccionlocal = new NonTerminal("instruccion_local");
            NonTerminal constante = new NonTerminal("Constante");
            NonTerminal accsesos = new NonTerminal("Accesos");
            NonTerminal accseso = new NonTerminal("Acceso");
            NonTerminal salir = new NonTerminal("Exit");
            NonTerminal graficar = new NonTerminal("Graficar");
            #endregion

            #region Gramatica
            ini.Rule = programa + instrucciones;

            instrucciones.Rule = MakePlusRule(instrucciones, instruccion);

            instruccion.Rule = declaracion + PTCOMA
                             | OBJETO + PTCOMA
                             | funcion + PTCOMA
                             | constante + PTCOMA
                             | procedimiento + PTCOMA
                             | main;


            programa.Rule = PROGRAM + IDENTIFICADOR + PTCOMA
                          | Empty;

            funcion.Rule = PR_FUNCTION + IDENTIFICADOR + PARIZQ + parametros + PARDER + DOSPT + tipo + PTCOMA + instruccioneslocales + bloque_sentencia;

            procedimiento.Rule = PR_PROCEDURE + IDENTIFICADOR + PARIZQ + parametros + PARDER + PTCOMA + instruccioneslocales + bloque_sentencia;

            instruccioneslocales.Rule = MakeStarRule(instruccioneslocales, instruccionlocal);

            instruccionlocal.Rule = declaracion + PTCOMA
                                  | OBJETO + PTCOMA
                                  | constante + PTCOMA
                                  | funcion + PTCOMA
                                  | procedimiento + PTCOMA;

            parametros.Rule = MakeStarRule(parametros, PTCOMA, parametro);

            parametro.Rule = VAR + lista_id + DOSPT + tipo
                           | lista_id + DOSPT + tipo;

            main.Rule = bloque_sentencia + PUNTO;

            declaracion.Rule = VAR + lista_id + DOSPT + tipo
                             | VAR + IDENTIFICADOR + DOSPT + tipo + declaracion_compuesta;

            declaracion_compuesta.Rule = IGUAL + expresion
                                       | Empty;

            constante.Rule = CONST + IDENTIFICADOR + IGUAL + expresion;

            bloque_sentencia.Rule = BEGIN + sentencias + END;

            sentencias.Rule = MakeStarRule(sentencias, sentencia);

            sentencia.Rule = imprimir + PTCOMA
                           | asignacion + PTCOMA
                           | ASIGNARATRIBUTO + PTCOMA
                           | IF + PTCOMA
                           | WHILE + PTCOMA
                           | FOR + PTCOMA
                           | SWITCH + PTCOMA
                           | DOWHILE + PTCOMA
                           | CALLFuncion + PTCOMA
                           | PR_BREAK + PTCOMA
                           | PR_CONTINUE + PTCOMA
                           | salir + PTCOMA
                           | graficar + PTCOMA;

            asignacion.Rule = IDENTIFICADOR + DOSPT + IGUAL + expresion;

            ASIGNARATRIBUTO.Rule = CALLOBJETO + DOSPT + IGUAL + expresion;

            IF.Rule = PR_IF + expresion + THEN + bloque_sentencia
                    | PR_IF + expresion + THEN + bloque_sentencia + PR_ELSE + bloque_sentencia
                    | PR_IF + expresion + THEN + bloque_sentencia + ELSE_IF
                    | PR_IF + expresion + THEN + bloque_sentencia + ELSE_IF + PR_ELSE + bloque_sentencia;

            ELSE_IF.Rule = MakePlusRule(ELSE_IF, ELSEIF);

            ELSEIF.Rule = PR_ELSE + PR_IF + expresion + THEN + bloque_sentencia;

            WHILE.Rule = PR_WHILE + expresion + PR_DO + bloque_sentencia;

            DOWHILE.Rule = PR_REPEAT + bloque_sentencia + PR_UNTIL + expresion
                         | PR_REPEAT + bloque_sentencia + PTCOMA + PR_UNTIL + expresion;

            FOR.Rule = PR_FOR + asignacion + PR_TO + expresion + PR_DO + bloque_sentencia
                     | PR_FOR + asignacion + PR_DOWNTO + expresion + PR_DO + bloque_sentencia;

            lista_id.Rule = MakePlusRule(lista_id, COMA, IDENTIFICADOR);

            imprimir.Rule = PR_PRINT + PARIZQ + lista_expresion + PARDER
                          | PR_PRINTLN + PARIZQ + lista_expresion + PARDER;

            SWITCH.Rule = PR_CASE + expresion + PR_OF + CASOS + DEFAULT + END;

            CASOS.Rule = MakePlusRule(CASOS, CASO);

            CASO.Rule = expresion + DOSPT + bloque_sentencia + PTCOMA;

            DEFAULT.Rule = PR_ELSE + bloque_sentencia
                         | PR_ELSE + bloque_sentencia + PTCOMA
                         | Empty;

            OBJETO.Rule = TYPE + IDENTIFICADOR + IGUAL + PR_OBJECT + VAR + ATRIBUTOS + END;

            ATRIBUTOS.Rule = MakeStarRule(ATRIBUTOS, ATRIBUTO);

            ATRIBUTO.Rule = lista_id + DOSPT + tipo + PTCOMA;

            lista_expresion.Rule = MakeStarRule(lista_expresion, COMA, expresion);

            expresion.Rule = primitiva
                           | CALLOBJETO
                           | CALLFuncion
                           | expresion_unaria
                           | expresion_numerica
                           | expresion_relacional
                           | expresion_logica
                           | PARIZQ + expresion + PARDER;

            expresion_numerica.Rule = expresion + MAS + expresion
                                    | expresion + MENOS + expresion
                                    | expresion + POR + expresion
                                    | expresion + DIV + expresion
                                    | expresion + MOD + expresion;

            expresion_logica.Rule = expresion + OR + expresion
                                  | expresion + AND + expresion
                                  | NOT + expresion;

            expresion_relacional.Rule = expresion + MAYOR + expresion
                                      | expresion + MENOR + expresion
                                      | expresion + IGUAL + expresion
                                      | expresion + DIFERENTE + expresion
                                      | expresion + MAYORIGUAL + expresion
                                      | expresion + MENORIGUAL + expresion;

            expresion_unaria.Rule = MENOS + expresion;

            primitiva.Rule = ENTERO
                           | DECIMAL
                           | CADENA
                           | TRUE
                           | FALSE
                           | IDENTIFICADOR;

            CALLOBJETO.Rule = IDENTIFICADOR + accsesos;

            accsesos.Rule = MakePlusRule(accsesos, accseso);

            accseso.Rule = PUNTO + IDENTIFICADOR;

            CALLFuncion.Rule = IDENTIFICADOR + PARIZQ + callparametro + PARDER;

            callparametro.Rule = MakeStarRule(callparametro, COMA, expresion);

            salir.Rule = exit + PARIZQ + expresion + PARDER
                       | exit + PARIZQ + PARDER;

            graficar.Rule = graficar_ts + PARIZQ + PARDER;

            tipo.Rule = STRING
                      | INTEGER
                      | REAL
                      | BOOLEAN
                      | IDENTIFICADOR;

            #endregion

            #region Preferencias
            this.Root = ini;
            this.MarkTransient(declaracion_compuesta, bloque_sentencia, tipo, accseso);
            this.MarkPunctuation(PTCOMA, PARDER, PARIZQ, COMA, DOSPT, PUNTO, BEGIN, END);
            #endregion
        }
    }
}
