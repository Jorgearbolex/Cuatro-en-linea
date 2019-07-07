using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuatroEnLinea : MonoBehaviour
{

    private bool primerturno;
    int ancho = 10;
    int alto = 10;
    public GameObject pieza;
    private GameObject[,] esf;
    public Color colorfondo;
    public Color colorJugaUno;
    public Color colorJugaDos;
    public float speed = 1f;
    public float countdown = 60f;
    bool parar = true;





    public void Start()
    {
        esf = new GameObject[ancho, alto]; // este es el array donde van la creación del trablero 
        for (int i = 0; i < ancho; i++) //se crean los "for" para formar el tablero de esferas
        {
            for (int j = 0; j < alto; j++) //se crean los "for" para formar el tablero de esferas
            {
                GameObject esfera = GameObject.Instantiate(pieza) as GameObject;
                Vector3 position = new Vector3(i, j, 0);
                esfera.transform.position = position;

                esfera.GetComponent<Renderer>().material.color = colorfondo;

                esf[i, j] = esfera; 
            }
        }
    }

    public void Update()
    {
        if (parar == true)
        { 

            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // pregunta por la posición de forma constante del mouse
            SelecESfera(mPosition);


           if (parar == true) // desde acá comienza mi propuesta de condición, si se pasa de un minuto el juego se detiene
            { 
                countdown -= Time.deltaTime; // contador en cuenta regresiva 
                if (countdown == 0.0f)
                {
                    transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
                
                }
               if (countdown <= 0.0f) // si llega a cero se detiene con el boleano en falso
                {
                    parar = false;
               
                }
            }


        }




    }

    public void SelecESfera(Vector3 position) // eta es una función donde se crea el array y la verificación de cada eje (X y Y) y las diagonales
    {
        int i = (int)(position.x + 0.5f);
        int j = (int)(position.y + 0.5f);

        if (Input.GetButtonDown("Fire1")) // la función para clickear en las esferas 
        {
            if (i >= 0 && j >= 0 && i < ancho && j < alto) // se marcan los limites hasta donde operarn las verificaciones
            {
                GameObject esfera = esf[i, j];
                if (esfera.GetComponent<Renderer>().material.color == colorfondo)
                {
                    Color colorAUsar = Color.clear; 
                    if (primerturno)
                        colorAUsar = colorJugaUno;

                    else
                        colorAUsar = colorJugaDos;

                    esfera.GetComponent<Renderer>().material.color = colorAUsar;
                    primerturno = !primerturno; 
                    VerificadorX(i, j, colorAUsar);
                    VerificadorY(i, j, colorAUsar);
                    DiagoPositiva(i, j, colorAUsar);
                    DiagoNegativa(i, j, colorAUsar);
    
                }
            }
        }

        
    }


    public void VerificadorX(int x, int y, Color colorAVerificando) //para verificar en el eje X
    {
        int contador = 0; // se inicia el contador en cero
        for (int i = x-3; i <= x+3; i++)
        {
            if (i < 0 || i >= ancho)
                continue;

            GameObject esfera = esf[i, y]; 

            if (esfera.GetComponent<Renderer>().material.color == colorAVerificando)
            {
                contador++; // se incrementa las interacciones para sumar 4 del mismo color
                if (contador == 4) // llega a cuatro he impirme el texto en la consola
                {
                    Debug.Log("lo hiciste en X guiñapo");                  
                    parar = false; // para detener el juego se tiene que poner este boleano en cada eje y diagonal

                }
            }

            else
            {
                contador = 0;

            }
        }
    }

    public void VerificadorY(int x, int y, Color colorAVerificando) //función para verificar en el eje Y
    {
        int contador = 0;
        for (int j = y - 3; j <= y + 3; j++)
        {
            if (j < 0 || j >= alto)
                continue;

            GameObject esfera = esf[x, j];

            if (esfera.GetComponent<Renderer>().material.color == colorAVerificando)
            {
                contador++; // se incrementa las interacciones para sumar 4 del mismo color
                if (contador == 4) // llega a cuatro he impirme el texto en la consola
                {
                    Debug.Log("lo hiciste en Y guiñapo");
                    parar = false;

                }
            }

            else
            {
                contador = 0;

            }
            
        }
    }

    public void DiagoPositiva(int x, int y, Color colorAVerificando) // diagonal de verificación hacia arriba 
    {
        int contador = 0; // se inica el contador en cero
        int j = y - 3; // se crea este int para marcar los limtes de y en negativo


        for (int i = x - 3; i <= x + 3; i++)
        {
            if (j < alto && j >= 0 && i < ancho && i >= 0) // se marca los limites de la interacción
            {
               
                GameObject esfera = esf[i, j];

                if (esfera.GetComponent<Renderer>().material.color == colorAVerificando)
                {
                    contador++;

                    if (contador == 4)
                    {
                        Debug.Log("lo hiciste de nuevo guiñapo");
                        parar = false;

                    }
                    

                }

                else
                {
                    contador = 0;
                }

            }

            j++; // con esta se incrementa y se contraresta en el tablero para crear la diagonal ya que antes j (Y) esta en negativo -3
        }
    }

    public void DiagoNegativa(int x, int y, Color colorAVerificando) // diagonal de verificación hacia arriba
    {
        int contador = 0;  // se inica el contador en cero
        int j = y + 3; // se crea este int para marcar los limtes de y en negativo


        for (int i = x - 3; i <= x + 3; i++) // para crear la interacion de verificación desde eje X
        {

                if (j < alto && j >= 0 && i < ancho && i >= 0)
                {
                

                          GameObject esfera = esf[i, j]; // el objeto array 

                    if (esfera.GetComponent<Renderer>().material.color == colorAVerificando)
                    {
                        contador++;

                        if (contador == 4)
                        {
                            Debug.Log("lo hiciste de nuevo mequetrafe");
                             parar = false; 

                        }


                    }

                    else
                    {
                        contador = 0;
                    }

                }

            j--;
        }
     }

}