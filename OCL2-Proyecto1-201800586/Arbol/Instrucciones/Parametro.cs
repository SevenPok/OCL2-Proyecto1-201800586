using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Parametro : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public String identificador { get; set; }
        public Simbolo.Tipo tipo { get; set; }

        public Parametro(String identificador, Simbolo.Tipo tipo, int line, int columna)
        {
            this.identificador = identificador;
            this.tipo = tipo;
            this.linea = line;
            this.columna = columna;
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            return null;
        }

        public Simbolo getTipo()
        {
            if (tipo == Simbolo.Tipo.ENTERO)
            {
                return new Simbolo(identificador, tipo, 0, linea, columna, "Funcion "+identificador);
            }
            else if (tipo == Simbolo.Tipo.DECIMAL)
            {
                return new Simbolo(identificador, tipo, (Double)0, linea, columna, "Funcion " + identificador);
            }
            else if (tipo == Simbolo.Tipo.BOOLEANA)
            {
                return new Simbolo(identificador, tipo, true, linea, columna, "Funcion " + identificador);
            }
            else
            {
                return new Simbolo(identificador, Simbolo.Tipo.CADENA, "", linea, columna, "Funcion " + identificador);
            }
        }
    }
}
