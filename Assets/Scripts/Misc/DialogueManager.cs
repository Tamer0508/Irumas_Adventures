using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;

    private void Awake()
    {
        if (dialogueRunner == null)
        {
            dialogueRunner = FindFirstObjectByType<DialogueRunner>();
        }
    }

    private void Start()
    {
        dialogueRunner.onDialogueStart.AddListener(HandleDialogueStart);
        dialogueRunner.onDialogueComplete.AddListener(HandleDialogueComplete);
    }

    private void HandleDialogueStart()
    {
        Debug.Log("DisableMovement called");
        GameInput.Instance.DisableMovement();
    }

    private void HandleDialogueComplete()
    {
        Debug.Log("EnableMovement called");
        GameInput.Instance.EnableMovement();
    }

    private void OnDestroy()
    {
        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueStart.RemoveListener(HandleDialogueStart);
            dialogueRunner.onDialogueComplete.RemoveListener(HandleDialogueComplete);
        }
    }
}