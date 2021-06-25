using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
    public ButtonsData buttonsData;
    GameObject selection;
    
    void Start () {
        // initialize services
        UiService.Initialize(this, buttonsData, gameObject.GetComponent<EditorList>());
        MapService.Initialize();
    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            UiService.Check();
        }
    }

    #region public methods
    public void OnClickButton (string key) {
        switch (key) {
            case "save": {
                Level levelData = MapService.GetMapData();
                SaveService.SaveLevel(levelData);
                break;
            }
            case "load": {
                Level levelData = SaveService.LoadLevel();
                MapService.InitializeMapData(levelData);
                Debug.Log(JsonUtility.ToJson(levelData, true));
                break;
            }
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

        Entity entity = MapService.InsertEntity(selection);
        if (entity != null) {
            HistoryService.AddEntity(entity);
        }
    }

    #endregion
}
