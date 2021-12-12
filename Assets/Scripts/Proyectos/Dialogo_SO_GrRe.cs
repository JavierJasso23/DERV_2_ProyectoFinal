using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogo_SO_GrRe : MonoBehaviour
{
    [SerializeField]
    Dialogo dialogo;
    [SerializeField]
    Dialogo dialogo2;

    public int indexActual;


    public GameObject contenedorImagen;
    public Image image;

    [SerializeField]
    public GameObject contenedorDialogo;

    [SerializeField]
    public TextMeshProUGUI texto;

    //[SerializeField]
    //Sprite imgDefecto;

    bool YouCan;
    [SerializeField]
    bool activoBTN;
    [SerializeField]
    bool activoQ;

    [SerializeField]
    GameObject habilitarBTN;

    [SerializeField]
    GameObject caracts;

    private void Awake()
    {
        caracts = GameObject.Find("caract");
        GameObject.Find("Contenedor");
        image = contenedorImagen.GetComponentInChildren<Image>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        indexActual = -1;
        PrimerDialogo();
        //contenedorDialogo.SetActive(false);
        //YouCan = true;
    }

    void PrimerDialogo()
    {
        YouCan = false;
        indexActual++;
        image.sprite = dialogo.getDatosPersonaje(indexActual).personaje.imagen;
        texto.text = dialogo.getDatosPersonaje(indexActual).dialogo;
        texto.richText = true;
        texto.maxVisibleCharacters = 0;
        StopAllCoroutines();
        StartCoroutine("mostrarTexto");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dialogo.getCantidadMensajes());

        if (Input.GetKeyDown(KeyCode.F) && YouCan) //atras
        {
            contenedorDialogo.SetActive(true);
            if (indexActual > 0)
            {
                YouCan = false;
                indexActual--;
                image.sprite = dialogo.getDatosPersonaje(indexActual).personaje.imagen;  //la forma más correcta es esta
                //image.sprite = dialogo.mensaje[indexActual].personaje.imagen;
                texto.text = dialogo.getDatosPersonaje(indexActual).dialogo;
                texto.richText = true;
                texto.maxVisibleCharacters = 0;
                StopAllCoroutines();
                StartCoroutine("mostrarTexto");
            }
            else {
                    //de momento, no se hace nada
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && YouCan) //adelante
        {
            contenedorDialogo.SetActive(true);
            if (indexActual < dialogo.getCantidadMensajes()-1)
            {
                YouCan = false;
                indexActual++;
                image.sprite = dialogo.getDatosPersonaje(indexActual).personaje.imagen;
                texto.text = dialogo.getDatosPersonaje(indexActual).dialogo;
                texto.richText = true;
                texto.maxVisibleCharacters = 0;
                StopAllCoroutines();
                StartCoroutine("mostrarTexto");
            }
            else {
                //de momento, no se hace nada
            }
        }

        if (indexActual == dialogo.getCantidadMensajes()-1 && activoBTN)
        {
            Debug.Log("Habilitar boton");
            habilitarBTN.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q) && activoQ)
        {
            contenedorDialogo.SetActive(false);
            indexActual = -1;
            texto.text =  "Sin Texto";
            //image.sprite = imgDefecto;
            StopCoroutine("mostrarTexto");
            YouCan = true;
            dialogo = dialogo2;
            activoBTN = true;
            caracts.SetActive(false);
        }

    }

    IEnumerator mostrarTexto()
    {
        while (true)
        {
            //Tarea para 19 octubre : 1 . detener corrutina una vez que termina su objetivo
             //                       2 . asegurarse de no poder mostrar un mensaje hasta que el anterior   
             //                           haya sido mostrado completamente

            float largo = texto.text.Length;
            if (texto.maxVisibleCharacters < largo)
            {
                texto.maxVisibleCharacters += 1;
            }
            if (texto.maxVisibleCharacters == largo)
            {
                YouCan = true;
                StopCoroutine("mostrarTexto");
            }
            Debug.Log("Ejecución Corrutina");
            yield return new WaitForSeconds(.01f);
        }
    }


}
