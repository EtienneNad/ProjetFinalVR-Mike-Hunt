using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class DonneeJoueur : MonoBehaviour
{
    public static DonneeJoueur Instance { get; private set; }

    [SerializeField, Tooltip("Nombre d'animaux à tuer")]
    private int nombreAnimauxRestant = 10; // Initialiser avec une valeur par défaut

    [SerializeField, Tooltip("Canvas du menu pause")]
    private CanvasGroup menuPause;

    [SerializeField, Tooltip("Canvas du menu fin")]
    private CanvasGroup menuFin;

    [SerializeField, Tooltip("Événement appelé lors de la modification du nombre d'animaux restants.  Utilise un tableau d'entiers.")]
    private UnityEvent<int[]> valeurNombreAnimauxRestant;

    private bool menuPauseOuvert = false;
    private bool menuFinOuvert = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Permet de conserver l'instance entre les scènes, si nécessaire.
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return; // Important d'arrêter l'exécution ici pour éviter d'autres erreurs.
        }
    }

    private void Start()
    {
        // Initialisation de l'affichage.  Appeler l'événement Start() est plus approprié.
        valeurNombreAnimauxRestant?.Invoke(ModificationNombreAnimauxRestant()); // Vérification null
        //S'assurer que les menus sont initialisés à l'état caché
        if (menuPause != null)
        {
            menuPause.alpha = 0;
            menuPause.interactable = false;
            menuPause.blocksRaycasts = false;
        }
        if (menuFin != null)
        {
            menuFin.alpha = 0;
            menuFin.interactable = false;
            menuFin.blocksRaycasts = false;
        }
    }

    private void Update()
    {
        // Vérifier si les menus sont null avant d'essayer de les utiliser.
        if (menuFinOuvert|| menuPauseOuvert)
        {
            // Si le menu de fin est ouvert
            if (OVRInput.Get(OVRInput.RawButton.A))
            {
                RecommencerJeu();
            }
            if (OVRInput.Get(OVRInput.RawButton.B))
            {
                QuitterJeu();
            }
        }
       

        if (nombreAnimauxRestant <= 0)
        {
            AfficherMenuFin();
        }

        // Gérer l'ouverture du menu de pause
        if (OVRInput.Get(OVRInput.RawButton.Start))
        {
            if (!menuPauseOuvert) //vérification pour ne pas rouvrir le menu pause s'il est déjà ouvert
            {
                AfficherMenuPause();
            }

        }
        // Gérer la fermeture du menu de pause
        if (OVRInput.Get(OVRInput.RawButton.X) && menuPauseOuvert)
        {
            FermerMenuPause();
        }


        // Mettre à jour la valeur affichée du nombre d'animaux restants
        valeurNombreAnimauxRestant?.Invoke(ModificationNombreAnimauxRestant()); // Vérification null
    }

    /// <summary>
    /// Méthode permettant d'afficher le menu pause.
    /// </summary>
    private void AfficherMenuPause()
    {
        if (menuPause != null) //vérification que menuPause n'est pas null
        {
            menuPause.alpha = 1;
            menuPause.interactable = true;
            menuPause.blocksRaycasts = true;
            Time.timeScale = 0; // Met le temps sur pause
            menuPauseOuvert = true;
        }
    }

    /// <summary>
    /// Méthode permettant de fermer le menu pause.
    /// </summary>
    private void FermerMenuPause()
    {
        if (menuPause != null) //vérification que menuPause n'est pas null
        {
            menuPause.alpha = 0;
            menuPause.interactable = false;
            menuPause.blocksRaycasts = false;
            Time.timeScale = 1; // Réactive le temps
            menuPauseOuvert = false;
        }
    }

    /// <summary>
    ///  Méthode permettant d'affiche le menu de fin.
    /// </summary>
    private void AfficherMenuFin()
    {
        if (menuFin != null)
        {
            menuFin.alpha = 1;
            menuFin.interactable = true;
            menuFin.blocksRaycasts = true;
            Time.timeScale = 0; // Met le temps sur pause
            menuFinOuvert = true;
        }
    }

    private void RecommencerJeu()
    {
        Time.timeScale = 1; // Important de remettre le temps à 1 avant de charger la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    ///  Méthode permettant de quitter le jeu.
    /// </summary>
    private void QuitterJeu()
    {
        Application.Quit();
    }

    /// <summary>
    /// Méthode permettant de gérer le nombre d'animaux restants.
    /// </summary>
    public void EnleverAnimaux()
    {
        if (nombreAnimauxRestant > 0)
        {
            nombreAnimauxRestant--; // Simplification de la décrémentation.
            valeurNombreAnimauxRestant?.Invoke(ModificationNombreAnimauxRestant()); //vérification null
            if (nombreAnimauxRestant <= 0)
            {
                AfficherMenuFin();
            }
        }
    }

    /// <summary>
    ///  Méthode permettant de modifier le nombre d'animaux restants.
    /// </summary>
    /// <returns>Le nombre d'animaux restant.</returns>
    private int[] ModificationNombreAnimauxRestant()
    {
        return new int[] { nombreAnimauxRestant };
    }
}
