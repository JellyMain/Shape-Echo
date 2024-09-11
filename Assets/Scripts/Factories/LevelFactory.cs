using Constants;
using UnityEngine;


namespace Factories
{
    public class LevelFactory
    {
        public void CreatePlayer(Vector2 position)
        {
            GameObject prefab = Resources.Load<GameObject>(RuntimeConstants.PrefabPaths.PLAYER);
            Object.Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
