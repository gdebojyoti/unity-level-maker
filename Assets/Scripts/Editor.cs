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
                SaveService.SaveLevel();
                break;
            case "load":
                Level levelData = SaveService.LoadLevel();
                MapService.InitializeMapData(levelData);
                Debug.Log(JsonUtility.ToJson(levelData, true));
                break;
            case "undo":
                break;
            case "redo":
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
