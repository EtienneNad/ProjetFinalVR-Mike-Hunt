using Oculus.Interaction;
using UnityEngine;

/// <summary>
/// Porte Menu
/// </summary>
public class ControleurMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject porte;

    /// <summary>
    /// Se détruire après une collision avec une Balle
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if(collision.gameObject.tag == "Balle")
            {
                Destroy(porte);
                Destroy(this.gameObject);
            }
            
        }

    }


}
