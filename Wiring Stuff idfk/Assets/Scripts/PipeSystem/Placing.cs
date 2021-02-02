using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Placing : MonoBehaviour
{
    //Events
    #region
    public delegate void onSelectionChangeEventHandler();
    public event onSelectionChangeEventHandler onObjectSelectionChange;
    protected virtual void OnSelectionChange()
    {
        if (onObjectSelectionChange != null)
            onObjectSelectionChange();
    }
    #endregion

    //Prefabs
    #region
    public GameObject pipePrefab;
    public GameObject crossPipePrefab;
    public GameObject curvedPipePrefab;
    public GameObject startPointPrefab;
    public GameObject endPointPrefab;
    #endregion

    private GameObject current;
    private IPlaceable currentPlaceable;

    private KeyCode placeKey = KeyCode.P;
    private KeyCode rotateXKey = KeyCode.R;
    private KeyCode rotateZKey = KeyCode.T; 
    public float range = 4;

    public static Dictionary<int, PlaceableObject> placeableObjects;
    public static Dictionary<Vector3Int, IPlaceable> grid = new Dictionary<Vector3Int, IPlaceable>();
    public int selectedObject = 0;

    void Start()
    {
        current = null;
        InitializePlaceableObjects();
        OnSelectionChange();
    }

    private void InitializePlaceableObjects()
    {
        placeableObjects = new Dictionary<int, PlaceableObject>
        {
            {0, new PlaceableObject(typeof(StraightPipe), typeof(PipeComponent), pipePrefab) },
            {1, new PlaceableObject(typeof(CurvedPipe), typeof(PipeComponent), curvedPipePrefab) },
            {2, new PlaceableObject(typeof(CrossPipe), typeof(PipeComponent), crossPipePrefab) },
            {3, new PlaceableObject(typeof(StartPoint), typeof(ConsumerComponent), startPointPrefab) },
            {4, new PlaceableObject(typeof(EndPoint), typeof(ConsumerComponent), endPointPrefab) }
        };
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.mouseScrollDelta.y != 0)
        {
            selectedObject = 
                Mathf.Clamp((selectedObject + Mathf.RoundToInt(Input.mouseScrollDelta.y)) % 
                placeableObjects.Count, 0, placeableObjects.Count - 1); 
            OnSelectionChange();
        }

        if (Input.GetKeyDown(placeKey))
        {
            SpawnObject();
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }

        if (current != null)
        {
            current.transform.position = MousePosition();
        }

        Move();
    }

    void SpawnObject()
    {
        if (!placeableObjects.ContainsKey(selectedObject))
            return;

        PlaceableObject selected = placeableObjects[selectedObject];
        if (selected.component != null)
        {
            //selected object is a Pipe
            //Create pipe with correct prefab and pipe type
            current = Instantiate(selected.model);
            currentPlaceable = (IPlaceable) current.AddComponent(selected.component);
            currentPlaceable.Spawn(selected.type);
        }
    }

    void PlaceObject()
    {
        if (current == null)
            return;

        Vector3Int gridPos = toVector3Int(current.transform.position);
        foreach(Vector3Int occPos in currentPlaceable.occupiedSpaces)
        {
            if (grid.ContainsKey(gridPos + occPos))
            {
                //Not valid position
                return;
            }
        }

        foreach(Vector3Int occPos in currentPlaceable.occupiedSpaces)
        {
            grid[gridPos + occPos] = currentPlaceable;
        }

        currentPlaceable.Create();
        currentPlaceable = null;
        current = null;
    }

    void Move()
    {
        if (Input.GetKeyDown(rotateZKey))
        {
            if (current != null)
                current.transform.Rotate(new Vector3(0, 0, 90), Space.World);
        }
        if (Input.GetKeyDown(rotateXKey))
        {
            if (current != null)
                current.transform.Rotate(new Vector3(90, 0, 0), Space.World);
        }
    }

    private Vector3 MousePosition()
    {
        Vector3 aimPos = Camera.main.transform.position + Camera.main.transform.forward * range;
        aimPos.x = Mathf.RoundToInt(aimPos.x);
        aimPos.y = Mathf.RoundToInt(aimPos.y);
        aimPos.z = Mathf.RoundToInt(aimPos.z);
        return aimPos;
    }

    private Vector3Int toVector3Int(Vector3 v)
    {
        return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
    }

    public struct PlaceableObject
    {
        public Type type;
        public Type component;
        public GameObject model;

        public PlaceableObject(Type type, Type component, GameObject model)
        {
            this.type = type;
            this.component = component;
            this.model = model;
        }
    }
}
