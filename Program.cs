using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=========================================");
        Console.WriteLine(" SOLUCIÓN DE SISTEMAS POR GAUSS-JORDAN");
        Console.WriteLine("=========================================\n");

        // 3 ecuaciones y 3 incógnitas
        SolverMatriz solver = new SolverMatriz(3, 4);

        solver.Mostrar("Matriz Original:");

        solver.ResolverGauss();
        solver.Mostrar("\nMatriz Escalonada (Gauss):");

        solver.ResolverGaussJordan();
        solver.Mostrar("\nMatriz Escalonada Reducida (Gauss-Jordan):");

        solver.MostrarSoluciones();

        Console.ReadKey();
    }
}

class SolverMatriz
{
    public double[,] Matriz;
    private int filas;
    private int columnas;

    public SolverMatriz(int f, int c)
    {
        filas = f;
        columnas = c;
        Matriz = new double[f, c];

        LlenarManual();
    }

    // Captura de datos
    private void LlenarManual()
    {
        Console.WriteLine("Ingrese los valores de la matriz aumentada:\n");

        for (int i = 0; i < filas; i++)
        {
            Console.WriteLine($"Fila {i + 1}");

            for (int j = 0; j < columnas; j++)
            {
                Console.Write($"Elemento [{i + 1},{j + 1}]: ");
                Matriz[i, j] = double.Parse(Console.ReadLine());
            }

            Console.WriteLine();
        }
    }

    // Eliminación de Gauss
    public void ResolverGauss()
    {
        for (int i = 0; i < filas; i++)
        {
            // Buscar el mejor pivote
            int max = i;

            for (int k = i + 1; k < filas; k++)
            {
                if (Math.Abs(Matriz[k, i]) > Math.Abs(Matriz[max, i]))
                {
                    max = k;
                }
            }

            IntercambiarFilas(i, max);

            // Validar pivote
            if (Matriz[i, i] == 0)
            {
                Console.WriteLine("\nError: Existe un pivote igual a cero.");
                return;
            }

            // Hacer ceros debajo del pivote
            for (int k = i + 1; k < filas; k++)
            {
                double factor = Matriz[k, i] / Matriz[i, i];

                for (int j = i; j < columnas; j++)
                {
                    Matriz[k, j] = Matriz[k, j] - factor * Matriz[i, j];
                }
            }
        }
    }

    // Gauss-Jordan
    public void ResolverGaussJordan()
    {
        for (int i = filas - 1; i >= 0; i--)
        {
            double pivote = Matriz[i, i];

            if (pivote == 0)
            {
                Console.WriteLine("\nNo se puede continuar. El pivote es cero.");
                return;
            }

            // Convertir el pivote en 1
            for (int j = i; j < columnas; j++)
            {
                Matriz[i, j] /= pivote;
            }

            // Hacer ceros arriba del pivote
            for (int k = i - 1; k >= 0; k--)
            {
                double factor = Matriz[k, i];

                for (int j = i; j < columnas; j++)
                {
                    Matriz[k, j] -= factor * Matriz[i, j];
                }
            }
        }
    }

    // Intercambiar filas
    private void IntercambiarFilas(int f1, int f2)
    {
        if (f1 == f2)
            return;

        for (int j = 0; j < columnas; j++)
        {
            double aux = Matriz[f1, j];
            Matriz[f1, j] = Matriz[f2, j];
            Matriz[f2, j] = aux;
        }
    }

    // Mostrar matriz
    public void Mostrar(string titulo)
    {
        Console.WriteLine("\n" + titulo);

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                Console.Write($"{Matriz[i, j],10:F2}");
            }

            Console.WriteLine();
        }
    }

    // Mostrar soluciones
    public void MostrarSoluciones()
    {
        Console.WriteLine("\n=================================");
        Console.WriteLine("SOLUCIONES");
        Console.WriteLine("=================================");

        for (int i = 0; i < filas; i++)
        {
            Console.WriteLine($"X{i + 1} = {Matriz[i, columnas - 1]:F4}");
        }
    }
}
