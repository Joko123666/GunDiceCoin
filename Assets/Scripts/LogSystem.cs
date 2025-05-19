using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LogSystem : MonoBehaviour {
    public Text logText;
    private Queue<string> logQueue = new Queue<string>();
    public int maxLines = 5;

    public void AddEntry(string entry) {
        if (logQueue.Count >= maxLines) logQueue.Dequeue();
        logQueue.Enqueue(entry);
        UpdateLogDisplay();
    }

    private void UpdateLogDisplay() {
        logText.text = string.Join("\n", logQueue);
    }
}
