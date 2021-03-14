using OCL2_Proyecto1_201800586.Analizador;
using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class SWITCH : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        private Operacion expresion;
        private LinkedList<Caso> casos;
        private LinkedList<Instruccion> listaElse;
        public SWITCH(Operacion expresion, LinkedList<Caso> casos, LinkedList<Instruccion> listaElse, int linea, int columna)
        {
            this.expresion = expresion;
            this.casos = casos;
            this.listaElse = listaElse;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public object ejeuctar(TablaSimbolo ts)
        {
            Object condicion = expresion.ejeuctar(ts);
            if (condicion != null)
            {
                LinkedList<Object> casoAux = new LinkedList<Object>();
                foreach(Caso c in casos){
                    Object casoVerdadero = c.ejecutarCaso(ts);
                    if (c != null && condicion.GetType().Equals(casoVerdadero.GetType()))
                    {
                        if (condicion.ToString() == casoVerdadero.ToString())
                        {
                            Object o = c.ejeuctar(ts);
                            if (o is Exit)
                            {
                                return o;
                            }
                            return null;
                        } 
                    }
                    else
                    {
                        Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " , el caso no es el mismo tipo de dato que la variable\n";
                        Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "el caso no es el mismo tipo de dato que la variable"));
                        return null;
                    }
                }
                foreach (Instruccion ELSE in listaElse)
                {
                    ELSE.ejeuctar(ts);
                }
                return null;

            }
            Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " , el caso no es el mismo tipo de dato que la variable\n";
            Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El caso no es el mismo tipo de dato que la variable"));
            return null;
        }
    }
}
