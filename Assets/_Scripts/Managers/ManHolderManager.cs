using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class ManHolderManager : MonoSingleton<ManHolderManager>
{
    public List<GameObject> Men;

    public Transform[] MovePath;

    private Vector3[] _pathval = new Vector3[3];

    private bool _isMoveCalled = false;
    private void OnEnable()
    {
        EventManager.MoveNextManToSling += MoveNextManToSling;
        EventManager.ApplyNextManToSling += ApplyManPositionToSlingPosition;
    }

    private void OnDisable()
    {
        EventManager.MoveNextManToSling -= MoveNextManToSling;
        EventManager.ApplyNextManToSling -= ApplyManPositionToSlingPosition;
    }

    private void Start()
    {
        GetMovePathVectors();
    }

    private void GetMovePathVectors()
    {
        for (int i = 0; i < MovePath.Length; i++)
        {
            _pathval[i] = MovePath[i].position;
        }
    }

    private void MoveNextManToSling()
    {
        if (Men.Count <= 0 || _isMoveCalled) return;

        _isMoveCalled = true;
        
        var man = Men.First();

        StartPath(man.transform);
    }

    private void ApplyManPositionToSlingPosition(Transform comingManTransform)
    {
        comingManTransform.position = GameManager.Instance.ManSlingPosStopPos.position;
    }

    private void StartPath(Transform manTransform)
    {
        manTransform.DOPath(_pathval, 3, PathType.CatmullRom, PathMode.Full3D, 10, Color.red)
            .OnComplete(() =>
            {
                manTransform.position = GameManager.Instance.ManSlingPosStopPos.position;
                EventManager.ApplyNextManToSling(manTransform);
            });
    }
}
