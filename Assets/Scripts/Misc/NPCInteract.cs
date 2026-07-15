using System;
using UnityEngine;
using Yarn.Unity;

public class NPCInteract : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private string nodeName;

    private bool playerInRange;

    private void Start()
    {
        GameInput.Instance.OnPlayerInteract += GameInput_OnPlayerInteract;
    }

    private void OnDestroy()
    {
        if (GameInput.Instance != null)
            GameInput.Instance.OnPlayerInteract -= GameInput_OnPlayerInteract;
    }

    private void GameInput_OnPlayerInteract(object sender, EventArgs e)
    {
        if (!playerInRange) return;
        if (dialogueRunner.IsDialogueRunning) return;

        dialogueRunner.StartDialogue(nodeName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}