using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Create Dialogue")]
public class Dialogue : ScriptableObject
{
    public string characterName, humor;
    public string[] sentences;
}
