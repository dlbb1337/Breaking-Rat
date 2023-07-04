using TMPro;
using UnityEngine;

namespace BreakingRat.UI
{
    public class GameCurtain : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        public TMP_Text Text => _text;
    }
}
