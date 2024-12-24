using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace campoRegalos
{
    internal class Program
    {
        /*
         * Vamos a realizar un programa que se llama Campo Regalos, el cual
         * mediante una matriz bidimensional nos ofrecerá la oportunidad de 
         * ir sumando puntos mientras nos desplazamos por ella y nos permitirá
         * seguir el camino mientras no obtengamos una casilla con valor 0, en
         * ese caso el programa nos dirá que hemos perdido
         */
        static void Main(string[] args)
        {
            /*
             * En el main vamos a ir ejecutando los metodos para que este programa
             * tenga modularidad y si quisiesemos cambiar la funcionalidad del mismo
             * no tengamos que hacer cambios en todo el programa
             */
            Random rnd = new Random();
            
            //Vamos a indicarle al usuario que el mismo genere la matriz introduciendo sus limites
            Console.WriteLine("introduce número de líneas");
            int lineas = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("introduce número de columnas");
            int columnas = int.Parse(Console.ReadLine());

            //Aquí vamos a declarar los puntos que va obteniendo el usuario
            int puntosParciales = 0;
            int puntosTotales = 0;
            
            //Vamos a hacer que el usuario aparezca en una columna de la primera fila de forma aleatoria
            int columnaAleatoria = rnd.Next(0, columnas);
            int[] posicionJugador = { 0, columnaAleatoria };

            int[,] tablero = new int[lineas, columnas];
            //Llamamos a la funcion crear el tablero
            CrearTablero(tablero,lineas, columnas);

            Console.WriteLine();
            Console.WriteLine("****");
            Console.WriteLine("****");
            Console.WriteLine();
            //Mostramos el tablero ya con las puntuaciones ocultas

            MostrarTablero(tablero, posicionJugador, puntosParciales);
            //Llamamos a la función movimiento, que nos permitirá jugar

            movimiento(tablero, posicionJugador, puntosParciales, puntosTotales);

           
            
        }

        /*
         * En primer lugar vamos a crear el tablero, de manera que le pasaremos por parámetros
         * los valores de filas y columnas que el usuario ha pasado por consola, ya que estos
         * nos servirán para marcar los limites de nuestros bucles for anidados para pintar la
         * matriz y asignar con la clase Random un número entre el 1 y el 100
         */
        static void CrearTablero(int[,] tablero, int filas, int torres)
        {
            Random random = new Random();

            for (int i = 0; i < filas; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < torres; j++)
                {
                    tablero[i, j] = random.Next(1, 100);
                }
                //A partir de la primera fila o de indice 0 vamos a asignar un 0 aleatorio
                if (i >= 1) 
                { 
                int posicionAleatoria = random.Next(torres);
                    tablero[i, posicionAleatoria] = 0;
                   
                }
                //Pintamos las columnas con sus valores
                for (int j = 0; j < torres; j++) { 
                
                    Console.Write(tablero[i, j] + "\t");
                }

                Console.WriteLine();
            }

        }
        //En caso de no querer tener esta guía y para una mejor experiencia, se deben borrar los Console
        //para que el programa no muestre ni puntuaciones ni donde se ocultan los ceros


        /*
         * Este metodo nos oculta los valores que tienen las casillas de nuestra Matriz, para ello
         * el codigo que va a a mostrar sera:
         * I => posición donde se encuentra el usuario
         * X => posiciones ocultas
         * - => posiciones de filas ya pasadas
         * * => posiciones que contenían un cero
         * El método va a recibir por parámetros la posición del jugador y los puntos Parciales que va obteniendo
         * el usuario, y un array con las coordenadas, de la posición del Jugador.
         */
        static void MostrarTablero(int[,]tablero, int[] posicionJugador, int puntosParciales) 
        {
            //voy a descomponer en variables simples los valores de la posición del jugador
            //para trabajar de manera más sencilla con estas variables
            int filas = posicionJugador[0];
            int torres = posicionJugador[1];

            
            for (int i = 0;i < tablero.GetLength(0); i++){

                Console.WriteLine();
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    //según condiciones if/else vamos pintando la representación de la matriz
                    if (i == filas && j == torres)
                    {
                        Console.Write("I\t");
                        //le asigno los puntos que ha obtenido en la jugada
                        puntosParciales = tablero[i, torres];
                    }

                    else if (i < filas && tablero[i, j] == 0)
                    {
                        Console.Write("*\t");
                    }
                    else if (i < filas)
                    {
                        Console.Write("-\t");
                    }
                    else
                    {
                        Console.Write("X\t");
                    }  
                }
            }
            //Muestro por pantalla los puntos obtenidos
            Console.WriteLine();
            Console.WriteLine("Has obtenido: " + puntosParciales + " puntos");
            }

        /*
         * El método Movimiento es el motor principal de nuestro programa, ya que se encarga de que
         * el usuario pueda moverse por el tablero, este recibe la posición del jugador y la puntuación
         * que va obteniendo el usuario.
         * Este es un método que va a pedir al usuario que se mueva, para ello
         * ponemos un bucle do/while para que nos pida movimiento y ejecute el movimiento
         * hasta cumplir las condiciones de éxito o derrota.
         */
        static void movimiento(int[,]tablero, int[]posicionJugador, int puntosParciales, int puntosTotales) {
            //iniciamos el bucle do/while
            do {
                //le decimos al usuario los movimientos permitidos
                Console.WriteLine();
                Console.WriteLine("Introduce movimiento: \"i\" para ir a la diagonal-izquierda, \"d\" hacia la diagonal-derecha o \"r\" continuar recto");
                string mover = Console.ReadLine();
            
                //con un switch vamos a realizar el movimiento
                //todos los movimientos suman filas, oséa que vamos siempre hacia abajo y en función 
                //de si vamos a izquierda o derecha, sumaremos o restaremos en las columnas
                //Además se ha implementado con una estructura if/else que el usuario no pueda desbordar la matriz
            switch (mover) 
            {

                case "d":
                        if (posicionJugador[1] < (tablero.GetLength(1)-1))
                        {
                            posicionJugador[0]++;
                            posicionJugador[1]++;
                            MostrarTablero(tablero, posicionJugador, puntosParciales);
                            puntosParciales = tablero[posicionJugador[0], posicionJugador[1]];
                            puntosTotales += puntosParciales;
                            Console.WriteLine("Tus puntos totales son " + puntosTotales);
                        }
                        else 
                        { 
                            Console.WriteLine("No puedes salirte de los límites de la matriz");
                        }
                        Console.WriteLine();
                    break;
                case "i":
                        if (posicionJugador[1] < (tablero.GetLength(1) - 1))
                        {
                            posicionJugador[0]++;
                            posicionJugador[1]--;
                            MostrarTablero(tablero, posicionJugador, puntosParciales);
                            puntosParciales = tablero[posicionJugador[0], posicionJugador[1]];
                            puntosTotales += puntosParciales;
                            Console.WriteLine("Tus puntos totales son " + puntosTotales);
                        }
                        else
                        {
                            Console.WriteLine("No puedes salirte de los límites de la matriz");

                        }
                        Console.WriteLine();
                    break;
                case "r":
                    posicionJugador[0]++;
                    MostrarTablero(tablero, posicionJugador, puntosParciales);
                        puntosParciales = tablero[posicionJugador[0], posicionJugador[1]];
                        puntosTotales += puntosParciales;
                        Console.WriteLine("Tus puntos totales son " + puntosTotales);
                        Console.WriteLine();
                    break;
                default:
                    Console.WriteLine("Movimiento no permitido");
                    break;

            }
            } while (posicionJugador[0] < (tablero.GetLength(0)-1) && tablero[posicionJugador[0], posicionJugador[1]] != 0);

            //una vez finaliazo el bucle, se le anuncia al usuario si ha ganado o perdido
            if (posicionJugador[0] >= tablero.GetLength(0) - 1)
            {
                Console.WriteLine("ENHORABUENA HAS GANADO");
                Console.WriteLine("Has obtenido: " + puntosTotales);
            }
            else {
                Console.WriteLine("HAS PERDIDO");
            }

        }
        }

    }

