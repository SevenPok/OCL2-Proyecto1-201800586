using Irony.Parsing;
using OCL2_Proyecto1_201800586.Arbol.Instrucciones;
using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using OCL2_Proyecto1_201800586.Graphviz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OCL2_Proyecto1_201800586.Analizador
{
    class Sintactico : Grammar
    {
        public static LinkedList<Funcion> funciones = new LinkedList<Funcion>();
        public static LinkedList<Objeto> objetos = new LinkedList<Objeto>();
        public bool analizar(String cadena)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if (raiz != null)
            {
                Form1.consola.Text = "";
                LinkedList<Instruccion> AST;

                if (raiz.ChildNodes.Count >= 1)
                {
                    AST = instrucciones(raiz.ChildNodes.ElementAt(1));
                }
                else
                {
                    AST = instrucciones(raiz.ChildNodes.ElementAt(0));
                }

                TablaSimbolo global = new TablaSimbolo();

                foreach (Instruccion ins in AST)
                {
                    ins.ejeuctar(global);
                }

                if (MessageBox.Show("Desear graficar el AST", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Graficador grafo = new Graficador();
                    grafo.graficar(generarGrafo(raiz));
                }
                funciones.Clear();
                objetos.Clear();
                //Form1.consola.Text += "\nLa lisata tiene: " + funciones.Count.ToString();
                return true;
            }

            return false;
        }

        public String generarGrafo(ParseTreeNode root)
        {
            return Graphviz.Dot.getDot(root);
        }

        public LinkedList<Instruccion> instrucciones(ParseTreeNode actual)
        {
            LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
            foreach(ParseTreeNode nodo in actual.ChildNodes)
            {
                lista.AddLast(instruccion(nodo)); 
            }
            return lista;
        }

        public Instruccion instruccion(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            switch (token)
            {
                case "Objeto":
                    return Struct(actual.ChildNodes.ElementAt(0));
                case "Funcion":
                    return funcion(actual.ChildNodes.ElementAt(0));
                case "Procedimiento":
                    return funcion(actual.ChildNodes.ElementAt(0));
                case "Declaracion":
                    return declaraciones(actual.ChildNodes.ElementAt(0));
                case "Constante":
                    return constante(actual.ChildNodes.ElementAt(0));
                default:
                    return main(actual.ChildNodes.ElementAt(0));
            }
        }

        public Constante constante(ParseTreeNode actual)
        {
            return new Constante(actual.ChildNodes[1].Token.Text, expresion(actual.ChildNodes[3]), actual.ChildNodes[1].Token.Location.Line, actual.ChildNodes[1].Token.Location.Column);
        }

        public Instruccion funcion(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 5)
            {
                Funcion aux = new Funcion(actual.ChildNodes[1].Token.Text, declararParametros(actual.ChildNodes[2]), instrucciones(actual.ChildNodes.ElementAt(3)), sentencias(actual.ChildNodes.ElementAt(4)), Simbolo.Tipo.VOID, actual.ChildNodes[0].Token.Location.Line, actual.ChildNodes[0].Token.Location.Column);
                if (aux.disponible)
                {
                    funciones.AddLast(aux);
                }
                return aux;
            }
            else
            {
                Funcion aux = new Funcion(actual.ChildNodes[1].Token.Text, declararParametros(actual.ChildNodes[2]), instrucciones(actual.ChildNodes.ElementAt(4)), sentencias(actual.ChildNodes.ElementAt(5)), tipoVariable(actual.ChildNodes[3].Token.Text), actual.ChildNodes[0].Token.Location.Line, actual.ChildNodes[0].Token.Location.Column);
                if (aux.disponible)
                {
                    funciones.AddLast(aux);
                }
                return aux;
            }

           
        }

        public LinkedList<Declaracion> declararParametros(ParseTreeNode actual)
        {
            LinkedList<Declaracion> declaracions = new LinkedList<Declaracion>();
            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                if (nodo.ChildNodes.Count == 2)
                {
                    string tipo = nodo.ChildNodes[1].Token.Text;
                    foreach (ParseTreeNode hijo in nodo.ChildNodes[0].ChildNodes)
                    {
                        declaracions.AddLast(new Declaracion(hijo.Token.Text, tipoVariable(tipo), hijo.Token.Location.Line, hijo.Token.Location.Column));
                    }
                }
                else
                {
                    string tipo = nodo.ChildNodes[2].Token.Text;
                    foreach (ParseTreeNode hijo in nodo.ChildNodes[1].ChildNodes)
                    {
                        Declaracion referencia = new Declaracion(hijo.Token.Text, tipoVariable(tipo), hijo.Token.Location.Line, hijo.Token.Location.Column);
                        referencia.referencia = true;
                        declaracions.AddLast(referencia);
                    }
                }
            }
            return declaracions;
        }


        public Operacion llamarFuncion(ParseTreeNode actual)
        {

            LLamadaFuncion aux = new LLamadaFuncion(actual.ChildNodes[0].Token.Text, listaOperacion(actual.ChildNodes.ElementAt(1)), actual.ChildNodes[0].Token.Location.Line, actual.ChildNodes[0].Token.Location.Column);
            return new Operacion(aux, actual.ChildNodes[0].Token.Location.Line, actual.ChildNodes[0].Token.Location.Column);

        }


        public LinkedList<Operacion> listaOperacion(ParseTreeNode actual)
        {
            LinkedList<Operacion> lista = new LinkedList<Operacion>();
            foreach(ParseTreeNode nodo in actual.ChildNodes)
            {
                lista.AddLast((Operacion)expresion(nodo).Clone());
            }
            return lista;
        }

        public LinkedList<Parametro> parametros(ParseTreeNode actual)
        {
            LinkedList<Parametro> lista = new LinkedList<Parametro>();
            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                foreach (ParseTreeNode hijo in nodo.ChildNodes.ElementAt(0).ChildNodes)
                {
                    lista.AddLast(new Parametro(hijo.Token.Text, tipoVariable(nodo.ChildNodes.ElementAt(1).Token.Text), hijo.Token.Location.Line, hijo.Token.Location.Column));
                }
            }
            return lista;
        }

        public Instruccion Struct(ParseTreeNode actual)
        {
            return new Objeto(actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0], atributos(actual.ChildNodes.ElementAt(5)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);   
        }

        public LinkedList<Declaracion> atributos(ParseTreeNode actual)
        {
            LinkedList<Declaracion> lista = new LinkedList<Declaracion>();
            foreach(ParseTreeNode nodo in actual.ChildNodes)
            {
                foreach(ParseTreeNode hijo in nodo.ChildNodes[0].ChildNodes)
                {
                    lista.AddLast(new Declaracion(hijo.Token.Text, nodo.ChildNodes[1].Token.Text, tipoVariable(nodo.ChildNodes[1].Token.Text), hijo.Token.Location.Line, hijo.Token.Location.Column));
                }
            }

            return lista;
        }

        public Instruccion declaraciones(ParseTreeNode actual)
        {
            LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
            string type = actual.ChildNodes.ElementAt(2).ToString().Split(' ')[0];
            if (actual.ChildNodes.Count == 4)
            {
                //Declaracion y asignacion
                string token = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                lista.AddLast(new Declaracion(token, tipoVariable(type), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column));
                lista.AddLast(new Asignacion(token, expresion(actual.ChildNodes.ElementAt(3)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column));
                return new Declaraciones(lista);
            }
            else
            {
                
                if (actual.ChildNodes.ElementAt(1).ChildNodes.Count > 0)
                {
                    
                    //declaracion e inicializacion de varias variables
                    foreach (ParseTreeNode node in actual.ChildNodes.ElementAt(1).ChildNodes)
                    {
                        string token = node.Token.ToString().Split(' ')[0];
                        string tipo = actual.ChildNodes.ElementAt(2).ToString().Split(' ')[0];

                        lista.AddLast(new Declaracion(token, tipo, tipoVariable(type), node.Token.Location.Line, node.Token.Location.Column));
                        
                    }
                    return new Declaraciones(lista);
                }
                else
                {
                    //declaracion e inicializacion una variable
                    string token = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                    string tipo = actual.ChildNodes.ElementAt(2).ToString().Split(' ')[0];
                    lista.AddLast(new Declaracion(token, tipo, tipoVariable(type), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column));
                    return new Declaraciones(lista);
                }
            }
        }

        public Simbolo.Tipo tipoVariable(String tipo)
        {
            switch (tipo)
            {
                case "string":
                    return Simbolo.Tipo.CADENA;
                case "integer":
                    return Simbolo.Tipo.ENTERO;
                case "real":
                    return Simbolo.Tipo.DECIMAL;
                case "boolean":
                    return Simbolo.Tipo.BOOLEANA;
                default:
                    return Simbolo.Tipo.OBJETO;
            }
        }

        public Operacion inicializar(String tipo, int linea, int columna)
        {
            switch (tipo)
            {
                case "string":
                    return new Operacion("", Operacion.Tipo.CADENA, linea, columna);
                case "integer":
                    return new Operacion((Double)0, linea, columna);
                case "real":
                    return new Operacion((Double)0, linea, columna);
                case "boolean":
                    return new Operacion(false, linea, columna);
                default:
                    return new Operacion("null", Operacion.Tipo.OBJETO, linea, columna); 
            }
        }

        public Instruccion main(ParseTreeNode actual)
        {
            return new Main(sentencias(actual.ChildNodes.ElementAt(0)));
        }

        public LinkedList<Instruccion> sentencias(ParseTreeNode actual)
        {
            LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                lista.AddLast(sentencia(nodo));
            }
            return lista;
        }

        public Instruccion sentencia(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            //if(token.ToLower() == "else") token = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
            switch (token)
            {
                case "Asignacion":
                    return asignar(actual.ChildNodes.ElementAt(0));
                case "break":
                    return new Break(actual.ChildNodes[0].Token.Location.Line, actual.ChildNodes[0].Token.Location.Column);
                case "Asignar_Atributo":
                    return asignarAtributo(actual.ChildNodes.ElementAt(0));
                case "While":
                    return mientras(actual.ChildNodes.ElementAt(0));
                case "Repeat_Until":
                    return hacerMientras(actual.ChildNodes.ElementAt(0));
                case "Call_Funcion":
                    return ((Operacion)llamarFuncion(actual.ChildNodes.ElementAt(0))).llamada;
                case "If":
                    if(actual.ChildNodes.ElementAt(0).ChildNodes.Count == 4)
                    {
                        return IF(actual.ChildNodes.ElementAt(0));
                    }
                    else if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 5)
                    {
                        return ELSEIF(actual.ChildNodes.ElementAt(0));
                    }
                    else if (actual.ChildNodes.ElementAt(0).ChildNodes.Count == 6)
                    {
                        return ELSE(actual.ChildNodes.ElementAt(0));
                    }
                    else
                    {
                        return ELSEIFELSE(actual.ChildNodes.ElementAt(0));
                    }
                case "Switch":
                    return Switch(actual.ChildNodes.ElementAt(0));
                case "For":
                    return para(actual.ChildNodes.ElementAt(0));
                default:
                    return imprimir(actual.ChildNodes.ElementAt(0));
            }
        }

        public Instruccion para(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(2).ToString().Split(' ')[0];
            if(token.ToLower() == "to")
            {
                return new FOR((Asignacion)asignar(actual.ChildNodes.ElementAt(1)), expresion(actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(2)), expresion(actual.ChildNodes.ElementAt(3)), 1, sentencias(actual.ChildNodes.ElementAt(5)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
            }
            return new FOR((Asignacion)asignar(actual.ChildNodes.ElementAt(1)), expresion(actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(2)), expresion(actual.ChildNodes.ElementAt(3)), -1, sentencias(actual.ChildNodes.ElementAt(5)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }
        public Instruccion asignar(ParseTreeNode actual)
        {
            return new Asignacion(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }

        public Instruccion asignarAtributo(ParseTreeNode actual)
        {
            string identificador = actual.ChildNodes[0].ChildNodes[0].Token.Text;
            LinkedList<String> atributos = new LinkedList<string>();

            foreach (ParseTreeNode nodo in actual.ChildNodes[0].ChildNodes[1].ChildNodes)
            {
                atributos.AddLast(nodo.Token.Text);
            }
            return new AsignarAtributo(identificador, atributos, expresion(actual.ChildNodes[2]), actual.ChildNodes[0].ChildNodes[0].Token.Location.Line, actual.ChildNodes[0].ChildNodes[0].Token.Location.Column);
            //return new Asignacion(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0], expresion(actual.ChildNodes.ElementAt(3)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }

        public Instruccion IF(ParseTreeNode actual)
        {
            return new IF(expresion(actual.ChildNodes.ElementAt(1)), sentencias(actual.ChildNodes.ElementAt(3)));
        }

        public Instruccion ELSEIF(ParseTreeNode actual)
        {
            LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
            foreach(ParseTreeNode nodo in actual.ChildNodes[4].ChildNodes)
            {
                lista.AddLast(new IF(expresion(nodo.ChildNodes.ElementAt(2)), sentencias(nodo.ChildNodes.ElementAt(4))));
            }
            return new IF(expresion(actual.ChildNodes.ElementAt(1)), sentencias(actual.ChildNodes.ElementAt(3)), null, lista);
        }
        public Instruccion ELSE(ParseTreeNode actual)
        {
            return new IF(expresion(actual.ChildNodes.ElementAt(1)), sentencias(actual.ChildNodes.ElementAt(3)), sentencias(actual.ChildNodes.ElementAt(5)));
        }

        public Instruccion ELSEIFELSE(ParseTreeNode actual)
        {
            LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
            foreach (ParseTreeNode nodo in actual.ChildNodes.ElementAt(4).ChildNodes)
            {
                lista.AddLast(new IF(expresion(nodo.ChildNodes.ElementAt(2)), sentencias(nodo.ChildNodes.ElementAt(4))));
            }
            return new IF(expresion(actual.ChildNodes.ElementAt(1)), sentencias(actual.ChildNodes.ElementAt(3)), sentencias(actual.ChildNodes.ElementAt(6)), lista);
        }
        public Instruccion mientras(ParseTreeNode actual)
        {
            return new While(expresion(actual.ChildNodes.ElementAt(1)), sentencias(actual.ChildNodes.ElementAt(3)));
        }

        public Instruccion hacerMientras(ParseTreeNode actual)
        {
            return new DoWhile(sentencias(actual.ChildNodes.ElementAt(1)), expresion(actual.ChildNodes.ElementAt(3)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }
        public Instruccion imprimir(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            switch (token)
            {
                case "writeln":
                    return new Imprimir(listaExpresion(actual.ChildNodes.ElementAt(1)),"\n");
                default:
                    return new Imprimir(listaExpresion(actual.ChildNodes.ElementAt(1)), "");
            }
        }

        public Instruccion Switch(ParseTreeNode actual)
        {
            if(actual.ChildNodes.ElementAt(4).ChildNodes.Count > 0){
                return new SWITCH(expresion(actual.ChildNodes.ElementAt(1)), listaCasos(actual.ChildNodes.ElementAt(3)), Default(actual.ChildNodes.ElementAt(4)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
            }
            return new SWITCH(expresion(actual.ChildNodes.ElementAt(1)), listaCasos(actual.ChildNodes.ElementAt(3)), null, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }

        public LinkedList<Caso> listaCasos(ParseTreeNode actual)
        {
            LinkedList<Caso> lista = new LinkedList<Caso>();
            foreach(ParseTreeNode nodo in actual.ChildNodes)
            {
                lista.AddLast(new Caso(expresion(nodo.ChildNodes.ElementAt(0)), sentencias(nodo.ChildNodes.ElementAt(1))));
            }
            return lista;
        }

        public LinkedList<Instruccion> Default(ParseTreeNode actual)
        {
            return sentencias(actual.ChildNodes.ElementAt(1));
        }
        public LinkedList<Instruccion> listaExpresion(ParseTreeNode actual)
        {
            LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
            foreach(ParseTreeNode node in actual.ChildNodes)
            {
                lista.AddLast(expresion(node));
            }
            return lista;
        }

        public Operacion expresion(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            switch (token)
            {
                case "Expresion":
                    return expresion(actual.ChildNodes.ElementAt(0));
                case "Expresion_Unaria":
                    return expresionUnaria(actual.ChildNodes.ElementAt(0));
                case "Expresion_Numerica":
                    return expresionNumerica(actual.ChildNodes.ElementAt(0));
                case "Expresion_Relacional":
                    return expresionRelacional(actual.ChildNodes.ElementAt(0));
                case "Expresion_Logica":
                    return expresionLogica(actual.ChildNodes.ElementAt(0));
                case "Call_Objeto":
                    return callObjeto(actual.ChildNodes.ElementAt(0));
                case "Call_Funcion":
                    return llamarFuncion(actual.ChildNodes.ElementAt(0));
                default:
                    return primitivo(actual.ChildNodes.ElementAt(0));
            }
        }

        public Operacion callObjeto(ParseTreeNode actual)
        {
            string identificador = actual.ChildNodes[0].Token.Text;
            LinkedList<String> atributos = new LinkedList<string>();
            
            foreach(ParseTreeNode nodo in actual.ChildNodes[1].ChildNodes)
            {
                atributos.AddLast(nodo.Token.Text);
            }
            CallAtributo atributo = new CallAtributo(identificador, atributos, actual.ChildNodes[0].Token.Location.Line, actual.ChildNodes[0].Token.Location.Column);
            return new Operacion(atributo, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }
        

        public Operacion expresionNumerica(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
            switch (token)
            {
                case "%":
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.MODULAR, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "/":
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.DIVISION, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "*":
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.MULTIPLICACION, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "-":
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.RESTA, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                default:
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.SUMA, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
            }
        }

        public Operacion expresionLogica(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)
            {
                string token = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0].ToLower();
                switch (token)
                {
                    case "and":
                        return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.AND, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                    default:
                        return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.OR, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                }
            }
            else
            {
                return new Operacion(expresion(actual.ChildNodes.ElementAt(1)), Operacion.Tipo.NOT, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
            }
        }
        public Operacion expresionUnaria(ParseTreeNode actual)
        {
            return new Operacion(expresion(actual.ChildNodes.ElementAt(1)), Operacion.Tipo.NEGATIVO, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }

        public Operacion expresionRelacional(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
            switch (token)
            {
                case ">":
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.MAYOR, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "<":
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.MENOR, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case ">=":
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.MAYORIGUAL, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "<=":
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.MENORIGUAL, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "=":
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.IGUAL, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                default:
                    return new Operacion(expresion(actual.ChildNodes.ElementAt(0)), Operacion.Tipo.DIFRENTE, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
            }
        }
        public Operacion primitivo(ParseTreeNode hoja)
        {
            string[] token = hoja.ChildNodes.ElementAt(0).ToString().Split(' ');
            switch (true)
            {
                case bool _ when Regex.IsMatch(hoja.ChildNodes.ElementAt(0).ToString(), "(Cadena)"):
                    return new Operacion(hoja.ChildNodes.ElementAt(0).ToString().Replace(" (Cadena)", ""), Operacion.Tipo.CADENA, hoja.ChildNodes.ElementAt(0).Token.Location.Line, hoja.ChildNodes.ElementAt(0).Token.Location.Column);
                case bool _ when Regex.IsMatch(token[0], "^-?[0-9]+$"):
                    return new Operacion(int.Parse(token[0]), hoja.ChildNodes.ElementAt(0).Token.Location.Line, hoja.ChildNodes.ElementAt(0).Token.Location.Column);
                case bool _ when Regex.IsMatch(token[0], "^-?[0-9]+(\\.[0-9]+)?$"):
                    return new Operacion(Double.Parse(token[0]), hoja.ChildNodes.ElementAt(0).Token.Location.Line, hoja.ChildNodes.ElementAt(0).Token.Location.Column);
                case bool _ when Regex.IsMatch(token[0], "true|false"):
                    return new Operacion(Boolean.Parse(token[0].ToLower()), hoja.ChildNodes.ElementAt(0).Token.Location.Line, hoja.ChildNodes.ElementAt(0).Token.Location.Column);
                default:
                    return new Operacion(token[0], Operacion.Tipo.IDENTIFICADOR, hoja.ChildNodes.ElementAt(0).Token.Location.Line, hoja.ChildNodes.ElementAt(0).Token.Location.Column);
            }

        }

    }
}
