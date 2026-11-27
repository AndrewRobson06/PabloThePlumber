using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Plunger : MonoBehaviour
{
    public bool hasPlunger = false;

    public Image plunger;
    public Sprite collected;

    public Sprite notCollected;

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Plunger")
        {
            hasPlunger = true;
            AudioManager.Instance.Play(AudioManager.SoundType.Plunger);
            Debug.Log(hasPlunger);
            Destroy(other.gameObject);

        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (hasPlunger == false)
        {
            plunger.sprite = notCollected;
        }

        else
        {
            plunger.sprite = collected;
        }
    }

    public void ImageChange()
    {

    }



}
