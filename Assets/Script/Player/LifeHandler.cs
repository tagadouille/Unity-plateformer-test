using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeHandler : MonoBehaviour
{


    void Update()
    {
        if(GetComponent<Transform>().position.y < -5f)
        {
            Die();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        ResetScene();
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
