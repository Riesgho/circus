using UnityEngine;

namespace Code.Views
{
    public class ChunkView : MonoBehaviour
    {
        public int Id { get; set; }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public bool HasActionable() => 
            GetComponentInChildren<Actionable>() != null;
    }
}
