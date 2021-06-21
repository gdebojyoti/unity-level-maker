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
    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            string name = grid.GetClickedObject();

            // if clicked on editor, set cursor as corresponding object
            if (name != "") {
                GameObject go = editorList.FetchEditorObject(name);

                if (go != null) {
                    grid.updateEditorSelection(go);
                }
            }
            // if clicked on grid, insert object at corresponding position
            if (name == "") {
                grid.InsertEntity();
            }
        }
    }
}
