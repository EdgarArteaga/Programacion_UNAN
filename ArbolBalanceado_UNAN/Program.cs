using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolBalanceado_UNAN
{
  
    class Program
    {
        public static int Menu()
        {
            int op;
            Console.Clear();
            Console.WriteLine("--------------Arbol Balanceado------------------");
            Console.WriteLine("1.- Insertar");
            Console.WriteLine("2.- Mostrar");
            Console.Write("Opcion: "); op = int.Parse(Console.ReadLine());
            return op;
        }
        static void Main(string[] args)
        {
            ArbolBalanceado arbol = new ArbolBalanceado();
           Console.WriteLine("Arbol Balanceado\n");
            arbol.Insertar(8);
            arbol.Insertar(9);
            arbol.Insertar(11);
            arbol.Insertar(15);
            arbol.Insertar(19);
            arbol.Insertar(20);
            arbol.Insertar(21);
            arbol.Insertar(7);
            arbol.Insertar(3);
            arbol.Insertar(2);
            arbol.Insertar(1);
            arbol.Insertar(5);
            arbol.Insertar(6);
            arbol.Insertar(4);
            arbol.Insertar(13);
            arbol.Insertar(14);
            arbol.Insertar(10);
            arbol.Insertar(12);
            arbol.Insertar(17);
            arbol.Insertar(16);
            arbol.Insertar(18);
            arbol.mostrarArbolAVL();
            Console.ReadKey();
        }
    }
}
