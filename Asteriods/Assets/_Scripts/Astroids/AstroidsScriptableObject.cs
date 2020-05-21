using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/AstriodsS0", fileName = "AstroidsS0.asset")]
[System.Serializable]
public class AstriodsScriptableObject : ScriptableObject
{
    [Header("Astroid Physics")]
    [SerializeField] public float maxThrust;
    [SerializeField] public float maxTorque;


    [Header("Astroid Sizes")]
    [SerializeField] public int astroidSize;
    [SerializeField] GameObject astroidMedium;
    [SerializeField] GameObject astroidSmall;
}