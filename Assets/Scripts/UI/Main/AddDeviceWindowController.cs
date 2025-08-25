using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using App.UI;

public class AddDeviceWindowController : ViewController
{
  [SerializeField] private TMP_InputField nameField;
  [SerializeField] private TMP_InputField nameShortField;
  [SerializeField] private Button submitButton;
  [SerializeField] private Button cancelButton;
  [SerializeField] private Button crossButton;

  private bool canClose = false;

  public void MarkClosingByChain()
  {
    canClose = true;
  }

  public override void Init(IWindowStarter starter)
  {
    base.Init(starter);
    submitButton.onClick.AddListener(OnSubmit);
    cancelButton.onClick.AddListener(Close);
    crossButton.onClick.AddListener(Close);
  }
  public override void Close()
  {
    if (canClose)
    {
      base.Close();
      return;
    }

    bool hasName = !string.IsNullOrWhiteSpace(nameField.text);
    bool hasShortName = !string.IsNullOrWhiteSpace(nameShortField.text);

    if (hasName || hasShortName)
    {
      var starter = new GenericStarter("Main", "AttentionWindow");
      var window = WindowsManager.Instance.CreateWindow<ViewController>(this, starter);
      window?.Show();
    }
    else base.Close();
  }

  private void OnSubmit()
  {
    if (nameField != null)
    {
      string nameInput = nameField.text;
      Debug.Log("Add device name: " + nameInput);
      canClose = true;
    }
    else
    {
      Debug.Log("No device Name");
    }
    if (nameShortField != null)
    {
      string nameShortInput = nameShortField.text;
      Debug.Log("Add device short name: " + nameShortInput);
    }
    else Debug.Log("No device ShortName");
    Close();
  }
}
