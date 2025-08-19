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

    Vector2 position = Input.mousePosition;

    float pivotX = position.x / Screen.width;
    float pivotY = position.y / Screen.height;

    rectTransform.pivot = new Vector2(pivotX, pivotY);
    transform.position = position;

    if (EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject())
    {
      TooltipSystem.Hide();
    }
  }

}
