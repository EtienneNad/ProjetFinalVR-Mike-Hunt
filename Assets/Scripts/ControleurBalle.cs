using UnityEngine;

/// <summary>
/// Balle de fusil
/// </summary>
public class ControleurBalle : MonoBehaviour
{
    /// <summary>
    /// Se d�truire apr�s une collision
    /// Pour d�boguage, la gestion de la collision avec une taupe est dans Taupe.cs
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            //Si on a touch� la cloche, la cogner
            if (collision.collider.gameObject.CompareTag("animal"))
            {
                collision.collider.GetComponent<Animal>().Blesser();
            }
            //La balle se d�truit apr�s une collision
            Destroy(this.gameObject);
        }

    }


}
