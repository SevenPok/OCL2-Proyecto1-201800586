using OCL2_Proyecto1_201800586.Analizador;
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
        public bool referencia;
        public Declaracion(String identificador, Simbolo.Tipo tipo, int linea, int columna)
        {
            this.identificador = identificador;
            this.tipo = tipo;
            this.linea = linea + 1;
            this.columna = columna + 1;
            referencia = false;
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
                foreach(Objeto o in Sintactico.objetos)
                {
                    if (objeto == o.identificador)
                    {
                        ts.AddLast(new Simbolo(identificador, Simbolo.Tipo.OBJETO, (TablaSimbolo)o.tabla.Clone(), linea, columna, "global"));
                        return null;
                    }
                }
                Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El objeto '" + identificador + "' no existe. \n";
                return null;
            }
            else
            {
                if (ts.getIdentificador(identificador) == null)
                {
                    if(tipo == Simbolo.Tipo.ENTERO || tipo == Simbolo.Tipo.DECIMAL)
                    {
                        ts.AddLast(new Simbolo(identificador, tipo, 0.0, linea, columna, "global"));
                    }
                    else if (tipo == Simbolo.Tipo.CADENA)
                    {
                        ts.AddLast(new Simbolo(identificador, tipo, "", linea, columna, "global"));
                    }
                    else if (tipo == Simbolo.Tipo.BOOLEANA)
                    {
                        ts.AddLast(new Simbolo(identificador, tipo, false, linea, columna, "global"));
                    }
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
