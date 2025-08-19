using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddDeviceWindowController : ViewController
{
  [SerializeField] private TMP_InputField nameField;
  [SerializeField] private TMP_InputField nameShortField;
  [SerializeField] private Button submitButton;
  [SerializeField] private Button cancelButton;
  [SerializeField] private Button crossButton;


  private void Start()
  {
    submitButton.onClick.AddListener(OnSubmit);
    cancelButton.onClick.AddListener(Close);
    crossButton.onClick.AddListener(Close);
  }

  private void OnSubmit()
  {
    if (nameField != null)
    {
      string nameInput = nameField.text;
      Debug.Log("Add device name: " + nameInput);
    }
    else Debug.Log("No device Name");
    if (nameShortField != null)
    {
      string nameShortInput = nameShortField.text;
      Debug.Log("Add device short name: " + nameShortInput);
    }
    else Debug.Log("No device ShortName");
    Close();
  }
}
