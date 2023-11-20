using UnityEngine;
using UnityEngine.EventSystems;

public class InstrumentRemoval : MonoBehaviour
{
    private bool _isDragging = false;
    private Vector2 _startPosition;
    [SerializeField] Material removalMaterial; 
    [SerializeField] Material defaultMaterial;
    private GameObject _currentObject;
    [SerializeField] private RectTransform targetRectTransform;
    private InstrumentManager _manager;


    private void Awake()
    {
        _manager = FindAnyObjectByType<InstrumentManager>();
    }

    private void Start()
    {
        _startPosition = transform.position;
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            ManageDrag();
        }
    }

    private void ManageDrag()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                StartDragging(touch.position);
                break;

            case TouchPhase.Moved:
                DragObject(touch.position);
                break;

            case TouchPhase.Ended:
                StopDragging();
                break;
        }
    }

    private void StartDragging(Vector2 touchPosition)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(targetRectTransform, touchPosition) && _manager._isEnoughRemoval == true)
        {
            _isDragging = true;
        }

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    void DragObject(Vector2 touchPos)
    {
        if (_isDragging)
        {
            Vector2 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPos);
            RaycastHit2D hit = Physics2D.Raycast(touchWorldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                if ((_currentObject == null|| _currentObject != hit.collider.gameObject) && hit.collider.gameObject.GetComponent<SpriteRenderer>() !=null)
                {//change color of previous object to default, and give a marking material to a new object
                    if (_currentObject != null) _currentObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
                    GameObject tempObject = hit.collider.gameObject;
                    _currentObject = tempObject;
                    _currentObject.GetComponent<SpriteRenderer>().material = removalMaterial;
                }
            }
            else if (_currentObject != null)
            {
                _currentObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
                _currentObject = null;
            }
            transform.position = touchPos;
        }
    }

    private void StopDragging()
    {
        _isDragging = false;
        if (_currentObject != null) _currentObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
        transform.position = _startPosition;
        gameObject.layer = LayerMask.NameToLayer("Default");
        RemoveObject();
    }


    private void RemoveObject()
    {
        if (_currentObject != null && _manager._isEnoughRemoval == true)
        {
            Destroy(_currentObject);
            _manager.CountChangeRemoval(-1);
        }
    }
}