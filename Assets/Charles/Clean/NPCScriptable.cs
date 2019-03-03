using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newNPC", menuName = "NPC")]
public class NPCScriptable : ScriptableObject
{
    public string nom;

    public string[] phrases;

    public Sprite npcSprite;

}
