using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class BehaviourTreeWindow : EditorWindow
{
    [MenuItem("BehaviourTreeWindow/Editor ...")]
    public static void OpenWndow()
    {
        BehaviourTreeWindow wnd = GetWindow<BehaviourTreeWindow>();
        wnd.titleContent = new GUIContent("BehaviourTreeWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        
        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviourTreeWindow.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeWindow.uss");
        root.styleSheets.Add(styleSheet);
    }
}