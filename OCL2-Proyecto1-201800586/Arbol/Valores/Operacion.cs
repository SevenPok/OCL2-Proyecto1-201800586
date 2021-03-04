using OCL2_Proyecto1_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCL2_Proyecto1_201800586.Arbol.Valores
{
    class Operacion : Instruccion
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
        }
        public Tipo tipo;
        private Operacion izquierda;
        private Operacion derecha;
        public Object valor;

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
        public Operacion(Tipo tipo, Operacion izquierda, int linea, int columna)
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
        public object ejeuctar(TablaSimbolo ts)
        {
            switch (tipo)
            {
                case Tipo.DIVISION:
                    try
                    {
                        if ((Double)derecha.ejeuctar(ts) != 0)
                        {
                            return (Double)izquierda.ejeuctar(ts) / (Double)derecha.ejeuctar(ts);
                        }
                        else
                        {
                            return "Linea: " + linea + " Columna: " + columna + " No se puede dividir dentro de 0\n";
                        }
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " No se puede dividir '" + izquierda.tipo + "' con '" + derecha.tipo + "'\n";
                    }    
                case Tipo.MULTIPLICACION:
                    try
                    {
                        return (Double)izquierda.ejeuctar(ts) * (Double)derecha.ejeuctar(ts);
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " No se puede multiplicar '" + izquierda.tipo + "' con '" + derecha.tipo + "'\n";
                    }
                case Tipo.RESTA:
                    try
                    {
                        return (Double)izquierda.ejeuctar(ts) - (Double)derecha.ejeuctar(ts);
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " No se puede restar '" + izquierda.tipo + "' con '" + derecha.tipo + "'\n";
                    }
                case Tipo.SUMA:
                    try
                    {
                        return (Double)izquierda.ejeuctar(ts) + (Double)derecha.ejeuctar(ts);
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " No se puede sumar '" + izquierda.tipo + "' con '" + derecha.tipo + "'\n";
                    }
                case Tipo.MODULAR:
                    try
                    {
                        if ((Double)derecha.ejeuctar(ts) != 0)
                        {
                            return (Double)izquierda.ejeuctar(ts) % (Double)derecha.ejeuctar(ts);
                        }
                        else
                        {
                            return "Linea: " + linea + " Columna: " + columna + " No se puede sacar el modulo dentro de 0\n";
                        }
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " No se puede scar el modulo '" + izquierda.tipo + "' con '" + derecha.tipo + "'\n";
                    }
                case Tipo.NEGATIVO:
                    try
                    {
                        return (Double)izquierda.ejeuctar(ts) * -1;
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " Solo los tipo 'interger' y 'real' pueden ser nagativos";
                    }
                case Tipo.NOT:
                    try
                    {
                        return !(Boolean)izquierda.ejeuctar(ts);
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " Solo se puede negar las expresiones boolenasa\n";
                    }
                case Tipo.ENTERO:
                    return Double.Parse(valor.ToString());
                case Tipo.DECIMAL:
                    return Double.Parse(valor.ToString());
                case Tipo.IDENTIFICADOR:
                    if (ts.getValor(valor.ToString()) != null)
                    {
                        return ts.getValor(valor.ToString());
                    }
                    return "";
                case Tipo.CADENA:
                    return valor.ToString();
                case Tipo.BOOLEANA:
                    return Boolean.Parse(valor.ToString());
                case Tipo.MAYOR:
                    try
                    {
                        return (Double)izquierda.ejeuctar(ts) > (Double)derecha.ejeuctar(ts);
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " El operador '>' solo funciona con 'Integer' o 'Real'\n";
                    }

                case Tipo.MENOR:

                    try
                    {
                        return (Double)izquierda.ejeuctar(ts) < (Double)derecha.ejeuctar(ts);
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " El operador '<' solo funciona con 'Integer' o 'Real'\n";
                    }
                case Tipo.MAYORIGUAL:

                    try
                    {
                        return (Double)izquierda.ejeuctar(ts) >= (Double)derecha.ejeuctar(ts);
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " El operador '>=' solo funciona con 'Integer' o 'Real'\n";
                    }
                case Tipo.MENORIGUAL:

                    try
                    {
                        return (Double)izquierda.ejeuctar(ts) <= (Double)derecha.ejeuctar(ts);
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " El operador '<=' solo funciona con 'Integer' o 'Real'\n";
                    }
                case Tipo.IGUAL:
                    try
                    {
                        return izquierda.ejeuctar(ts).ToString() == derecha.ejeuctar(ts).ToString();
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " El operador '=' dado que no son del mismo tipo\n";
                    }

                case Tipo.DIFRENTE:
                    try
                    {
                        return izquierda.ejeuctar(ts).ToString() != derecha.ejeuctar(ts).ToString();
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " El operador '!=' dado que no son del mismo tipo\n";
                    }
                case Tipo.OR:
                    try
                    {
                        return (Boolean)izquierda.ejeuctar(ts) || (Boolean)derecha.ejeuctar(ts);
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " El operador 'or' solo funciona con 'true' o 'false'\n";
                    }
                case Tipo.AND:
                    try
                    {
                        return (Boolean)izquierda.ejeuctar(ts) && (Boolean)derecha.ejeuctar(ts);
                    }
                    catch
                    {
                        return "Linea: " + linea + " Columna: " + columna + " El operador 'and' solo funciona con 'true' o 'false'\n";
                    }
                    
                default:
                    return null;
            }
           
        }
    }
}
