using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Asignacion : Instruccion
    {
        private String identificador;
        private Operacion valor;
        public int linea { get; set; }
        public int columna { get; set; }
        public Asignacion(String identificador, Operacion valor, int linea, int columna)
        {
            this.identificador = identificador;
            this.valor = valor;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            string tipo = ts.getTipo(identificador).ToString();
            if (tipo.Equals(valor.tipo.ToString()))
            {
                ts.setValor(identificador, valor.ejeuctar(ts));
            }
            else if (tipo.Equals("DECIMAL") && valor.tipo.ToString().Equals("ENTERO"))
            {
                ts.setValor(identificador, valor.ejeuctar(ts));
            }
            else
            {
                Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " No se puede asignar un tipo '" + valor.tipo.ToString() + "' a un tipo '" + tipo + "'\n";
            }
            
            return null;
        }
    }
}
