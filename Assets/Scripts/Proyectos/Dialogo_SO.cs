using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogo_SO : MonoBehaviour
{
    [SerializeField]
    Dialogo dialogo;

    public int indexActual;

    public GameObject contenedorImagen;
    public Image image;

    [SerializeField]
    public GameObject contenedorDialogo;

    [SerializeField]
    public TextMeshProUGUI texto;

    [SerializeField]
    Sprite imgDefecto;

    bool YouCan;
    [SerializeField]
    bool activoBTN;
    [SerializeField]
    bool activoQ;

    [SerializeField]
    GameObject habilitarBTN;

    //btnsRESP
    [SerializeField]
    TextMeshProUGUI respuesta1;
    [SerializeField]
    TextMeshProUGUI respuesta2;
    [SerializeField]
    TextMeshProUGUI respuesta3;
    [SerializeField]
    TextMeshProUGUI respuesta4;

    [SerializeField]
    Respuestas txtRespuestas;

    public int indexActResp;

    bool YouCanAnswer;

    int iCorrectas = 0;
    int[] correctas = { 1, 3, 2, 1, 4, 3, 3, 2, 1, 3 };

    string answ;

    [SerializeField]
    TextMeshProUGUI txtVidaSpidey;
    [SerializeField]
    TextMeshProUGUI txtVidaVenom;

    int vidaSpidey = 100;
    int vidaVenom = 100;

    private void Awake()
    {
        contenedorImagen = GameObject.Find("Contenedor");
        image = contenedorImagen.GetComponentInChildren<Image>();
    }

    void Start()
    {
        indexActual = -1;
        indexActResp = -1;
        YouCanAnswer = false;
        PrimerDialogo();
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

        if (Input.GetKeyDown(KeyCode.G) && indexActual == 13)
        {
            if (vidaSpidey > vidaVenom)
            {
                texto.text = "¡No puede ser! Ganaste... pero solo por esta vez...";
                texto.maxVisibleCharacters = 0;
                StartCoroutine("mostrarCorInc");
            }
            if (vidaVenom > vidaSpidey)
            {
                texto.text = "¿¡Perdiste!? ¡Al fin te gane Spiderman!";
                texto.maxVisibleCharacters = 0;
                StartCoroutine("mostrarCorInc");
            }
            if (vidaVenom == vidaSpidey)
            {
                texto.text = "Oh, bueno, esto es un empate. Vamos por pizza. ¿Si?";
                texto.maxVisibleCharacters = 0;
                StartCoroutine("mostrarCorInc");
            }
            indexActual++;
            YouCan = false;
            habilitarBTN.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q) && activoQ)
        {
            contenedorDialogo.SetActive(false);
            indexActual = -1;
            texto.text =  "Sin Texto";
            image.sprite = imgDefecto;
            StopCoroutine("mostrarTexto");
            YouCan = true;
        }

        //Respuestas
        if (Input.GetKeyDown(KeyCode.Alpha1) && YouCanAnswer)
        {
            ComprobarRespuesta(1);
            YouCanAnswer = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && YouCanAnswer)
        {
            ComprobarRespuesta(2);
            YouCanAnswer = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && YouCanAnswer)
        {
            ComprobarRespuesta(3);
            YouCanAnswer = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && YouCanAnswer)
        {
            ComprobarRespuesta(4);
            YouCanAnswer = false;
        }
    }

    IEnumerator mostrarTexto()
    {
        while (true)
        {
            float largo = texto.text.Length;
            if (texto.maxVisibleCharacters < largo)
            {
                texto.maxVisibleCharacters += 1;
            }
            if (texto.maxVisibleCharacters == largo)
            {
                if (indexActual >= 2 && indexActual < 12)
                {
                    MostrarRespuestas();
                    YouCanAnswer = true;
                }
                else
                {
                    YouCan = true;
                }
                StopCoroutine("mostrarTexto");
            }
            yield return new WaitForSeconds(.01f);
        }
    }

    IEnumerator mostrarCorInc()
    {
        while (true)
        {
            float largo = texto.text.Length;
            if (texto.maxVisibleCharacters < largo)
            {
                texto.maxVisibleCharacters += 1;
            }
            if (texto.maxVisibleCharacters == largo)
            {
                respuesta1.text = "1";
                respuesta2.text = "2";
                respuesta3.text = "3";
                respuesta4.text = "4";
                YouCan = true;
                StopCoroutine("mostrarCorInc");
            }
            yield return new WaitForSeconds(.01f);
        }
    }

    public void ComprobarRespuesta(int n)
    {
        if (n == correctas[iCorrectas])
        {
            vidaVenom -= 10;
            txtVidaVenom.text = vidaVenom.ToString() + "/100";
            texto.text = "Muy bien Spidey, respuesta correcta...";
            texto.maxVisibleCharacters = 0;
            StartCoroutine("mostrarCorInc");
        }
        else
        {
            vidaSpidey -= 10;
            txtVidaSpidey.text = vidaSpidey.ToString() + "/100";
            texto.text = "¡Mal Spidey!, respuesta incorrecta...";
            texto.maxVisibleCharacters = 0;
            StartCoroutine("mostrarCorInc");
        }
        iCorrectas++;
    }

    public void MostrarRespuestas()
    {
        indexActResp++;
        respuesta1.text = txtRespuestas.getDatosRespuesta(indexActResp).dialogo;
        indexActResp++;
        respuesta2.text = txtRespuestas.getDatosRespuesta(indexActResp).dialogo;
        indexActResp++;
        respuesta3.text = txtRespuestas.getDatosRespuesta(indexActResp).dialogo;
        indexActResp++;
        respuesta4.text = txtRespuestas.getDatosRespuesta(indexActResp).dialogo;
    }

}
