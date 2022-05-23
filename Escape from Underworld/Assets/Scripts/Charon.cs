using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charon : MonoBehaviour
{
    public DialogueManager dMan;
    public static bool intro = true;
    public static bool doneIntro;
    public string[] introText;
    public string[] baseDialogue;
    public string[] hermesBoots;
    public string[] zeusBolt;
    public string[] hadesFlame;
    private bool inRange;

    private void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (dMan == null) dMan = FindObjectOfType<DialogueManager>();

        if (Input.GetKeyDown(KeyCode.UpArrow) && inRange)
        {
            if (!dMan.dialogueActive)
            {
                if (Player.hadesFlame) dMan.dialogueLines = hadesFlame;
                else if (Player.zeusBolt) dMan.dialogueLines = zeusBolt;
                else if (Player.hermesBoots) dMan.dialogueLines = hermesBoots;
                else if (doneIntro) dMan.dialogueLines = baseDialogue;
                else if (intro) dMan.dialogueLines = introText;
                doneIntro = true;
                dMan.currentLine = 0;
                dMan.ShowDialogue();
                UI.permanent.message.SetActive(false);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
        UI.permanent.message.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        UI.permanent.message.SetActive(false);
    }
}
