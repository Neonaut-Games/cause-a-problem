using UnityEngine;
using Toggle = UnityEngine.UI.Toggle;

namespace Main_Menu
{
    public class CategorySelect : MonoBehaviour
    {
        public Toggle toggleBasic;
        public Toggle toggleAdvanced;

        public GameObject panelBasic;
        public GameObject panelAdvanced;

        public void OnBasic()
        {
            if (!toggleBasic.isOn) return;
            Toggle(true);
        }
        
        public void OnAdvanced()
        {
            if (!toggleAdvanced.isOn) return;
            Toggle(false);
        }

        private void Toggle(bool state)
        {
            panelBasic.SetActive(state);
            panelAdvanced.SetActive(!state);
            toggleBasic.SetIsOnWithoutNotify(state);
            toggleAdvanced.SetIsOnWithoutNotify(!state);
        }
    }
}
