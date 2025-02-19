using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerNameDisplay : MonoBehaviourPun
{
    [SerializeField] private GameObject nameLabelPrefab;
    private GameObject nameLabel;

    void Start()
    {
        if(photonView.IsMine)
        {
            // Solo el dueño del PhotonView instancia el nombre del usuario
            nameLabel = Instantiate(nameLabelPrefab, Vector3.zero, Quaternion.identity);
            nameLabel.transform.SetParent(GameObject.Find("Canvas").transform, false);

            // Asignar el nombre de usuario al texto
            TextMeshProUGUI nameText = nameLabel.GetComponent<TextMeshProUGUI>();
            photonView.Owner.NickName = $"Player {PhotonNetwork.PlayerList.Length}";
            nameText.text = photonView.Owner.NickName;

            //Asignar un color diferente para el jugador local
            nameText.color = Color.green; //Puedes cambiar el color segun tus preferencias
        }
        else
        {
            // Para otros jugadores, instanciar el nombre del usuario
            nameLabel = Instantiate(nameLabelPrefab, Vector3.zero, Quaternion.identity);
            nameLabel.transform.SetParent(GameObject.Find("Canvas").transform, false);


            TextMeshProUGUI nameText = nameLabel.GetComponent<TextMeshProUGUI>();
            nameText.text = photonView.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(nameLabel != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);
            nameLabel.transform.position = screenPosition;
        }
    }

    void OnDestroy()
    {
        // Destruir el nombre de usuario cuando el jugador se destruye
        if (nameLabel != null)
        {
            Destroy(nameLabel);
        }
    }
}
