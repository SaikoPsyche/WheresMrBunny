using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    [SerializeField] private UnitStats_SO[] characters;
    public UnitStats_SO Character;

    private void Awake()
    {
        Instance = this;
    }

    public void GetCharacter(int index)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == index)
            {
                Character = characters[i];
            }

            else if (i > index) return;
        }
    }
}
