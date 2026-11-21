using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Gloves : MonoBehaviour
{
    private int gloves = 0;

    public TextMeshProUGUI gloveText;

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Glove")
        {
            gloves++;
            gloveText.text = "Gloves: " + gloves.ToString();
            Debug.Log(gloves);
            Destroy(other.gameObject);
        }
    }

}
