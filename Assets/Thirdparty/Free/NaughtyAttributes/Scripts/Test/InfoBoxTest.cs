using UnityEngine;

namespace NaughtyAttributes.Test
{
    public class InfoBoxTest : MonoBehaviour
    {
        [InfoBox("This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? Test 2.This is an expannded test of the info box. How big can it get? \n Test 2.This is an expannded test of the info box. How big can it get? est 2.This is an expannded test of the info box. How big can it get?  Test 2.", EInfoBoxType.Normal)]
        public int normal;

        public InfoBoxNest1 nest1;
    }

    [System.Serializable]
    public class InfoBoxNest1
    {
        [InfoBox("Warning", EInfoBoxType.Warning)]
        public int warning;

        public InfoBoxNest2 nest2;
    }

    [System.Serializable]
    public class InfoBoxNest2
    {
        [InfoBox("Error", EInfoBoxType.Error)]
        public int error;
    }
}
