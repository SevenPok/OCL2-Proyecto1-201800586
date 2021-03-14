using OCL2_Proyecto1_201800586.Analizador;
using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Objeto : Instruccion , ICloneable
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public String identificador;
        private LinkedList<Declaracion> atributos;
        public TablaSimbolo tabla;
        public Objeto(String identificador, LinkedList<Declaracion> atributos, int linea, int columna)
        {
            this.identificador = identificador;
            this.atributos = atributos;
            this.linea = linea + 1;
            this.columna = columna + 1;
            tabla = new TablaSimbolo();
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            foreach(Declaracion declarar in atributos)
            {
                declarar.ejeuctar(tabla);
            }

            //ts.AddLast(new Simbolo(identificador, Simbolo.Tipo.STRUCT, tabla, linea, columna, "global"));
            Sintactico.objetos.AddLast((Objeto)this.Clone());
            return null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
