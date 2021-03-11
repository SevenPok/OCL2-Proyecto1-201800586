using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class CallAtributo : Instruccion
    {
        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public String identificador;
        public String atributo;

        public CallAtributo(String identificador, String atributo, int linea, int columna)
        {
            this.identificador = identificador;
            this.atributo = atributo;
            this.linea = linea;
            this.columna = columna;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            if (ts.existe(identificador) && ts.getTipo(identificador) == Simbolo.Tipo.OBJETO)
            {
                TablaSimbolo atributos = (TablaSimbolo)ts.getValor(identificador);
                if (atributos.existe(atributo))
                {
                    return atributos.getValor(atributo);
                }
                else
                {
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El atributo '" + atributo + "' no existe\n";
                }
            }
            else
            {
                Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El objeto '" + identificador + "' no esta declarado\n";
            }
            return null;
        }
    }
}
