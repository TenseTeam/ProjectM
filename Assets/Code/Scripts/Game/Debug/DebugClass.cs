namespace ProjectM.Debug
{
    using System;
    using System.IO;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;
    using VUDK.Extensions;

    public class DebugClass : MonoBehaviour
    {

        [ContextMenu("Print File Location")]
        public void PrintFileLocation()
        {
            TraceMessage("Print File Location");
        }

        public void TraceMessage(string message,
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            Debug.Log("message: " + message);
            Debug.Log("member name: " + memberName);
            Debug.Log("source file path: " + sourceFilePath);
            Debug.Log("source line number: " + sourceLineNumber);
        }
    }
}
