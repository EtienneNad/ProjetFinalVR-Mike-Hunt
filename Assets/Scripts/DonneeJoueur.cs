using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class DonneeJoueur : MonoBehaviour
{
    public static DonneeJoueur Instance { get; private set; }

    [SerializeField, Tooltip("Nombre d'animaux � tuer")]
    private int nombreAnimauxRestant = 10; // Initialiser avec une valeur par d�faut

� � [SerializeField, Tooltip("Canvas du menu pause")]
    private CanvasGroup menuPause;

    [SerializeField, Tooltip("Canvas du menu fin")]
    private CanvasGroup menuFin;

    [SerializeField, Tooltip("�v�nement appel� lors de la modification du nombre d'animaux restants.  Utilise un tableau d'entiers.")]
    private UnityEvent<int[]> valeurNombreAnimauxRestant;

    private bool menuPauseOuvert = false;
    private bool menuFinOuvert = false;

� � private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Permet de conserver l'instance entre les sc�nes, si n�cessaire.
� � � � }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return; // Important d'arr�ter l'ex�cution ici pour �viter d'autres erreurs.
� � � � }
    }

    private void Start()
    {
� � � � // Initialisation de l'affichage.  Appeler l'�v�nement Start() est plus appropri�.
� � � � valeurNombreAnimauxRestant?.Invoke(ModificationNombreAnimauxRestant()); // V�rification null
� � � � //S'assurer que les menus sont initialis�s � l'�tat cach�
� � � � if (menuPause != null)
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
� � � � // V�rifier si les menus sont null avant d'essayer de les utiliser.
� � � � if (menuFinOuvert|| menuPauseOuvert)
        {
� � � � � � // Si le menu de fin est ouvert
� � � � � � if (OVRInput.Get(OVRInput.RawButton.A))
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
� � � � }

� � � � // G�rer l'ouverture du menu de pause
� � � � if (OVRInput.Get(OVRInput.RawButton.Start))
        {
            if (!menuPauseOuvert) //v�rification pour ne pas rouvrir le menu pause s'il est d�j� ouvert
� � � � � � {
                AfficherMenuPause();
            }

        }
        // G�rer la fermeture du menu de pause
        if (OVRInput.Get(OVRInput.RawButton.X) && menuPauseOuvert)
        {
            FermerMenuPause();
        }


� � � � // Mettre � jour la valeur affich�e du nombre d'animaux restants
� � � � valeurNombreAnimauxRestant?.Invoke(ModificationNombreAnimauxRestant()); // V�rification null
� � }

    /// <summary>
    /// M�thode permettant d'afficher le menu pause.
    /// </summary>
    private void AfficherMenuPause()
    {
        if (menuPause != null) //v�rification que menuPause n'est pas null
� � � � {
            menuPause.alpha = 1;
            menuPause.interactable = true;
            menuPause.blocksRaycasts = true;
            Time.timeScale = 0; // Met le temps sur pause
� � � � � � menuPauseOuvert = true;
� � � � }
    }

    /// <summary>
    /// M�thode permettant de fermer le menu pause.
    /// </summary>
    private void FermerMenuPause()
    {
        if (menuPause != null) //v�rification que menuPause n'est pas null
� � � � {
            menuPause.alpha = 0;
            menuPause.interactable = false;
            menuPause.blocksRaycasts = false;
            Time.timeScale = 1; // R�active le temps
� � � � � � menuPauseOuvert = false;
        }
    }

    /// <summary>
    ///  M�thode permettant d'affiche le menu de fin.
    /// </summary>
    private void AfficherMenuFin()
    {
        if (menuFin != null)
        {
            menuFin.alpha = 1;
            menuFin.interactable = true;
            menuFin.blocksRaycasts = true;
            Time.timeScale = 0; // Met le temps sur pause
� � � � � � menuFinOuvert = true;
        }
    }

    private void RecommencerJeu()
    {
        Time.timeScale = 1; // Important de remettre le temps � 1 avant de charger la sc�ne
� � � � SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    ///  M�thode permettant de quitter le jeu.
    /// </summary>
    private void QuitterJeu()
    {
        Application.Quit();
    }

    /// <summary>
    /// M�thode permettant de g�rer le nombre d'animaux restants.
    /// </summary>
    public void EnleverAnimaux()
    {
        if (nombreAnimauxRestant > 0)
        {
            nombreAnimauxRestant--; // Simplification de la d�cr�mentation.
� � � � � � valeurNombreAnimauxRestant?.Invoke(ModificationNombreAnimauxRestant()); //v�rification null
� � � � � � if (nombreAnimauxRestant <= 0)
            {
                AfficherMenuFin();
            }
        }
    }

    /// <summary>
    ///  M�thode permettant de modifier le nombre d'animaux restants.
    /// </summary>
    /// <returns>Le nombre d'animaux restant.</returns>
    private int[] ModificationNombreAnimauxRestant()
    {
        return new int[] { nombreAnimauxRestant };
    }
}
