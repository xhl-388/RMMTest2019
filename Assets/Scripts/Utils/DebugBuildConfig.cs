using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugBuildConfig : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Debug.isDebugBuild)
        {
            UpdateDebug();
        }
#if UNITY_EDITOR
        UpdateDebug();
#endif
    }

    private void UpdateDebug()
    {
        // 按R可以重新开始
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
