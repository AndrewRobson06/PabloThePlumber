using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public bool hasDuck = false;

    public TextMeshProUGUI gloveText;

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Duck")
        {
            hasDuck = true;
            AudioManager.Instance.Play(AudioManager.SoundType.Duck);
            //gloveText.text = "Gloves: " + gloves.ToString();
            Debug.Log(hasDuck);
            Destroy(other.gameObject);
        }
    }

}
