using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Respuestas_", menuName = "Crear Respuesta", order = 1)]
public class Respuestas : ScriptableObject
{
    [System.Serializable]
    public struct Mensaje
    {
        [TextArea(3, 5)]
        public string dialogo;
    }

    
    public Mensaje []mensaje;


   public Mensaje getDatosRespuesta(int i) {
        return (mensaje[i]);        
    }

    public int getCantidadMensajes() {
        return mensaje.Length;
    }


    
}
