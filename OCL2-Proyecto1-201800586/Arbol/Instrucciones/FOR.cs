using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class FOR : Instruccion
    {
        int Instruccion.linea { get ; set ; }
        int Instruccion.columna { get ; set ; }

        private Asignacion asignar;
        private Operacion inicializar;
        private Operacion finalizar;
        //Solo acpeta 1 o -1
        private int incrementar_decrementar;
        public FOR(Asignacion asignar, Operacion inicializar, Operacion finalizar, int incrementar_decrementar)
        {
            this.asignar = asignar;
            this.inicializar = inicializar;
            this.finalizar = finalizar;
            this.incrementar_decrementar = incrementar_decrementar;
        }

        object Instruccion.ejeuctar(TablaSimbolo ts)
        {
            if ((Boolean)asignar.ejeuctar(ts))
            {
                Object aux = finalizar.ejeuctar(ts);
                if(aux is Double)
                {

                }
            }
            return null;
        }
    }
}
