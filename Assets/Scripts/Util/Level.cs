using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelSO", order = 1)]
public class  Level: ScriptableObject
{

 public string levelkey;
 public int currentIndex;
 public List<string> levels;


}