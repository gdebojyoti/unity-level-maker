// dynamically generates the UI of the editor

using System;
using UnityEngine;
using UnityEngine.UI;

public static class UiService {

  public static int q = 9;

  public static ButtonsData buttonsData;

  #region public methods

    // generate buttons
    public static void InitializeLayout (Editor editor, ButtonsData data) {
      buttonsData = data;
      _InitializeListeners(editor);
    }

  #endregion

  #region private methods

    static void _InitializeListeners (Editor editor) {
      for (int i = 0; i < buttonsData.buttons.Length; i++) {
        ButtonData data = buttonsData.buttons[i];
        Debug.Log(data.id + " " + data.key);

        Button button = GameObject.Find(data.id).GetComponent<Button>();
        button.onClick.AddListener(() => {
          editor.OnClickButton(data.key);
        });
      }
    }

  #endregion
}