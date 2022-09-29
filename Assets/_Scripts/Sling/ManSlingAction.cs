using UnityEngine;

public class ManSlingAction : MonoBehaviour
{
    public void ManLaunch(float force, Vector3 direction)
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);

        var menList = ManHolderManager.Instance.Men;

        if (!menList.Contains(gameObject))
            Debug.LogError(gameObject.name + " gameobject not int men list!");
        else
        {
            menList.Remove(gameObject);
            EventManager.UpdateShotLeft(menList.Count);
        }

        Destroy(this);
    }
}
