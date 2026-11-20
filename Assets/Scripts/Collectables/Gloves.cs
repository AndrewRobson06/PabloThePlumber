using Unity.VisualScripting;
using UnityEngine;

public class Gloves : MonoBehaviour
{
    private int gloves = 0;

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Glove")
        {
            gloves++;
            Debug.Log(gloves);
            Destroy(other.gameObject);
        }
    }

}
