using UnityEngine;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public string _name;
    [TextArea(3, 10)]
    public string[] sentences;
}
