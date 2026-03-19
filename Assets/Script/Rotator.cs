using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float speedX = 0;
    [SerializeField] private float speedY = 0;
    [SerializeField] private float speedZ = 0;

    // Update is called once per frame
    void Update()
    {
        float rotConst = 360 * Time.deltaTime;

        transform.Rotate(new Vector3(rotConst * (speedX / 10), rotConst * (speedY / 10), rotConst * (speedZ / 10)));
    }
}
