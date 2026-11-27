using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Duck : MonoBehaviour
{
    public bool hasDuck = false;
    public Image duck;
    public Sprite collected;

    public Sprite notCollected;


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

    public void ImageChange()
    {

    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (hasDuck == false)
        {
            duck.sprite = notCollected;
        }

        else
        {
            duck.sprite = collected;
        }
    }

}
