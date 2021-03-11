using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Valores
{
    class Simbolo : ICloneable
    {
        public enum Tipo
        {
            CADENA,
            ENTERO,
            DECIMAL,
            BOOLEANA,
            STRUCT,
            OBJETO,
            VOID,
            DESCONOCIDO,
            CONST
        }
        public int linea { get; set; }
        public int columna { get; set; }
        private Tipo tipo;
        private String identificador;
        private Object valor;
        private String ambito;
        public bool constate = false;
        public Simbolo(String identificador, Tipo tipo, int linea, int columna, String ambito)
        {
            this.tipo = tipo;
            this.identificador = identificador;
            this.linea = linea;
            this.columna = columna;
            this.ambito = ambito;
        }

        public Simbolo(String identificador, Tipo tipo, Object valor, int linea, int columna, String ambito)
        {
            this.tipo = tipo;
            this.identificador = identificador;
            this.valor = valor;
            this.ambito = ambito;
        }
        public String Identificador { get => identificador; set => identificador = value; }
        public Object Valor { get => valor; set => valor = value; }
        public Tipo type { get => tipo; set => tipo = value; }
        public String Ambito { get => ambito; set => ambito = value; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
