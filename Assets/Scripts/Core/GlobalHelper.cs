using UnityEngine;

public static class GlobalHelper
{
    public static string GenerateUniqueID(GameObject gameObject)
    {
        return $"{gameObject.scene.name}_{gameObject.transform.position.x}_{gameObject.transform.position.y}";
    }
}
