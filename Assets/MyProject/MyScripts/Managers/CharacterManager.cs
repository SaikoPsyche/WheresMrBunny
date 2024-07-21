using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private EnemyStats_SO[] characters;

    public static CharacterDetails Character;

    public struct CharacterDetails
    {
        public int Index;
        public string Name;
        public Sprite Sprite;
        public bool IsOverrideController;
        public AnimatorController AnimatorController;
        public AnimatorOverrideController AnimOverrideController;
        public int Health;
        public int Strength;
        public float MSMultiplier;
        public float JumpHeightMultiplier;
        public float ASMultiplier;
        public float SizeMultiplier;
    }

    private void OnEnable()
    {
        EventManager.OnChooseCharacter += GetCharacter;
    }

    private void OnDisable()
    {
        EventManager.OnChooseCharacter -= GetCharacter;
    }

    public void GetCharacter(int index)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == index)
            {
                Character.Index = index; // for debugging
                Character.Name = characters[i].Name;

                if (characters[i].Sprite != null) Character.Sprite = characters[i].Sprite;
                else Debug.Log(name + $": Sprite does not exist. \nCharacter index is {Character.Index}.");

                Character.IsOverrideController = characters[i].IsOverrideController;
                Debug.Log(name + $": Use Character Override Controller = {Character.IsOverrideController}");

                if (characters[i].AnimatorController != null || characters[i].AnimOverrideController != null)
                {
                    switch (Character.IsOverrideController)
                    {
                        case true:
                            Character.AnimOverrideController = characters[i].AnimOverrideController;
                            Debug.Log(name + $": AnimatorController = {Character.AnimOverrideController}");
                            break;
                        case false:
                            Character.AnimatorController = characters[i].AnimatorController;
                            Debug.Log(name + $": AnimatorController = {Character.AnimatorController}");
                            break;
                    }
                }
                else if (characters[i].AnimatorController == null & characters[i].AnimOverrideController == null)
                    Debug.Log(name + $": Animator Controller and Animator Override Controller do not exist. \nCharacter index is {Character.Index}.");
                
                Character.Health = characters[i].MaxHP;
                Character.Strength = characters[i].CurrentStr;
                Character.MSMultiplier = characters[i].CurrentMS;
                Character.JumpHeightMultiplier = characters[i].CurrentJumpHeight;
                Character.ASMultiplier = characters[i].CurrentAS;
            }

            else if (i > index) return;
        }

        this.index = index; // for debugging
    }
}
