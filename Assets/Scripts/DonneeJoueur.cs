using UnityEngine;
using UnityEngine.Events;

public class DonneeJoueur : MonoBehaviour
{
    public static DonneeJoueur Instance { get; private set; }

    [SerializeField]
    private int nombreAnimauxRestant;

    [SerializeField]
    private CanvasGroup menuPause;

    [SerializeField]
    private CanvasGroup menuFin; 

    [SerializeField]
    private UnityEvent<int[]> valeurNombreAnimauxRestant;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        valeurNombreAnimauxRestant.Invoke(ModificationNombreAnimauxRestant());

    }
    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.Start))
        {
            AfficherMenuPause();
        }
        if (nombreAnimauxRestant <= 0)
        {
            AfficherMenuFin();
        }
        valeurNombreAnimauxRestant.Invoke(ModificationNombreAnimauxRestant());

    }

    private void AfficherMenuPause()
    {
        if (menuPause != null)
        {
            menuPause.alpha = 1;
            menuPause.interactable = true;
            menuPause.blocksRaycasts = true;
            Time.timeScale = 0;

        }
    }
    private void AfficherMenuFin()
    {
        if (menuPause != null)
        {
            menuFin.alpha = 1;
            menuFin.interactable = true;
            menuFin.blocksRaycasts = true;
            Time.timeScale = 0;
        }
    }

    public void EnleverAnimaux()
    {
        nombreAnimauxRestant -= 1;
        valeurNombreAnimauxRestant.Invoke(ModificationNombreAnimauxRestant());
    }

    private int[] ModificationNombreAnimauxRestant()
    {
        return new int[] {nombreAnimauxRestant};
    }
}
