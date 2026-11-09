using System;
using System.Collections.Generic;

namespace JuegoAhorcado
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            bool jugarDeNuevo = true;
            int modo = 0;

            while (jugarDeNuevo)
            {
                Console.Clear();

                if (modo == 0)
                {
                    Console.WriteLine("=== JUEGO DEL AHORCADO ===");
                    Console.WriteLine("1. Un jugador (palabra aleatoria)");
                    Console.WriteLine("2. Dos jugadores");
                    Console.Write("Selecciona una opción: ");

                    string opcion = Console.ReadLine();

                    if (opcion == "1")
                    {
                        modo = 1;
                    }
                    else if (opcion == "2")
                    {
                        modo = 2;
                    }
                    else
                    {
                        Console.WriteLine("Opción no válida.");
                        Console.WriteLine("Presiona una tecla para continuar...");
                        Console.ReadKey();
                        continue;
                    }
                }

                string palabra;

                if (modo == 1)
                {
                    palabra = ObtenerPalabraAleatoria();
                }
                else
                {
                    palabra = PedirPalabra();
                }

                JugarAhorcado(palabra, modo);

                Console.WriteLine();
                Console.WriteLine("================================");
                Console.WriteLine("¿QUÉ DESEAS HACER?");
                Console.WriteLine("1. Volver a jugar en el mismo modo");
                Console.WriteLine("2. Cambiar de modo de juego");
                Console.WriteLine("3. Salir");
                Console.WriteLine("================================");
                Console.Write("Selecciona una opción: ");

                string respuesta = Console.ReadLine();

                if (respuesta == "1")
                {
                    jugarDeNuevo = true;
                }
                else if (respuesta == "2")
                {
                    modo = 0;
                    jugarDeNuevo = true;
                }
                else if (respuesta == "3")
                {
                    jugarDeNuevo = false;
                }
                else
                {
                    Console.WriteLine("Opción no válida. El juego terminará.");
                    jugarDeNuevo = false;
                }
            }

            Console.WriteLine("¡Gracias por jugar!");
        }

        static string ObtenerPalabraAleatoria()
        {
            string[] palabras =
            {
                "programacion", "computadora", "desarrollo",
                "variable", "algoritmo", "teclado",
                "consola", "csharp", "internet",
                "videojuego", "framework", "codigo",
                "sistema", "monitor"
            };

            int posicion = random.Next(0, palabras.Length);
            return palabras[posicion];
        }

        static string PedirPalabra()
        {
            while (true)
            {
                Console.Write("Jugador 1, ingresa una palabra sin que te vea el Jugador 2: ");
                string palabra = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(palabra))
                {
                    Console.WriteLine("No puedes dejar el espacio vacío.");
                    continue;
                }

                palabra = palabra.Trim().ToLowerInvariant();
                bool esValida = true;

                foreach (char letra in palabra)
                {
                    if (!char.IsLetter(letra))
                    {
                        esValida = false;
                        break;
                    }
                }

                if (esValida)
                {
                    Console.Clear();
                    return palabra;
                }

                Console.WriteLine("Ingresa una palabra que contenga solamente letras.");
            }
        }

        static void JugarAhorcado(string palabra, int modo)
        {
            char[] letras = palabra.ToCharArray();
            bool[] letrasDescubiertas = new bool[letras.Length];
            HashSet<char> letrasUsadas = new HashSet<char>();
            int intentos = 6;

            while (true)
            {
                Console.WriteLine("Intentos restantes: " + intentos);
                MostrarPalabra(letras, letrasDescubiertas);
                Console.WriteLine("Letras utilizadas: " + MostrarLetrasUsadas(letrasUsadas));

                if (TodasLasLetrasDescubiertas(letrasDescubiertas))
                {
                    Console.WriteLine("¡Has ganado el juego!");
                    if (modo == 2)
                    {
                        Console.WriteLine("¡Ha ganado el Jugador 2!");
                    }
                    break;
                }

                char letra = PedirLetra(letrasUsadas);
                letrasUsadas.Add(letra);
                bool letraEncontrada = false;

                for (int i = 0; i < letras.Length; i++)
                {
                    if (letras[i] == letra)
                    {
                        letrasDescubiertas[i] = true;
                        letraEncontrada = true;
                    }
                }

                if (letraEncontrada)
                {
                    Console.WriteLine("¡Correcto! La letra sí está en la palabra.");
                }
                else
                {
                    intentos--;
                    Console.WriteLine("La letra no está en la palabra. Pierdes un intento.");
                }

                if (intentos == 0)
                {
                    Console.WriteLine("Game Over. Se terminaron tus intentos.");
                    Console.WriteLine("La palabra era: " + palabra);

                    if (modo == 2)
                    {
                        Console.WriteLine("¡Ha ganado el Jugador 1!");
                    }
                    break;
                }

                Console.WriteLine();
            }
        }

        static void MostrarPalabra(char[] letras, bool[] letrasDescubiertas)
        {
            for (int i = 0; i < letras.Length; i++)
            {
                if (letrasDescubiertas[i])
                {
                    Console.Write(" " + letras[i] + " ");
                }
                else
                {
                    Console.Write(" _ ");
                }
            }

            Console.WriteLine();
        }

        static char PedirLetra(HashSet<char> letrasUsadas)
        {
            while (true)
            {
                Console.Write("Ingresa una letra: ");
                string entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("No puedes dejar el espacio vacío.");
                    continue;
                }

                entrada = entrada.Trim();

                if (entrada.Length != 1 || !char.IsLetter(entrada[0]))
                {
                    Console.WriteLine("Debes ingresar solamente una letra.");
                    continue;
                }

                char letra = char.ToLowerInvariant(entrada[0]);

                if (letrasUsadas.Contains(letra))
                {
                    Console.WriteLine("Esa letra ya fue utilizada. Intenta con otra.");
                    continue;
                }

                return letra;
            }
        }

        static string MostrarLetrasUsadas(HashSet<char> letrasUsadas)
        {
            if (letrasUsadas.Count == 0)
            {
                return "Ninguna";
            }

            List<char> letrasOrdenadas = new List<char>(letrasUsadas);
            letrasOrdenadas.Sort();
            return string.Join(", ", letrasOrdenadas);
        }

        static bool TodasLasLetrasDescubiertas(bool[] letrasDescubiertas)
        {
            for (int i = 0; i < letrasDescubiertas.Length; i++)
            {
                if (!letrasDescubiertas[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
