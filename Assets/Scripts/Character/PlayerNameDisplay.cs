using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerNameDisplay : MonoBehaviourPun
{
    [Header("Inspector")]
    [SerializeField] private GameObject nameLabelPrefab;
    [SerializeField] private GameObject nameLabel;

    void Start()
    {
        if (photonView.IsMine){
            nameLabel = Instantiate(nameLabelPrefab, transform.position, Quaternion.identity, transform);
            nameLabel.transform.SetParent(GameObject.Find("Canvas").transform, false);

            TextMeshProUGUI nameText = nameLabel.GetComponent<TextMeshProUGUI>();
            nameText.text = photonView.Owner.NickName;

            nameText.color = Color.green;
        }
        else
        {
            nameLabel = Instantiate(nameLabelPrefab, Vector3.zero, Quaternion.identity);
            nameLabel.transform.SetParent(GameObject.Find("Canvas").transform, false);

            TextMeshProUGUI nameText = nameLabel.GetComponent<TextMeshProUGUI>();
            nameText.text = photonView.Owner.NickName;

            nameText.color = Color.red;
        }
    }

    void Update()
    {
        if (nameLabel != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
            nameLabel.transform.position = screenPosition;
        }
    }
}
