using UnityEngine;

public class MailBox : Interactive
{
     private SpriteRenderer spriteRenderer;

     private BoxCollider2D coll;
     public Sprite openSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }
  protected override void OnClickedAction()
  {
    spriteRenderer.sprite = openSprite;
    transform.GetChild(0).gameObject.SetActive(true);
  }

  private void OnEnable()
  {
    EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
  }

  private void OnDisable()
  {
    EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
  }

  private void OnAfterSceneLoadEvent()
  {
    if (!isDone)
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    else
    {
        spriteRenderer.sprite = openSprite;
        coll.enabled = false;
    }
  }
}
