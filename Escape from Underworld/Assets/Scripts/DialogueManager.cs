using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dBox;
    public Text dText;
    public bool dialogueActive;
    public string[] dialogueLines = new string[10];
    public int currentLine;


    // Update is called once per frame
    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
        }

        if (currentLine >= dialogueLines.Length)
        {
            dBox.SetActive(false);
            dialogueActive = false;

            currentLine = 0;
        }

        dText.text = dialogueLines[currentLine];
    }

    public void ShowBox(string dialogue)
    {
        
        dBox.SetActive(true);
        dText.text = dialogue;
        dialogueActive = true;
    }

    public void ShowDialogue()
    {
        dialogueActive = true;
        dBox.SetActive(true);
    }
}
