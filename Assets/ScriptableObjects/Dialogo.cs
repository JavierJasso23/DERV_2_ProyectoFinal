using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Dialogo_", menuName = "Crear Dialogo", order = 1)]
public class Dialogo : ScriptableObject
{
    [System.Serializable]
    public struct Mensaje
    {
        public Personajes personaje;
        [TextArea(3, 5)]
        public string dialogo;
    }

    
    public Mensaje []mensaje;


   public Mensaje getDatosPersonaje(int i) {
        return (mensaje[i]);        
    }

    public int getCantidadMensajes() {
        return mensaje.Length;
    }


    
}
