using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    public bool hasPlunger = false;

    public TextMeshProUGUI gloveText;

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Plunger")
        {
            hasPlunger = true;
            AudioManager.Instance.Play(AudioManager.SoundType.Plunger);
            //gloveText.text = "Gloves: " + gloves.ToString();
            Debug.Log(hasPlunger);
            Destroy(other.gameObject);
        }
    }

}
