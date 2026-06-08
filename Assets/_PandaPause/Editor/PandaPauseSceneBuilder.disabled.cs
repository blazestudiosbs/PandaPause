using PandaPause.Core;
using PandaPause.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PandaPause.EditorTools
{
    public static class PandaPauseSceneBuilder
    {
        private const string MenuPath = "Panda Pause/Build/Clean Main Scene UI";
        private const string ScenePath = "Assets/_PandaPause/Scenes/PandaPause_Main.unity";

        [MenuItem(MenuPath)]
        public static void BuildMainSceneUi()
        {
            EnsureSceneFolderExists();

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            var appController = CreateGameObject("PandaAppController");
            appController.AddComponent<PandaAppController>();

            CreateMainCamera();
            CreateEventSystem();

            var canvas = CreateCanvas();

            var setupScreen = CreateScreenRoot(canvas.transform, "SetupScreen");
            var homeScreen = CreateScreenRoot(canvas.transform, "HomeScreen");

            var setupUi = setupScreen.AddComponent<SetupScreenUI>();
            var homeUi = homeScreen.AddComponent<HomeScreenUI>();

            BuildSetupScreen(setupScreen.transform);
            BuildHomeScreen(homeScreen.transform);

            WireSetupScreen(setupUi, setupScreen, homeScreen, homeUi);
            WireHomeScreen(homeUi, homeScreen);

            setupScreen.SetActive(true);
            homeScreen.SetActive(false);

            EditorSceneManager.MarkSceneDirty(scene);
            if (!EditorSceneManager.SaveScene(scene, ScenePath))
            {
                Debug.LogError($"PandaPause scene builder could not save the scene to '{ScenePath}'.");
            }
        }

        private static void EnsureSceneFolderExists()
        {
            const string sceneFolder = "Assets/_PandaPause/Scenes";
            if (!AssetDatabase.IsValidFolder(sceneFolder))
            {
                AssetDatabase.CreateFolder("Assets/_PandaPause", "Scenes");
            }
        }

        private static GameObject CreateGameObject(string name)
        {
            var go = new GameObject(name);
            Undo.RegisterCreatedObjectUndo(go, $"Create {name}");
            return go;
        }

        private static Camera CreateMainCamera()
        {
            var cameraObject = CreateGameObject("Main Camera");
            cameraObject.tag = "MainCamera";

            var camera = cameraObject.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = Color.black;

            return camera;
        }

        private static EventSystem CreateEventSystem()
        {
            var eventSystemObject = CreateGameObject("EventSystem");
            eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<StandaloneInputModule>();
            return eventSystemObject.GetComponent<EventSystem>();
        }

        private static Canvas CreateCanvas()
        {
            var canvasObject = CreateUiGameObject("Canvas");
            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            return canvas;
        }

        private static GameObject CreateScreenRoot(Transform parent, string name)
        {
            var screen = CreateUiGameObject(name);
            screen.transform.SetParent(parent, false);
            var rect = screen.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            return screen;
        }

        private static void BuildSetupScreen(Transform parent)
        {
            var title = CreateText(parent, "TitleText", "Panda Pause");
            ConfigureTextLayout(title.rectTransform, new Vector2(0f, 220f), new Vector2(560f, 60f));

            var userNameInput = CreateInputField(parent, "UserNameInput", "Your name");
            ConfigureInputLayout(userNameInput.GetComponent<RectTransform>(), new Vector2(0f, 120f));

            var pandaNameInput = CreateInputField(parent, "PandaNameInput", "Panda name");
            ConfigureInputLayout(pandaNameInput.GetComponent<RectTransform>(), new Vector2(0f, 60f));

            var continueButton = CreateButton(parent, "ContinueButton", "Continue");
            ConfigureButtonLayout(continueButton.GetComponent<RectTransform>(), new Vector2(0f, -10f));
        }

        private static void BuildHomeScreen(Transform parent)
        {
            var greetingText = CreateText(parent, "GreetingText", "Hi, Friend.");
            ConfigureTextLayout(greetingText.rectTransform, new Vector2(0f, 120f), new Vector2(560f, 60f));

            var pandaPromptText = CreateText(parent, "PandaPromptText", "Maple is here. How are you feeling today?");
            ConfigureTextLayout(pandaPromptText.rectTransform, new Vector2(0f, 60f), new Vector2(640f, 60f));
        }

        private static Text CreateText(Transform parent, string name, string text)
        {
            var go = CreateUiGameObject(name);
            go.transform.SetParent(parent, false);

            var uiText = go.AddComponent<Text>();
            uiText.text = text;
            uiText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            uiText.color = Color.white;
            uiText.alignment = TextAnchor.MiddleLeft;
            uiText.horizontalOverflow = HorizontalWrapMode.Overflow;
            uiText.verticalOverflow = VerticalWrapMode.Overflow;
            uiText.raycastTarget = false;

            return uiText;
        }

        private static InputField CreateInputField(Transform parent, string name, string placeholderText)
        {
            var go = CreateUiGameObject(name);
            go.transform.SetParent(parent, false);

            var inputField = go.AddComponent<InputField>();
            var rectTransform = go.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(320f, 40f);

            var background = go.AddComponent<Image>();
            background.color = new Color(1f, 1f, 1f, 0.12f);

            var textObject = CreateUiGameObject("Text");
            textObject.transform.SetParent(go.transform, false);
            var text = textObject.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleLeft;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.raycastTarget = false;
            var textRect = textObject.GetComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.05f, 0.1f);
            textRect.anchorMax = new Vector2(0.95f, 0.9f);
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var placeholderObject = CreateUiGameObject("Placeholder");
            placeholderObject.transform.SetParent(go.transform, false);
            var placeholder = placeholderObject.AddComponent<Text>();
            placeholder.text = placeholderText;
            placeholder.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            placeholder.fontStyle = FontStyle.Italic;
            placeholder.color = new Color(1f, 1f, 1f, 0.5f);
            placeholder.alignment = TextAnchor.MiddleLeft;
            placeholder.horizontalOverflow = HorizontalWrapMode.Overflow;
            placeholder.verticalOverflow = VerticalWrapMode.Overflow;
            placeholder.raycastTarget = false;
            var placeholderRect = placeholderObject.GetComponent<RectTransform>();
            placeholderRect.anchorMin = new Vector2(0.05f, 0.1f);
            placeholderRect.anchorMax = new Vector2(0.95f, 0.9f);
            placeholderRect.offsetMin = Vector2.zero;
            placeholderRect.offsetMax = Vector2.zero;

            inputField.targetGraphic = background;
            inputField.textComponent = text;
            inputField.placeholder = placeholder;

            return inputField;
        }

        private static Button CreateButton(Transform parent, string name, string labelText)
        {
            var go = CreateUiGameObject(name);
            go.transform.SetParent(parent, false);

            var button = go.AddComponent<Button>();
            var background = go.AddComponent<Image>();
            background.color = new Color(1f, 1f, 1f, 0.18f);
            button.targetGraphic = background;

            var rectTransform = go.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(180f, 44f);

            var labelObject = CreateUiGameObject("Text");
            labelObject.transform.SetParent(go.transform, false);
            var label = labelObject.AddComponent<Text>();
            label.text = labelText;
            label.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            label.color = Color.white;
            label.alignment = TextAnchor.MiddleCenter;
            label.horizontalOverflow = HorizontalWrapMode.Overflow;
            label.verticalOverflow = VerticalWrapMode.Overflow;
            label.raycastTarget = false;
            var labelRect = labelObject.GetComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.offsetMin = Vector2.zero;
            labelRect.offsetMax = Vector2.zero;

            return button;
        }

        private static GameObject CreateUiGameObject(string name)
        {
            var go = new GameObject(name, typeof(RectTransform));
            Undo.RegisterCreatedObjectUndo(go, $"Create {name}");
            return go;
        }

        private static void ConfigureTextLayout(RectTransform rectTransform, Vector2 anchoredPosition, Vector2 sizeDelta)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = sizeDelta;
        }

        private static void ConfigureInputLayout(RectTransform rectTransform, Vector2 anchoredPosition)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = anchoredPosition;
        }

        private static void ConfigureButtonLayout(RectTransform rectTransform, Vector2 anchoredPosition)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = anchoredPosition;
        }

        private static void WireSetupScreen(SetupScreenUI setupUi, GameObject setupScreen, GameObject homeScreen, HomeScreenUI homeUi)
        {
            Debug.Log("[PandaPauseSceneBuilder] SetupScreenUI component found: " + (setupUi != null));
            var serializedObject = new SerializedObject(setupUi);
            serializedObject.Update();
            var userNameInputTransform = setupScreen.transform.Find("UserNameInput");
            InputField userNameInput = null;
            if (userNameInputTransform != null)
            {
                userNameInput = userNameInputTransform.GetComponent<InputField>();
            }
            Debug.Log("[PandaPauseSceneBuilder] userNameInput object found: " + (userNameInput != null));
            SetObjectReference(serializedObject, "userNameInput", userNameInput);

            var pandaNameInputTransform = setupScreen.transform.Find("PandaNameInput");
            InputField pandaNameInput = null;
            if (pandaNameInputTransform != null)
            {
                pandaNameInput = pandaNameInputTransform.GetComponent<InputField>();
            }
            Debug.Log("[PandaPauseSceneBuilder] pandaNameInput object found: " + (pandaNameInput != null));
            SetObjectReference(serializedObject, "pandaNameInput", pandaNameInput);

            var continueButtonTransform = setupScreen.transform.Find("ContinueButton");
            Button continueButton = null;
            if (continueButtonTransform != null)
            {
                continueButton = continueButtonTransform.GetComponent<Button>();
            }
            Debug.Log("[PandaPauseSceneBuilder] continueButton object found: " + (continueButton != null));
            SetObjectReference(serializedObject, "continueButton", continueButton);

            Debug.Log("[PandaPauseSceneBuilder] setupScreen object found: " + (setupScreen != null));
            SetObjectReference(serializedObject, "setupScreen", setupScreen);
            Debug.Log("[PandaPauseSceneBuilder] homeScreen object found: " + (homeScreen != null));
            SetObjectReference(serializedObject, "homeScreen", homeScreen);
            Debug.Log("[PandaPauseSceneBuilder] HomeScreenUI component found: " + (homeUi != null));
            SetObjectReference(serializedObject, "homeScreenUI", homeUi);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(setupUi);
        }

        private static void WireHomeScreen(HomeScreenUI homeUi, GameObject homeScreen)
        {
            var serializedObject = new SerializedObject(homeUi);
            serializedObject.Update();
            var greetingTextTransform = homeScreen.transform.Find("GreetingText");
            Text greetingText = null;
            if (greetingTextTransform != null)
            {
                greetingText = greetingTextTransform.GetComponent<Text>();
            }
            SetObjectReference(serializedObject, "greetingText", greetingText);

            var pandaPromptTextTransform = homeScreen.transform.Find("PandaPromptText");
            Text pandaPromptText = null;
            if (pandaPromptTextTransform != null)
            {
                pandaPromptText = pandaPromptTextTransform.GetComponent<Text>();
            }
            SetObjectReference(serializedObject, "pandaPromptText", pandaPromptText);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(homeUi);
        }

        private static void SetObjectReference(SerializedObject serializedObject, string propertyName, Object reference)
        {
            var property = serializedObject.FindProperty(propertyName);
            Debug.Log("[PandaPauseSceneBuilder] SerializedProperty '" + propertyName + "' lookup returned null: " + (property == null));
            if (property == null)
            {
                Debug.LogWarning("PandaPause scene builder could not find serialized field '" + propertyName + "'.");
                return;
            }

            property.objectReferenceValue = reference;
        }
    }
}
