using EasyTransition;

using UnityEngine;

namespace CrawfisSoftware
{
    internal class AutoTransition : MonoBehaviour
    {
        public TransitionSettings transition;
        public float _transitionDelay = 2f;
        public string _scene = "TransitionDemo";

        private void Start()
        {
            TransitionManager.Instance().Transition(_scene, transition, _transitionDelay);
        }
    }
}