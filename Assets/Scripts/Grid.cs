using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    GameObject selection;
    GameObject entitiesFolder;
    Vector3 worldPosition;
    int xPos;
    int yPos;

    [SerializeField] int size = 1;
    
    void Start () {
        entitiesFolder = new GameObject("Entities");
    }

    #region public methods

    public void updateEditorSelection (GameObject go) {
        selection = go;
    }

    public string GetClickedObject () {
        // if mouse pointer is hovering over an object with collider, return true; else return false
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null) {
            return hit.collider.name;
        }
        return "";
    }

    // TODO: check for existing entities at this location; if found, replace them
    public void InsertEntity () {
        // exit if nothing is selected
        if (selection == null) {
            return;
        }

        // instantiate entity
        GameObject entity = Instantiate(selection, _GetNearestPositionOnGrid(), Quaternion.identity);

        // set parent in Hierarchy View
        entity.transform.SetParent(entitiesFolder.transform, false);

        // remove box collider
        Destroy(entity.GetComponent<BoxCollider2D>());
    }

    #endregion

    #region private methods

    private Vector3 _GetNearestPositionOnGrid () {
        // determine mouse position on screen
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // get nearest point to mouse pointer
        xPos = size * Mathf.RoundToInt(worldPosition.x / size);
        yPos = size * Mathf.RoundToInt(worldPosition.y / size);

        // return computed position
        return new Vector3(xPos, yPos, 0);
    }

    #endregion
}
