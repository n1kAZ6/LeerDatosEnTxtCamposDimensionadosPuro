/*Leer Datos En Fichero.Txt Con Campos dimensionados puros (sin salto de línea)

Si te has dado cuenta, en la práctica anterior, los datos no estaban guardados realmente 
como campos dimensionados: se les había colocado un separador de registros (el salto de línea).
Realiza un programa que lea el fichero AlumNotas_CDpuro.txt  que tienes en la carpeta Datos. 
Este archivo contiene los datos que se guardaron en la práctica de escritura de datos 
con campos dimensionados P34a (pero sin saltos de líneas):
		Dato →			id	Apellidos  Nombre  Nota1  Nota2  Nota3
		Nº Caracteres	3	   18	     12	     3	    3	   3

A partir de estos datos, rellena las tres tablas siguientes:
  ●	Una de byte tabIds
  ●	otra de string tabAlums, con los «Apellidos, Nombre» de cada alumno, 
  ●	y otra de float tabNotas de tres columnas.

A continuación presentar los datos con su cabecera y la media de cada alumno
 Importante: 
 1.	Utilizar Ruta Relativa y mantener la estructura de archivos que se te entrega. 
 2.	El archivo debe estar abierto el menor tiempo posible.
 3.	Se puede utilizar una lista auxiliar pero tienes que actuar como si no se supiera 
    el número de alumnos que guarda el fichero.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Program
{
	static void Main(string[] args)
	{
		StreamReader sR = File.OpenText(".\\Datos\\AlumNotas_CDpuro.txt");
		string ficheroCompleto = sR.ReadToEnd();
		sR.Close();

		int tamañoTotalCaracteres = ficheroCompleto.Length;	//Tamaño total del string con todos
															//los registros dimensionados puros del fichero
		int numeroAlumnos = 0;

        for (int i = 0; i < tamañoTotalCaracteres; i += 42) //Para obtener el total de registros, en cada iteración 	
			numeroAlumnos++;                                //se cuenta un registro que tiene una dimensión total de 42


		//Declaramos los array con el tamaño de los registros antes hallado
        byte[] tabIds = new byte[numeroAlumnos]; 
        string[] tabAlums = new string[numeroAlumnos];
        float[,] tabNotas = new float[numeroAlumnos,3];

		int cont = 0; //Contador para que incremente en 1 por registro no en 42
		for (int i=0;i<tamañoTotalCaracteres;i+=42) 
		{
			tabIds[cont] =Convert.ToByte(ficheroCompleto.Substring(i,3));
			tabAlums[cont] = ficheroCompleto.Substring(i + 3,18).Trim()+","+ficheroCompleto.Substring(i + 21,12).Trim();
			tabNotas[cont, 0] = Convert.ToSingle(ficheroCompleto.Substring(i + 33, 3));
            tabNotas[cont, 1] = Convert.ToSingle(ficheroCompleto.Substring(i + 36, 3));
            tabNotas[cont, 2] = Convert.ToSingle(ficheroCompleto.Substring(i + 39, 3));
			cont++;
		}

        //-------------- Mostramos los datos  -----------------

        Console.WriteLine("\tId  Alumno\t\t\t\tProg    Ed      BD      Media");
        Console.WriteLine("\t------------------------------------------------------------------------");

		double media;
		for (int i=0;i<numeroAlumnos;i++) 
		{
			media = (tabNotas[i, 0] + tabNotas[i, 1] + tabNotas[i, 2]) / 3;
            Console.WriteLine("\t{0} {1}\t{2}\t{3}\t{4}\t{5}", tabIds[i],CuadrarTexto(tabAlums[i],35,'.'), tabNotas[i,0], tabNotas[i, 1], tabNotas[i, 2],Math.Round(media,2));
		}



        Console.WriteLine("\n\n\t Pulsa una tecla para salir");
		Console.ReadKey();
	}
	static string CuadrarTexto(string texto, int tamaño, char caracter) 
	{
		while (texto.Length < tamaño)
			texto += caracter;

		return texto.Substring(0,tamaño);
	}

}
