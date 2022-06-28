using System;
namespace ArbolBalanceado_UNAN
{
    class ArbolBalanceado
    {
        Nodo root;
        public void Insertar(int clave)
        {
            root = InsertarBalanceado(root, clave);
        }
        private Nodo InsertarBalanceado(Nodo nodoActual, int key)
        {
            if (nodoActual == null)
            {
                return (new Nodo(key));
            }
            if (key < nodoActual.info)
            {
                nodoActual.izq = InsertarBalanceado(nodoActual.izq, key);
            }
            else if (key > nodoActual.info)
            {
                nodoActual.der = InsertarBalanceado(nodoActual.der, key);
            }
            else
            {// Si la clave esta duplicada retorna el mismo nodo encontrado
                return nodoActual;
            }
            // aqui voy ahorita----------------------------------------------------------
            // Actualizacion de la altura
            nodoActual.altura = 1 +
            max(getAltura(nodoActual.izq), getAltura(nodoActual.der));

            //Se obtiene el factor de equilibrio
            int fe = getFactorEquilibrio(nodoActual);

            if (fe > 1 && key < nodoActual.izq.info)
            {
                return rightRotate(nodoActual);
            }

            // Caso Rotacion Simple a la izquierda
            /*
            5 (-2)
              \
               12 (-1)
                  \
                   20 (0)
            */
            if (fe < -1 && key > nodoActual.der.info)
            {
                return leftRotate(nodoActual);
            }
            // Caso Rotacion Doble Izquierda Derecha
            /*
                   12 (2)
                  /
                5 (-1)
                  \
                   8 (0)
            */
            if (fe > 1 && key > nodoActual.izq.info)
            {// aca vengo
                nodoActual.izq = leftRotate(nodoActual.izq);
                return rightRotate(nodoActual);
            }

            // Caso Rotacion Doble Derecha Izquierda
            /*
            5 (-2)
              \
               12 (1)
              /
             8 (0)
            */
            if (fe < -1 && key < nodoActual.der.info)
            {
                nodoActual.der = rightRotate(nodoActual.der);
                return leftRotate(nodoActual);
            }

            return nodoActual;
        }

        /***************************************************************************/
        /******************************* BUSQUEDA **********************************/
        /***************************************************************************/

        //---búsqueda de un elemento en el AVL
        public void buscar(int elemento)
        {
            if (BuscaEnAVL(root, elemento))
            {
                Console.WriteLine("Exite");
            }
            else
            {
                Console.WriteLine("No exite");
            }
        }

        //Búsqueda recursiva en un AVL
        private bool BuscaEnAVL(Nodo nodoActual, int elemento)
        {
            if (nodoActual == null)
            {
                return false;
            }
            else if (elemento == nodoActual.info)
            {
                return true;
            }
            else if (elemento < nodoActual.info)
            {
                return BuscaEnAVL(nodoActual.izq, elemento);
            }
            else
            {
                return BuscaEnAVL(nodoActual.der, elemento);
            }
        }

        /***************************************************************************/
        /**************************** ELIMINACION **********************************/
        /**
         * @param key*************************************************************************/
        public void eliminar(int key)
        {
            root = eliminarAVL(root, key);
        }

