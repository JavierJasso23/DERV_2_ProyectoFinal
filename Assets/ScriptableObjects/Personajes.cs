using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Personaje_", menuName = "Crear Personaje", order = 1)]

public class Personajes : ScriptableObject
{    
   public string nombre;
   public Sprite imagen;
}
