using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="VegetableDataSO")]
public class VegetableDataSO : ScriptableObject
{
    [Serializable]
    public struct VegetableData
    {
        public string name;
        public VegetableType type;
        public Sprite sprite;
    }
    public List<VegetableData> vegetableDataList;
}
