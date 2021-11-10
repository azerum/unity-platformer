using UnityEngine;

public class TriggerSaw : MonoBehaviour
{
    public SawMovement sawMovement;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sawMovement.StartMovement();
        }
    }
}