        private Nodo eliminarAVL(Nodo nodoActual, int key)
        {
            if (nodoActual == null)
                return nodoActual;

            if (key < nodoActual.info)
            {
                nodoActual.izq = eliminarAVL(nodoActual.izq, key);
            }
            else if (key > nodoActual.info)
            {
                nodoActual.der = eliminarAVL(nodoActual.der, key);
            }
            else
            {
                //El nodo es igual a la clave, se elimina
                //Nodo con un unico hijo o es hoja
                if ((nodoActual.izq == null) || (nodoActual.der == null))
                {
                    Nodo temp = null;
                    if (temp == nodoActual.izq)
                    {
                        temp = nodoActual.der;
                    }
                    else
                    {
                        temp = nodoActual.izq;
                    }

                    // Caso que no tiene hijos
                    if (temp == null)
                    {
                        nodoActual = null; //Se elimina dejandolo en null
                    }
                    else
                    {
                        // Caso con un hijo
                        nodoActual = temp; //Elimina el valor actual reemplazandolo por su hijo
                    }
                }
                else
                {
                    //Nodo con dos hijos, se busca el predecesor
                    Nodo temp = getNodoConValorMaximo(nodoActual.izq);

                    // Se copia el dato del predecesor
                    nodoActual.info = temp.info;

                    // Se elimina el predecesor
                    nodoActual.izq = eliminarAVL(nodoActual.izq, temp.info);
                }
            }

            //Si solo tiene un nodo
            if (nodoActual == null)
                return nodoActual;

            // Actualiza altura
            nodoActual.altura = max(getAltura(nodoActual.izq), getAltura(nodoActual.der)) + 1;

            // Calcula factor de equilibrio (FE)
            int fe = getFactorEquilibrio(nodoActual);

            // Se realizan las rotaciones pertinentes dado el FE
            // Caso Rotacion Simple Derecha
            /*
                    20 (2)
                  /
                12 (1)
              /
            5 (0)
            */
            if (fe > 1 && getFactorEquilibrio(nodoActual.izq) >= 0)
            {
                return rightRotate(nodoActual);
            }

            //  Caso Rotacion Simple Izquierda
            /*
            5 (-2)
              \
               12 (-1)
                  \
                   20 (0)
            */
            if (fe < -1 && getFactorEquilibrio(nodoActual.der) <= 0)
            {
                return leftRotate(nodoActual);
            }

            // Caso Rotacion Doble Izquierda-Derecha
            /*
                   12 (2)
                  /
                5 (-1)
                  \
                   8 (0)
            */
            if (fe > 1 && getFactorEquilibrio(nodoActual.izq) < 0)
            {
                nodoActual.izq = leftRotate(nodoActual.izq);
                return rightRotate(nodoActual);
            }

            //Caso Rotacion Doble Derecha-Izquierda
            /*
            5 (-2)
              \
               12 (1)
              /
             8 (0)
            */
            if (fe < -1 && getFactorEquilibrio(nodoActual.der) > 0)
            {
                nodoActual.der = rightRotate(nodoActual.der);
                return leftRotate(nodoActual);
            }

            return nodoActual;
        }

        /***************************************************************************/
        /***********************
        /***************************************************************************/
        // Rotar hacia la derecha
        /*
                20 (2)
              /
            12 (1)
          /
        5 (0)
        */
        private Nodo rightRotate(Nodo nodoActual)
        {
            Nodo nuevaRaiz = nodoActual.izq;
            Nodo temp = nuevaRaiz.der;

            //  Se realiza la rotacion
            nuevaRaiz.der = nodoActual;
            nodoActual.izq = temp;

            //   Actualiza alturas
            nodoActual.altura = max(getAltura(nodoActual.izq), getAltura(nodoActual.der)) + 1;
            nuevaRaiz.altura = max(getAltura(nuevaRaiz.izq), getAltura(nuevaRaiz.der)) + 1;

            return nuevaRaiz;
        }

        // Rotar hacia la izquierda
        private Nodo leftRotate(Nodo nodoActual)
        {
            Nodo nuevaRaiz = nodoActual.der;
            Nodo temp = nuevaRaiz.izq;

            // Se realiza la rotacion
            nuevaRaiz.izq = nodoActual;
            nodoActual.der = temp;

            // Actualiza alturas
            nodoActual.altura = max(getAltura(nodoActual.izq), getAltura(nodoActual.der)) + 1;
            nuevaRaiz.altura = max(getAltura(nuevaRaiz.izq), getAltura(nuevaRaiz.der)) + 1;

            return nuevaRaiz;
        }

        /***************************************************************************/
        /******************
        /***************************************************************************/

        public void mostrarArbolAVL()
        {
            Console.WriteLine("Arbol AVL");
            showTree(root, 0);
        }

        private void showTree(Nodo nodo, int depth)
        {
            if (nodo.der != null)
            {
                showTree(nodo.der, depth + 1);// profundidad 
            }
            for (int i = 0; i < depth; i++)
            {
                Console.Write("          ");
            }
            Console.WriteLine("(" + nodo.info + ")");

            if (nodo.izq != null)
            {
                showTree(nodo.izq, depth + 1);
            }
        }
        /***************************************************************************/
        /**************************
        /***************************************************************************/

        // Obtiene el peso de un arbol dado
        private int getAltura(Nodo nodoActual)
        {
            if (nodoActual == null)
            {
                return 0;
            }

            // Notar que no es necesario recorrer el arbol para conocer la altura del nodo
            // debido a que en las rotaciones se incluye la actualizacion de las alturas respectivas
            return nodoActual.altura;
        }

        //Devuelve el mayor entre dos numeros
        private int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        //Obtiene el factor de equilibrio de un nodo
        private int getFactorEquilibrio(Nodo nodoActual)
        {
            if (nodoActual == null)
            {
                return 0;
            }
            return getAltura(nodoActual.izq) - getAltura(nodoActual.der);
        }
        private Nodo getNodoConValorMaximo(Nodo node)
        {
            Nodo Actual = node;

            while (Actual.der!= null)
            {
                Actual = Actual.der;
            }

            return Actual;
        }
    }

}
