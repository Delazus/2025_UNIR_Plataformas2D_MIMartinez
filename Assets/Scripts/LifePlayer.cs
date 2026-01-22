using UnityEngine;

public class LifePlayer : MonoBehaviour
{
    public int lifes;
    [SerializeField] private GameObject[] corazones; 
    
    void Start()
    {
        
    }

    void Update()
    {
        ShowHearth();
    }

    void ShowHearth()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            corazones[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < lifes; i++)
        {
            corazones[i].gameObject.SetActive(true);

        }
    }
}
