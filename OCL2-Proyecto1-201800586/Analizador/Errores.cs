using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Analizador
{
    class Errores
    {
        public enum Tipo
        {
            LEXICO,
            SINTACTICO,
            SEMANTICO
        }
        public int linea;
        public int columna;
        public String token;
        public Tipo tipo;
        public String descripcion;

        public Errores(int linea, int columna, String token, Tipo tipo, String descripcion)
        {
            this.linea = linea;
            this.columna = columna;
            this.token = token;
            this.tipo = tipo;
            this.descripcion = descripcion;
        }

        public String getError()
        {
            return "linea: " + linea + " ,columna: " + columna + " error: " + tipo + "descripcion: " + descripcion;
        } 
    }
}
