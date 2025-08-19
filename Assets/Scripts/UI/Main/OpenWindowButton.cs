using UnityEngine;
using UnityEngine.UI;

public class OpenWindowButton : MonoBehaviour
{
  [SerializeField] private string group;
  [SerializeField] private string windowName;

  private void Awake()
  {
    Button btn = GetComponent<Button>();
    if (btn != null)
    {
      btn.onClick.AddListener(OnClick);
    }
    else
    {
      Debug.LogWarning("OpenWindowButton: Button component not found on " + gameObject.name);
    }
  }

  private void OnClick()
  {
    var starter = new GenericStarter(group, windowName);
    var window = WindowsManager.Instance.CreateWindow<ViewController>(starter);
    window?.Show();
  }
}