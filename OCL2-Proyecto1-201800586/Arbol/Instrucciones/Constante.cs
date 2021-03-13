using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Constante : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }

        private String identificador;
        private Operacion valor;

        public Constante(String identificador, Operacion valor, int linea, int columna)
        {
            this.identificador = identificador;
            this.valor = valor;
            this.linea = linea;
            this.columna = columna;
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            Object o = valor.ejeuctar(ts);
            if (o != null)
            {
                if (!ts.existe(identificador))
                {
                    
                    if ((Regex.IsMatch(o.ToString(), "^-?[0-9]+$")))
                    {
                        Simbolo simbolo = new Simbolo(identificador, Simbolo.Tipo.ENTERO, o, linea, columna, "global");
                        simbolo.constate = true;
                        ts.AddLast(simbolo);
                    }
                    else if (o.GetType().Equals(typeof(Double)))
                    {
                        Simbolo simbolo = new Simbolo(identificador, Simbolo.Tipo.DECIMAL, o, linea, columna, "global");
                        simbolo.constate = true;
                        ts.AddLast(simbolo);
                    }
                    else if (o.GetType().Equals(typeof(String)))
                    {
                        Simbolo simbolo = new Simbolo(identificador, Simbolo.Tipo.CADENA, o, linea, columna, "global");
                        simbolo.constate = true;
                        ts.AddLast(simbolo);
                    }
                    else if (o.GetType().Equals(typeof(Boolean)))
                    {
                        Simbolo simbolo = new Simbolo(identificador, Simbolo.Tipo.BOOLEANA, o, linea, columna, "global");
                        simbolo.constate = true;
                        ts.AddLast(simbolo);
                    }
                    else
                    {
                        Form1.consola.Text += "Linea: " + valor.linea + " Columna: " + valor.columna + ", la expresion no concuerda con el tipo de dato.\n";
                    }
                    return null;
                }
                else
                {
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + ", el identificador '" + identificador + "' ya existe.\n";
                    return null;
                }
            }
            return null;
        }
    }
}
