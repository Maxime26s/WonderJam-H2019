using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newNPC", menuName = "NPC")]
public class NPCScriptable : ScriptableObject
{
    public string nom;

    public string[] phrases;
    public bool[] reponse;
    public string[] choix;
    public bool[] required;
    public int[] amount;


    public Sprite npcSprite;

}
