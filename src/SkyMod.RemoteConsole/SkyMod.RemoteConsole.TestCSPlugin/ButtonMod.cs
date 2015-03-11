using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace SkyMod.RemoteConsole.TestCSPlugin
{
    public class ButtonMod : IUserMod
    {
        public string Name { get { return "Button"; } }
        public string Description { get { return "Shows a button"; } }
    }

    public class LoadingExtension : LoadingExtensionBase
    {
        // Find a GameObject by name.
        private static GameObject FindObjectByName(string name)
        {
            var gameObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.name == name) return gameObject;
            }

            return null;
        }

        public override void OnCreated(ILoading loading)
        {
            RemoteConsole.Client.ConsoleClient.Info("LoadingExtension was created");
            base.OnCreated(loading);
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            RemoteConsole.Client.ConsoleClient.Info("The level was loaded.");

            // Get the UIView object. This seems to be the top-level object for most
            // of the UI.
            var uiView = FindObjectByName("UIView");
            if (uiView == null) return;

            // Create a GameObject with a ColossalFramework.UI.UIButton component.
            var buttonObject = new GameObject("MyButton", typeof(UIButton));

            // Make the buttonObject a child of the uiView.
            buttonObject.transform.parent = uiView.transform;

            // Get the button component.
            var button = buttonObject.GetComponent<UIButton>();

            // Set the text to show on the button.
            button.text = "Click Me!";

            // Set the button dimensions.
            button.width = 100;
            button.height = 30;

            // Style the button to look like a menu button.
            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenuFocused";
            button.pressedBgSprite = "ButtonMenuPressed";
            button.textColor = new Color32(255, 255, 255, 255);
            button.disabledTextColor = new Color32(7, 7, 7, 255);
            button.hoveredTextColor = new Color32(7, 132, 255, 255);
            button.focusedTextColor = new Color32(255, 255, 255, 255);
            button.pressedTextColor = new Color32(30, 30, 44, 255);

            // Place the button.
            button.transformPosition = new Vector3(-1.65f, 0.97f);

            // Respond to button click.
            button.eventClick += ButtonClick;
        }

        private void ButtonClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            // Do something
            RemoteConsole.Client.ConsoleClient.Info("The button was clicked.");
            RemoteConsole.Client.ConsoleClient.Warn("This should be yellow (warning).");
            RemoteConsole.Client.ConsoleClient.Error("This should be red (error).");
            RemoteConsole.Client.ConsoleClient.Info("This should be normal.");
        }
    }
}
