using UnityEngine;

public class SlingDots : MonoBehaviour
{
    public GameObject point;

    public int numberOfPoints;

    public float spaceBetweenPoints = .2f;

    private float _force;

    private GameObject[] _points;
    private GameObject dotsHolder;

    private SlingInputController _slingInputController;


    private void Start()
    {
        dotsHolder = new GameObject();

        TakeCalculationValues();

        CreatePoints();

        CloseDots();
    }

    private void TakeCalculationValues()
    {
        _force = GameManager.Instance.Force;
        _slingInputController = GetComponent<SlingInputController>();
    }

    private void CreatePoints()
    {
        _points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            _points[i] = Instantiate(point, transform.position, Quaternion.identity);
            _points[i].transform.SetParent(dotsHolder.transform);
        }
    }

    public void CloseDots()
    {
        dotsHolder.SetActive(false);
    }

    public void OpenDots()
    {
        dotsHolder.SetActive(true);
    }

    public void DrawDots()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            _points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }


    private Vector3 PointPosition(float t)
    {
        Vector3 position = transform.position + (_force * _slingInputController.Direction * t) + .5f * Physics.gravity * (t * t);
        return position;
    }
}