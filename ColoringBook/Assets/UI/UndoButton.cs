using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Undo);
    }

    private void Undo()
    {
        if (gameController.brushStrokes.Count > 0)
        {
            gameController.brushStrokes.RemoveAt(gameController.brushStrokes.Count - 1);

            // You may also need to update the mesh or the rendering of the brush strokes
        }
    }
}