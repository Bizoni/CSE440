using UnityEngine;

public class catUpdater : MonoBehaviour
{

    private catController catcontroller;

    void Start()
    {
        catcontroller = transform.parent.GetComponent<catController>();
    }

    void UpdateTargetPosition()
    {
        catcontroller.UpdateTargetPosition();
    }
    void OnBecameInvisible()
    {
        catcontroller.OnBecameInvisible();
    }
    void GrantCatTheSweetReleaseOfDeath()
    {
        Destroy(transform.parent.gameObject);
    }
}