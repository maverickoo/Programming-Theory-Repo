using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Animal[] animals;
    [SerializeField] private TextMeshProUGUI animalName;


    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ClearSelect()
    {
        foreach(Animal animal in animals)
        {
            animal.ClearSelect();
        }
    }

    public void SetName(string name)
    {
        animalName.text = name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
