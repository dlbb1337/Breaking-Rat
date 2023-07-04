using UnityEngine;
using UnityEngine.Events;

namespace BreakingRat.GameLogic
{
    [RequireComponent(typeof(Collider2D))]
    public class Trigger : MonoBehaviour
    {
        public UnityEvent<Collider2D> Enter { get; private set; } = new();
        public UnityEvent<Collider2D> Exit { get; private set; } = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enter.Invoke(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Exit.Invoke(collision);
        }
    }
}
