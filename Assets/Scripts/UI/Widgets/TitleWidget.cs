using TMPro;
using UnityEngine;

namespace Custom.View.UI.Widgets
{
    //уже в последствии подумал что в принципе можно было не делать этот виджет, а оставить ТМП, которые тоже виджет по сути
    //но решил оставить как есть
    public class TitleWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        public void SetTitle(string title)
        {
            _title.text = title;
        }
    }
}
