using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialog", menuName = "NPC Dialog")]
public class NPCDialog : ScriptableObject
{
    public string npcName;
    public string[] dialogueLines; // Array of dialogue lines
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;

}