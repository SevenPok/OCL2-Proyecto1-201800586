using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Valores
{
    class Simbolo
    {
        public enum Tipo
        {
            CADENA,
            ENTERO,
            DECIMAL,
            BOOLEANA,
            OBJETO,
            DESCONOCIDO
        }

        private Tipo tipo;
        private String identificador;
        private Object valor;
        public Simbolo(String identificador, Tipo tipo)
        {
            this.tipo = tipo;
            this.identificador = identificador;
        }
        public String Identificador { get => identificador; set => identificador = value; }
        public Object Valor { get => valor; set => valor = value; }
        public Tipo type { get => tipo; set => tipo = value; }

    }
}
