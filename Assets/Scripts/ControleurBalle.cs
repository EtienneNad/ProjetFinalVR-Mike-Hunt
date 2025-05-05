using UnityEngine;

/// <summary>
/// Balle de fusil
/// </summary>
public class ControleurBalle : MonoBehaviour
{
    /// <summary>
    /// Se d�truire apr�s une collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            Destroy(this.gameObject);
        }

    }


}
