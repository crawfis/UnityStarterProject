using UnityEngine;

namespace CrawfisSoftware.Unity3D.Utility
{
    public class Blackboard : MonoBehaviour
    {
        [field: SerializeField] public static RandomProvider MasterRandom { get; set; }
    }
}