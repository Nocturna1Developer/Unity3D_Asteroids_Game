using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Astroid Sizes")]
    [SerializeField] public int astroidSize;
    [SerializeField] GameObject astroidLarge;
    [SerializeField] GameObject astroidMedium;
    [SerializeField] GameObject astroidSmall;

    private void Awake() 
    {
        Instantiate(astroidMedium, transform.position, transform.rotation);
        Instantiate(astroidMedium, transform.position, transform.rotation);
    }
}