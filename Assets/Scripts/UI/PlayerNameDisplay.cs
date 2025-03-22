using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameDisplay : MonoBehaviourPun
{
    public GameObject nameLabelPrefab; // Prefab del nombre de usuario (TextMeshPro)
    public GameObject healthLabelPrefab; // Prefab de la vida del jugador (TextMeshPro)
    private GameObject nameLabel; // Instancia del nombre de usuario
    private GameObject healthLabel; // Instancia de la vida del jugador
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI healthText;
    private CharacterHealth CharacterHealth;

    void Start()
    {
        if (photonView.IsMine)
        {
            // Obtener la referencia a CharacterHealth primero
            CharacterHealth = GetComponent<CharacterHealth>();
            if (CharacterHealth == null)
            {
                Debug.LogError("No se encontró el componente CharacterHealth");
                return;
            }

            // Solo el dueño del PhotonView instancia el nombre de usuario
            nameLabel = Instantiate(nameLabelPrefab, Vector3.zero, Quaternion.identity);
            healthLabel = Instantiate(healthLabelPrefab, Vector3.zero, Quaternion.identity);

            // Asignar el nombre de usuario al Canvas
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            if (canvas != null)
            {
                nameLabel.transform.SetParent(canvas.transform, false);
                healthLabel.transform.SetParent(canvas.transform, false);
                Debug.Log("Canvas encontrado y nombre asignado al Canvas.");
            }
            else
            {
                Debug.LogError("No se encontró un Canvas en la escena.");
                return;
            }

            // Asignar un nombre fijo al texto
            nameText = nameLabel.GetComponent<TextMeshProUGUI>();
            healthText = healthLabel.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = "Jugador Local"; // Nombre fijo para pruebas
                nameText.color = Color.green; // Cambiar el color para el jugador local
                Debug.Log("Texto asignado: " + nameText.text);
            }
            else
            {
                Debug.LogError("El prefab no tiene un componente TextMeshProUGUI.");
            }

            if (healthText != null)
            {
                healthText.text = $"Vida: {CharacterHealth.currentHealth}"; // Ahora CharacterHealth ya está asignado
                healthText.color = Color.green;
                Debug.Log("Texto de vida asignado.");
                
                // Suscribirse al evento de cambio de vida
                CharacterHealth.onHealthChanged += UpdateHealthDisplay;
            }
            else
            {
                Debug.LogError("El prefab de vida no tiene un componente TextMeshProUGUI.");
            }
        }
        else
        {
            // Para otros jugadores, instanciar el nombre de usuario
            nameLabel = Instantiate(nameLabelPrefab, Vector3.zero, Quaternion.identity);
            healthLabel = Instantiate(healthLabelPrefab, Vector3.zero, Quaternion.identity);

            // Asignar el nombre de usuario al Canvas
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            if (canvas != null)
            {
                nameLabel.transform.SetParent(canvas.transform, false);
                healthLabel.transform.SetParent(canvas.transform, false);
                Debug.Log("Canvas encontrado y nombre asignado al Canvas.");
            }
            else
            {
                Debug.LogError("No se encontró un Canvas en la escena.");
                return;
            }

            // Asignar un nombre fijo al texto
            nameText = nameLabel.GetComponent<TextMeshProUGUI>();
            healthText = healthLabel.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = "Otro Jugador"; // Nombre fijo para pruebas
                Debug.Log("Texto asignado: " + nameText.text);
            }
            else
            {
                Debug.LogError("El prefab no tiene un componente TextMeshProUGUI.");
            }

            if (healthText != null)
            {
                healthText.text = $"Vida: {CharacterHealth.currentHealth}"; // Valor inicial de vida
                Debug.Log("Texto de vida asignado.");
            }
            else
            {
                Debug.LogError("El prefab de vida no tiene un componente TextMeshProUGUI.");
            }

            // Buscar el componente CharacterHealth
            CharacterHealth = GetComponent<CharacterHealth>();
            if (CharacterHealth != null)
            {
                CharacterHealth.onHealthChanged += UpdateHealthDisplay;
            }
        }
    }

    void Update()
    {
        if (nameLabel != null && healthLabel != null)
        {
            // Actualizar la posición del nombre de usuario en la pantalla
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f); // Ajusta la altura del texto
            nameLabel.transform.position = screenPosition;
            healthLabel.transform.position = screenPosition + new Vector3(0, -20, 0); // Ajusta la posición de la vida
            //Debug.Log("Posición del nombre y vida actualizada: " + screenPosition);
        }
    }

    private void UpdateHealthDisplay(int currentHealth)
    {
        if (healthText != null)
        {
            // Actualizar el texto con la vida actual
            healthText.text = $"Vida: {currentHealth}";
            Debug.Log($"Vida actualizada: {currentHealth}");

            // Calcular el porcentaje de vida
            float healthPercentage = (float)currentHealth / 100f; // Asumimos que 100 es el valor máximo de vida

            // Actualizar el color según el umbral
            float targetPoint = 1 - healthPercentage;
            healthText.color = Color.Lerp(Color.green, Color.red, targetPoint);
            
        }
    }

    void OnDestroy()
    {
        // Destruir el nombre de usuario cuando el jugador se destruye
        if (nameLabel != null)
        {
            Destroy(nameLabel);
            Debug.Log("Nombre de usuario destruido.");
        }

        if (healthLabel != null)
        {
            Destroy(healthLabel);
            Debug.Log("Texto de vida destruido.");
        }

        // Desuscribirse del evento cuando se destruye el objeto
        if (CharacterHealth != null)
        {
            CharacterHealth.onHealthChanged -= UpdateHealthDisplay;
        }
    }
}