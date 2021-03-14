using OCL2_Proyecto1_201800586.Analizador;
using OCL2_Proyecto1_201800586.Arbol.Instrucciones;
using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Valores
{
    internal class Operacion : Instruccion, ICloneable
    {
        public enum Tipo
        {
            SUMA,
            RESTA,
            MULTIPLICACION,
            DIVISION,
            MODULAR,
            NEGATIVO,
            MAYOR,
            MENOR,
            MAYORIGUAL,
            MENORIGUAL,
            IGUAL,
            DIFRENTE,
            AND,
            OR,
            NOT,
            CONCATENACION, 
            CADENA,
            ENTERO,
            DECIMAL,
            IDENTIFICADOR,
            BOOLEANA,
            OBJETO,
            FUNCION,
            DESCONOCIDO
        }
        public Tipo tipo;
        private Operacion izquierda;
        private Operacion derecha;
        public Object valor;
        private CallAtributo atributo;
        public LLamadaFuncion llamada;

        public int linea { get ; set ; }
        public int columna { get ; set ; }

        public Operacion(Operacion izquierda, Tipo tipo, Operacion derecha, int linea , int columna)
        {
            this.izquierda = izquierda;
            this.tipo = tipo;
            this.derecha = derecha;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public Operacion(Operacion izquierda, Tipo tipo, int linea, int columna)
        {
            this.tipo = tipo;
            this.izquierda = izquierda;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public Operacion(String valor, Tipo tipo, int linea, int columna)
        {
            this.valor = valor;
            this.tipo = tipo;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public Operacion(Boolean valor, int linea, int columna)
        {
            this.valor = valor;
            this.tipo = Tipo.BOOLEANA;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public Operacion(Double valor, int linea, int columna)
        {
            this.valor = valor;
            this.tipo = Tipo.DECIMAL;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public Operacion(int valor, int linea, int columna)
        {
            this.valor = valor;
            this.tipo = Tipo.ENTERO;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public Operacion(CallAtributo atributo, int linea, int columna)
        {
            this.atributo = atributo;
            this.tipo = Tipo.OBJETO;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public Operacion(LLamadaFuncion llamada, int linea, int columna)
        {
            this.llamada = llamada;
            this.tipo = Tipo.FUNCION;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }
        public object ejeuctar(TablaSimbolo ts)
        {
            Object left;
            Object right;
            switch (tipo)
            {
                case Tipo.DIVISION:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null && left is Double && right is Double)
                    {
                        if((Double)right != 0) {
                            return (Double)left / (Double)right;
                        }
                        Form1.consola.Text += "No se puede dividir entre 0'\n";
                        Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "No se puede dividir entre 0"));
                        return null;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '/' no se puede ejecutar entre un '" + izquierda.tipo + "' con un '"+ derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '/' no se puede ejecutar entre un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.MULTIPLICACION:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null && left is Double && right is Double)
                    {
                        return (Double)left * (Double)right;   
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '*' no se puede ejecutar entre un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '*' no se puede ejecutar entre un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.RESTA:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null && left is Double && right is Double)
                    {
                        return (Double)left - (Double)right;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '-' no se puede ejecutar entre un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '-' no se puede ejecutar entre un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.SUMA:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null)
                    {
                        if (left is Double && right is Double)
                        {
                            return (Double)left + (Double)right;
                        }
                        else if (left is String && right is String)
                        {
                            return (String)((String)left + (String)right);
                        }
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '+' no se puede ejecutar entre un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '+' no se puede ejecutar entre un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.MODULAR:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null && left is Double && right is Double)
                    {
                        return (Double)left % (Double)right;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + "El operador '%' no se puede ejecutar entre un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '%' no se puede ejecutar entre un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.NEGATIVO:
                    left = izquierda.ejeuctar(ts);
                    if (left != null && left is Double)
                    {
                        return (Double)left*-1;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '-' no se puede ejecutar con un '" + izquierda.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '-' no se puede ejecutar con un '" + izquierda.tipo + "'"));
                    return null;
                case Tipo.NOT:
                    left = izquierda.ejeuctar(ts);
                    if (left != null && left is Boolean)
                    {
                        return !(Boolean)left;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador 'not' no se puede ejecutar con un '" + izquierda.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador 'not' no se puede ejecutar con un '" + izquierda.tipo + "'"));
                    return null;
                case Tipo.ENTERO:
                    return Double.Parse(valor.ToString());
                case Tipo.DECIMAL:
                    return Double.Parse(valor.ToString());
                case Tipo.IDENTIFICADOR:
                    if (ts.getValor(valor.ToString()) != null)
                    {
                        return ts.getValor(valor.ToString());
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El identificador '" + valor.ToString() + "' es desconocido.\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El identificador '" + valor.ToString() + "' es desconocido"));
                    return null;
                case Tipo.CADENA:
                    return valor.ToString();
                case Tipo.BOOLEANA:
                    return Boolean.Parse(valor.ToString());
                case Tipo.OBJETO:
                    Object ob = atributo.ejeuctar(ts);
                    if (ob != null)
                    {
                        return ob;
                    }
                    return null;
                case Tipo.FUNCION:
                    Object o = llamada.ejeuctar(ts);
                    if (o != null)
                    {
                        return o; 
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + linea + " La funcion '" + llamada.identificador + "' no devuelve ningun valor.\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "La funcion '" + llamada.identificador + "' no devuelve ningun valor"));
                    return null;
                case Tipo.MAYOR:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null && left is Double && right is Double)
                    {
                        return (Double)left > (Double)right;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '>' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '>' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.MENOR:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null && left is Double && right is Double)
                    {
                        return (Double)left < (Double)right;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '<' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '<' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.MAYORIGUAL:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null && left is Double && right is Double)
                    {
                        return (Double)left >= (Double)right;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '>=' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '>=' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.MENORIGUAL:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null && left is Double && right is Double)
                    {
                        return (Double)left <= (Double)right;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '<=' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '<=' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.IGUAL:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null)
                    {
                        if(left is Double && right is Double)
                        {
                            return (Double)left == (Double)right;
                        }
                        else if (left is String && right is String)
                        {
                            return (String)left == (String)right;
                        }
                        else if (left is Boolean && right is Boolean)
                        {
                            return (Boolean)left == (Boolean)right;
                        }
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '=' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '=' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.DIFRENTE:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null)
                    {
                        if (left is Double && right is Double)
                        {
                            return (Double)left != (Double)right;
                        }
                        else if (left is String && right is String)
                        {
                            return (String)left != (String)right;
                        }
                        else if (left is Boolean && right is Boolean)
                        {
                            return (Boolean)left != (Boolean)right;
                        }
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador '<>' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador '<>' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.OR:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null && left is Boolean && right is Boolean)
                    {
                        return (Boolean)left || (Boolean)right;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador 'or' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador 'or' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                case Tipo.AND:
                    left = izquierda.ejeuctar(ts);
                    right = derecha.ejeuctar(ts);
                    if (left != null && right != null && left is Boolean && right is Boolean)
                    {
                        return (Boolean)left && (Boolean)right;
                    }
                    Form1.consola.Text += "Linea: " + linea + " Columna: " + columna + " El operador 'and' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'\n";
                    Sintactico.errores.AddLast(new Errores(linea, columna, "", Errores.Tipo.SEMANTICO, "El operador 'and' no se puede ejecutar un '" + izquierda.tipo + "' con un '" + derecha.tipo + "'"));
                    return null;
                default:
                    return null;
            }
           
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
