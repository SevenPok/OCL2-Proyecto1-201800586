using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Objeto : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        private String identificador;
        private LinkedList<Atributo> atributos;

        public Objeto(String identificador, LinkedList<Atributo> atributos, int linea, int columna)
        {
            this.identificador = identificador;
            this.atributos = atributos;
            this.linea = linea;
            this.columna = linea;
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            TablaSimbolo aux = new TablaSimbolo();
            foreach (Atributo atributo in atributos)
            {
                aux.AddLast(atributo.getAtributo());
            }
            ts.AddLast(new Simbolo(identificador, Simbolo.Tipo.STRUCT, aux, linea, columna, "global"));
            return null;
        }
    }
}
