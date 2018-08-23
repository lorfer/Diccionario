/*
    Fernando Garcia
    100339356
    Proyecto Final Programacion I
    PF090818
 */

using System;
using System.IO;

namespace Diccionario
{
    class Diccionario
    {
        private string palabra; //20
        private string definicion;//150
        //Total de 170

        // Metodo Constructor de la clase
        public Diccionario() { }

        public void setPalabra(string palabra)
        {
            this.palabra = palabra;
        }

        public string getPalabra()
        {
            return palabra;
        }
     
        public string getDefinicion()
        {
            return definicion;
        }

        public void setDefinicion(string definicion)
        {
            this.definicion = definicion;
        }

        public int getRecordSize()
        {
            return 170;
        }
       
		// Metodo Para agregar Definicion al archivo
        public void agregarAlArchivo(BinaryWriter bw)
        {
            //variable temporarl
            string varTemp;

            if (palabra != null && palabra.Length > 20)
                varTemp = palabra.Substring(0, 20);
            else
                varTemp = palabra.PadRight(20);
            bw.Write(varTemp);

            string varTemp2;
            if (definicion != null && definicion.Length > 150)
                varTemp2 = definicion.Substring(0, 150);
            else
                varTemp2 = definicion.PadRight(150);
            bw.Write(varTemp2);


        }

     // Metodo que lee los valores del binaryReader y se lo asigna a los campos de la clase
        public void leerDefinicion(BinaryReader br)
        {
            palabra = br.ReadString();
            definicion = br.ReadString();
           
        }
        
                   
        //Metodo para buscar palabras devuelve True  si la encuentra de lo contrario false
        public bool buscarPalabra(string buscar, FileStream fs, BinaryReader br)
        { 
              Diccionario dc = new Diccionario();
                //variable temporal
                string temp;
                          
                 try
                 {
                   
                    for(int i = 0; i<fs.Length/getRecordSize(); i++ )
                    {
                         leerDefinicion(br);
                        temp = getPalabra();
                        temp = temp.Substring(0, buscar.Length);

                        if(buscar == temp)
                        {
                          Console.WriteLine("Registro No.->" + (i+1) + "\n\n") ;
                          Console.WriteLine( getPalabra() + ":" + getDefinicion());
                         //exist =  true;
                        }     
                    }  
                 }
                    catch(EndOfStreamException)
                    {
                        return false;
                    }
                    return false;
        }
        //Metodo Para Obtener cantidad y longitud del archivo
        public void cantidadDeRegistro()
        {
             FileStream fs = new FileStream("Diccionario.bin", FileMode.OpenOrCreate, FileAccess.Read);
            Console.WriteLine("\n\tCANTIDAD ACTUAL DE REGISTROS = " + fs.Length / getRecordSize());
            Console.WriteLine("\n\t TAMAñO ACTUAL DE REGISTROS = " + fs.Length);
            fs.Close();
        }
       
    }

    class program
    {
        public static void Main(string[] args)
        {
            
            Diccionario dc = new Diccionario();
            string res = "n";
            string buscar ;

             menu:
            Console.WriteLine("\tBIENVENIDO A TU DICCIONARIO");
            dc.cantidadDeRegistro();
            Console.Write("Que Quieres hacer => \n");
            Console.WriteLine("1) Agregar Definicion\n2)buscar palabra\n3)Salir");

            string resp = Console.ReadLine();

            switch (resp)
            {
                case "1":
               // agregar:
                   FileStream fs = new FileStream("Diccionario.bin", FileMode.Append, FileAccess.Write);
                   BinaryWriter bw = new BinaryWriter(fs);
                    
                    while (res != "n")
                    {
                        System.Console.Write("Ingresa la palabra -> ");
                        dc.setPalabra(Console.ReadLine());

                        System.Console.Write("Su definicion: ");
                        dc.setDefinicion(Console.ReadLine());
                        dc.agregarAlArchivo(bw);

                        Console.Write("Quieres agregar otra palabra? y/n => ");
                        res = Console.ReadLine();
                        
                    }
                    fs.Close();
                    Console.Clear();
                    res = "y";
                    
                    goto menu;

                case "2":
                      Console.Clear();
                      //Si el archivo no existe lo crea o simplemente lo habre
                            fs = new FileStream("Diccionario.bin",FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            BinaryReader  br = new BinaryReader(fs);
                            bw = new BinaryWriter(fs);

                                                       
                            Console.Write("ingresa la palabra a buscar -> ");
                            buscar = Console.ReadLine();
                            
                            
                            if( dc.buscarPalabra(buscar, fs, br) == false)
                            {
                                Console.Write("No se encontro<"+ buscar+">\n quieres agregarla al diccionario? y/n -> ");
                                res = Console.ReadLine();
                                if(res == "y")
                                {
                                    dc.setPalabra(buscar);
                                    Console.Write("Definicion ->");
                                    dc.setDefinicion(Console.ReadLine());
                                    dc.agregarAlArchivo(bw);
                                    fs.Close();
                                }
                                Console.Write("Quieres volver al menu y/n ->");
                                res = Console.ReadLine();
                                if(res == "y")
                                {
                                    fs.Close();
                                    Console.Clear(); 
                                    goto menu;
                                }
                            }
                            
                    fs.Close();
                    break;
                    // Console.Clear();
                    // goto menu;
                case "3":

                    Console.WriteLine("Gracias Por usar el DICCIONARIO ELECTRONICO!");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Esta no es una Opcion!!");
                    break;
            }

        }

    }

    }
    