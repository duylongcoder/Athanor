using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The two language files; publishes them through Loc and picks the start language from the saved settings.</summary>
    [DefaultExecutionOrder(-180)]
    public sealed class LocalizationManager : Singleton<LocalizationManager>
    {
        [Tooltip("Assets/Resources/lang/en.json")] [SerializeField] TextAsset english;
        [Tooltip("Assets/Resources/lang/vi.json")] [SerializeField] TextAsset vietnamese;

        protected override void OnAwake()
        {
            if (english == null || vietnamese == null) throw new System.InvalidOperationException("LocalizationManager: a language file is not set");
            Loc.Use(english, vietnamese);
            Loc.InitFromPlayer(SaveManager.Instance.Settings.language);
        }
    }
}
