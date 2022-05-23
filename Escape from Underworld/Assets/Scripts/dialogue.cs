using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue : MonoBehaviour
{
    private DialogueManager dMan;
    public string[] dialogueLines;
    private bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dMan == null) dMan = FindObjectOfType<DialogueManager>();

        if (inRange && Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (!dMan.dialogueActive)
            {
                dMan.dialogueLines = dialogueLines;
                dMan.currentLine = 0;
                dMan.ShowDialogue();
                UI.permanent.message.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            UI.permanent.message.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            UI.permanent.message.SetActive(false);
        }

    }
}
