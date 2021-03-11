using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class AsignarAtributo : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public String identificador;
        public Object valor;
        public Simbolo.Tipo tipo;

        public AsignarAtributo(Object valor , Simbolo.Tipo tipo)
        {
            this.valor = valor;
            this.tipo = tipo;
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            if (ts.getIdentificador(identificador) != null)
            {

            }
            return null;
        }
    }
}
