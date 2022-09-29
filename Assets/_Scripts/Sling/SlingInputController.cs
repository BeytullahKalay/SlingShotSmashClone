using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class SlingInputController : MonoBehaviour
{
    [SerializeField] private GameObject man;

    private SlingDots _slingDots;

    private GameManager _gm;
    
    private Vector3 _offset;
    private Vector3 _startPosWorld;
    private Vector3 _direction;
    private Vector3 _slingStartPos;
    private Vector3 _manOffset;

    private float _ZCoordinate;
    
    private BoxCollider _StrechCollider;


    private void OnEnable()
    {
        EventManager.ApplyNextManToSling += ApplyManGameObjectToMan;
        EventManager.ApplyNextManToSling += OpenCloseSlingStretchCollider;
        EventManager.OnGameStart += OpenStretch;
        EventManager.OnGameFinished += CloseSlingStretchCollider;
    }

    private void OnDisable()
    {
        EventManager.ApplyNextManToSling -= ApplyManGameObjectToMan;
        EventManager.ApplyNextManToSling -= OpenCloseSlingStretchCollider;
        EventManager.OnGameStart -= OpenStretch;
        EventManager.OnGameFinished -= CloseSlingStretchCollider;
    }


    private void Start()
    {
        _gm = GameManager.Instance;
        _slingDots = GetComponent<SlingDots>();
        _StrechCollider = GetComponent<BoxCollider>();
        AssingOffsetsAndPositions();
    }

    private void AssingOffsetsAndPositions()
    {
        _startPosWorld = transform.position;
        _slingStartPos = transform.position;
        _manOffset = man.transform.position - transform.position;
    }

    private void OnMouseDown()
    {
        _slingDots.OpenDots();
        _ZCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        _offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        _direction = _startPosWorld - transform.position;
        _direction.y -= _gm.DecreaseDirectionY;

        
        man.GetComponent<ManSlingAction>().ManLaunch(_gm.Force, _direction);

        man = null;

        transform.DOMove(_slingStartPos, _gm.SlingResetSpeed).SetSpeedBased();
        
        _slingDots.CloseDots();
        CloseSlingStretchCollider();
    }

    private void OnMouseDrag()
    {
        _slingDots.DrawDots();
        transform.position = GetMouseWorldPos() + _offset;
        man.transform.position = transform.position + _manOffset;
        _direction = _startPosWorld - transform.position;
        _direction.y -= _gm.DecreaseDirectionY;
    }


    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _ZCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void ApplyManGameObjectToMan(Transform comingTransform)
    {
        man = comingTransform.gameObject;
    }

    private void CloseSlingStretchCollider()
    {
        _StrechCollider.enabled = false;
    }

    private void OpenCloseSlingStretchCollider(Transform comingTransform)
    {
        _StrechCollider.enabled = true;
    }

    private void OpenStretch()
    {
        _StrechCollider.enabled = true;
    }

    public Vector3 Direction => _direction;
}