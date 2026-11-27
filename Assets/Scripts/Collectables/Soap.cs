using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Soap : MonoBehaviour
{
    private int healAmount = 45;
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Soap")
        {
            /*hp += healAmount;
            // if (hp > maxHP){
            //hp = maxHP;
            }*/
            AudioManager.Instance.Play(AudioManager.SoundType.Soap);
            Destroy(other.gameObject);
        }
    }

}
