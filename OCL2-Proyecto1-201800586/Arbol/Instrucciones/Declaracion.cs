using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Declaracion : Instruccion
    {
        public String identificador;
        private String objeto;
        public Simbolo.Tipo tipo;

        public Declaracion(String identificador, Simbolo.Tipo tipo, int linea, int columna)
        {
            this.identificador = identificador;
            this.tipo = tipo;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public Declaracion(String identificador, String objeto, Simbolo.Tipo tipo, int linea, int columna)
        {
            this.identificador = identificador;
            this.objeto = objeto;
            this.tipo = tipo;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public object ejeuctar(TablaSimbolo ts)
        {
            if (tipo == Simbolo.Tipo.OBJETO)
            {
                if (ts.getIdentificador(identificador) == null)
                {
                    if(ts.getIdentificador(objeto) != null && ts.getTipo(objeto) == Simbolo.Tipo.STRUCT )
                    {
                        TablaSimbolo aux = (TablaSimbolo)ts.getValor(objeto);
                        ts.AddLast(new Simbolo(identificador, tipo, (TablaSimbolo)aux.Clone(), linea, columna, "global"));
                        //Form1.consola.Text += "Declarado\n";
                    }
                    else
                    {
                        Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El objeto '" + objeto + "' no ha sido declarado\n";
                    }
                    
                }
                else
                {
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " La variable '" + identificador + "' ya ha sido declarado\n";
                }
            }
            else
            {
                if (ts.getIdentificador(identificador) == null)
                {
                    ts.AddLast(new Simbolo(identificador, tipo, linea, columna, "global"));
                }
                else
                {
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " La variable '" + identificador + "' ya ha sido declarado\n";
                }
            }            
            return null;
        }
    }
}
