using UnityEngine;

namespace Controllers
{
    public class LevelLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(GameObject gameObject, Transform levelHolder)
        {
            Instantiate(gameObject, levelHolder);
        }
    }
}