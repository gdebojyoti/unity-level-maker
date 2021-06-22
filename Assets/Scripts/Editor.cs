using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
    Grid grid;
    EditorList editorList;
    
    void Start () {
        grid = gameObject.GetComponent<Grid>();

        editorList = gameObject.GetComponent<EditorList>();

        // initialize buttons
        _InitializeButtons();
    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            string name = grid.GetClickedObject();

            // if clicked on editor, set cursor as corresponding object
            if (name != "") {
                GameObject go = editorList.FetchEditorObject(name);

                if (go != null) {
                    grid.UpdateEditorSelection(go);
                }
            }
            // if clicked on grid, insert object at corresponding position
            if (name == "") {
                Entity entity = grid.InsertEntity();
                HistoryService.AddEntity(entity);
            }
        }
    }

    #region public methods

    public void OnClickButton (string key) {
        switch (key) {
            case "save":
                HistoryService.Save();
                break;
            case "load":
                HistoryService.Load();
                break;
            case "undo":
                HistoryService.Undo();
                break;
            case "redo":
                HistoryService.Redo();
                break;
        }
    }

    #endregion

    #region private methods

    private void _InitializeButtons () {
        // Debug.Log("random public method inside editor script");
    }

    #endregion
}
