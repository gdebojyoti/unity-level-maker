// dynamically generates the UI of the editor

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class UiService {

  public static int q = 9;

  public static Editor editor;
  public static ButtonsData buttonsData;
  public static EditorList editorList; // TODO: use ScriptableObject

  #region public methods

    // generate buttons
    public static void Initialize (Editor ed, ButtonsData bd, EditorList el) {
      // initialize static variables
      editor = ed;
      buttonsData = bd;
      editorList = el;
      
      _InitializeListeners();
    }

    // check for mouse clicks on block selector and grid
    public static void Check () {
      // if pointer is over a button, ignore click
      if (EventSystem.current.IsPointerOverGameObject()) {
        return;
      }

      // check for clicks on blocks
      RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      if (hit.collider != null) {
          string colliderName = hit.collider.name;
          GameObject go = editorList.FetchEditorObject(colliderName);
          editor.UpdateSelection(go);
          return;
      }

      // seems like user has clicked on grid, so call Draw method
      editor.Draw();
    }

  #endregion

  #region private methods

    static void _InitializeListeners () {
      for (int i = 0; i < buttonsData.buttons.Length; i++) {
        ButtonData data = buttonsData.buttons[i];

        Button button = GameObject.Find(data.id).GetComponent<Button>();
        button.onClick.AddListener(() => {
          editor.OnClickButton(data.key);
        });
      }
    }

  #endregion
}