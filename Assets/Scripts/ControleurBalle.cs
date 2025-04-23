using UnityEngine;

/// <summary>
/// Balle de fusil
/// </summary>
public class ControleurBalle : MonoBehaviour
{
    /// <summary>
    /// Se détruire après une collision
    /// Pour déboguage, la gestion de la collision avec une taupe est dans Taupe.cs
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            //Si on a touché la cloche, la cogner
            if (collision.collider.gameObject.CompareTag("animal"))
            {
                collision.collider.GetComponent<Animal>().Blesser();
            }
            //La balle se détruit après une collision
            Destroy(this.gameObject);
        }

    }


}
