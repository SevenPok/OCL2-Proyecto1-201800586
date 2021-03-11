using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Atributo : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        private String identificador;
        private Simbolo.Tipo tipo;

        public Atributo(String identificador, Simbolo.Tipo tipo, int linea, int columna)
        {
            this.identificador = identificador;
            this.tipo = tipo;
            this.linea = linea;
            this.columna = columna;
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            return null;
        }

        public Simbolo getAtributo()
        {
            if (tipo == Simbolo.Tipo.ENTERO)
            {
                return new Simbolo(identificador, tipo, 0, linea, columna, "global");
            }
            else if (tipo == Simbolo.Tipo.DECIMAL)
            {
                return new Simbolo(identificador, tipo, (Double)0, linea, columna, "global");
            }
            else if (tipo == Simbolo.Tipo.BOOLEANA)
            {
                return new Simbolo(identificador, tipo, true, linea, columna, "global");
            }
            else
            {
                return new Simbolo(identificador, Simbolo.Tipo.CADENA, "", linea, columna, "global");
            }
        }
    }
}
