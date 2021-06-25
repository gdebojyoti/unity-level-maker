using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
    Grid grid;
    public ButtonsData buttonsData;
    GameObject selection;
    
    void Start () {
        grid = gameObject.GetComponent<Grid>();

        // initialize UI
        UiService.Initialize(this, buttonsData, gameObject.GetComponent<EditorList>());
    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            UiService.Check();
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

    public void UpdateSelection (GameObject go) {
        selection = go;
    }

    public void Draw () {
        // exit if nothing is selected
        if (selection == null) {
            return;
        }

        Entity entity = grid.InsertEntity(selection);
        HistoryService.AddEntity(entity);
    }

    #endregion
}
