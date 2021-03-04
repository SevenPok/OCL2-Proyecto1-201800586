using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using OCL2_Proyecto1_201800586.Arbol.Valores;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Instrucciones
{
    class Imprimir : Instruccion
    {
        private LinkedList<Instruccion> contenido;
        private String salto;
        public int linea { get; set; }
        public int columna { get; set; }
        public Imprimir(LinkedList<Instruccion> contenido, String salto)
        {
            this.contenido = contenido;
            this.salto = salto;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            String texto = "";
            foreach(Instruccion o in contenido)
            {
                Object impresion = o.ejeuctar(ts);
                if (impresion != null)
                {
                    texto += impresion.ToString();
                }
                else
                {
                    return null;
                }
            }
            Form1.consola.Text += texto + salto;
            return null;
        }
    }
}
