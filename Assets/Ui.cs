using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    [SerializeField] private Button button;

    private List<Texture3D> textures = new List<Texture3D>();

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            // Dealocate existing textures
            foreach (var texture in textures)
            {
                Destroy(texture);
            }

            textures.Clear();

            textures = VramFiller.Allocate3DTextures(float.Parse(inputField.text)).ToList();
        });
    }
}