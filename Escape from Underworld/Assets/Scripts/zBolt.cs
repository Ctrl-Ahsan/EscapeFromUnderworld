using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zBolt : MonoBehaviour
{
    private DialogueManager dMan;
    public string[] dialogueLines;
    private int angle = 0;

    private void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (dMan == null) dMan = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.zeusBolt = true;
            UI.permanent.boltIndic.SetActive(true);
            dMan.dialogueLines = dialogueLines;
            dMan.currentLine = 0;
            dMan.ShowDialogue();
            Destroy(gameObject);
        }
    }

    
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, angle, 0);
        angle = +3;
    }
}
