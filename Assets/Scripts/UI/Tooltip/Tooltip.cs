using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.EventSystems;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI headerField;
  [SerializeField] private TextMeshProUGUI contentField;
  [SerializeField] private LayoutElement layoutElement;

  [SerializeField] private int characterWrapLimit;

  [SerializeField] private RectTransform rectTransform;

  private void Awake()
  {
    if (rectTransform == null)
      rectTransform = GetComponent<RectTransform>();

    if (headerField == null)
      headerField = GetComponentInChildren<TextMeshProUGUI>();

    if (contentField == null)
      contentField = GetComponentInChildren<TextMeshProUGUI>();

    if (layoutElement == null)
      layoutElement = GetComponent<LayoutElement>();
  }

  public void SetPosition(RectTransform tooltipBoundsWindow)
  {
    Vector2 position = Input.mousePosition;
    float pivotX;
    float pivotY;

    Vector2 referenceSize;

    if (tooltipBoundsWindow != null)
    {
      referenceSize = tooltipBoundsWindow.rect.size;
    }
    else
    {
      referenceSize = new Vector2(Screen.width, Screen.height);
    }

    Canvas.ForceUpdateCanvases();
    LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    Vector2 tooltipSize = rectTransform.rect.size;

    pivotX = position.x < referenceSize.x / 3f ? 0f : 1f;
    pivotY = position.y < referenceSize.y / 3f ? 0f : 1f;

    float screenPadding = 20f;

    if (pivotX == 0f && position.x + tooltipSize.x > referenceSize.x - screenPadding)
      pivotX = 1f;
    else if (pivotX == 1f && position.x - tooltipSize.x < screenPadding)
      pivotX = 0f;

    if (pivotY == 0f && position.y + tooltipSize.y > referenceSize.y - screenPadding)
      pivotY = 1f;
    else if (pivotY == 1f && position.y - tooltipSize.y < screenPadding)
      pivotY = 0f;


    rectTransform.pivot = new Vector2(pivotX, pivotY);

    Vector2 offset = new Vector2(10f, -10f);
    offset.x *= pivotX == 0f ? 1f : -1f;
    offset.y *= pivotY == 0f ? 1f : -1f;

    transform.position = position + offset;
  }

  public void SetText(string content, string header = "")
  {
    if (headerField == null || contentField == null || layoutElement == null)
    {
      Debug.LogWarning("Tooltip: One or more UI fields are not assigned.");
      return;
    }

    if (string.IsNullOrEmpty(header))
    {
      headerField.gameObject.SetActive(false);
    }
    else
    {
      headerField.gameObject.SetActive(true);
      headerField.text = header;
    }

    contentField.text = content;

    if (Application.isEditor)
    {
      int headerLength = headerField.text.Length;
      int contentLength = contentField.text.Length;

      layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
    }
  }

  private void Update()
  {
    if (rectTransform == null)
    {
      rectTransform = GetComponent<RectTransform>();
      if (rectTransform == null)
      {
        Debug.LogWarning("Tooltip: RectTransform not found.");
        return;
      }
    }
    
    if (EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject())
    {
      TooltipSystem.Hide();
    }
  }
}
