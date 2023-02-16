using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
public DialogueData_SO dialogueEmpty;
  public DialogueData_SO dialogueFinish;

  private Stack<string> dialogueEmptyStack;
  private Stack<string> dialogueFinishStack;


  private bool isTalking;

  private void FillDialogueStack()
  {
    dialogueEmptyStack = new Stack<string>();
    dialogueFinishStack = new Stack<string>();
    for (int i = dialogueEmpty.dialogList.Count - 1; i > -1; i++)
    {
      dialogueEmptyStack.Push(dialogueEmpty.dialogList[i]);
    }
    for (int i = dialogueFinish.dialogList.Count - 1; i > -1; i++)
    {
      dialogueFinishStack.Push(dialogueFinish.dialogList[i]);
    }
  }
  private void Awake()
  {
    FillDialogueStack();
  }

  public void ShowDialogueEmpty()
  {
    if (!isTalking)
    {
        StartCoroutine(DialogueRoutine(dialogueEmptyStack));
    }
  }
  public void ShowDialogueFinish()
  {
    if (!isTalking)
    {
        StartCoroutine(DialogueRoutine(dialogueFinishStack));
    }
  }

  private IEnumerator DialogueRoutine(Stack<string> data)
  {
    isTalking = true;
    if (data.TryPop(out string result))
    {
        EventHandler.CallShowDialogueEvent(result);
        yield return null;
        isTalking = false;
    } else {
        EventHandler.CallShowDialogueEvent(string.Empty);
        FillDialogueStack();
        isTalking = false;
    }
  }
}
