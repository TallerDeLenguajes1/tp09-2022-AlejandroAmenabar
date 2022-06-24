// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Text.Json;
namespace Ejercicio1;
public class Program{

    public static void Main(){

        Console.WriteLine("Ingrese la ruta a la carpeta que quiere leer: ");
        string?  ruta = Console.ReadLine();

        if(Directory.Exists(ruta)){
            List<string> ListaCarpetas = Directory.GetDirectories(ruta).ToList();

            foreach (string item in ListaCarpetas)
            {
                Console.WriteLine(item);
            }
            string NombreArchivo = ruta + @"\index.json";
            FileStream FS;
            if(!File.Exists(NombreArchivo)){
                FS=File.Create(NombreArchivo);
                FS.Close();
            }

            using (FS = new FileStream(NombreArchivo, FileMode.OpenOrCreate))
            using (var streamWriter = new StreamWriter(FS))
            {
                var contador = 0;

                var carpetas = new List<Carpeta>();
                var archivos = new List<Archivo>();

                foreach(string auxiliar in ListaCarpetas){
                    var carpeta = new Carpeta(contador, new DirectoryInfo(auxiliar).Name);
                    carpetas.Add(carpeta);
                    contador++;
                }

                foreach(string auxiliar in Directory.GetFiles(ruta)){
                    var archivo = new Archivo(contador, Path.GetFileNameWithoutExtension(auxiliar), Path.GetExtension(auxiliar));
                    archivos.Add(archivo);
                    contador++;
                }

                string carpetaJson = JsonSerializer.Serialize(carpetas);
                string archivoJson = JsonSerializer.Serialize(archivos);
                streamWriter.WriteLine(carpetaJson);
                streamWriter.WriteLine(archivoJson);
            }

        }
    }

}