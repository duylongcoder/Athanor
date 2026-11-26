using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The game's content: the ContentDatabase asset, validated and indexed once at start-up.</summary>
    [DefaultExecutionOrder(-200)]
    public sealed class ContentManager : Singleton<ContentManager>
    {
        [Tooltip("Assets/Content/ContentDatabase.asset")]
        [SerializeField] ContentDatabase database;

        /// <summary>The asset the Inspector points at.</summary>
        public ContentDatabase Database => database;
        /// <summary>The validated content the rules read.</summary>
        public ContentDb Db { get; private set; }

        protected override void OnAwake()
        {
            if (database == null) throw new System.InvalidOperationException("ContentManager: database is not set (Assets/Content/ContentDatabase.asset)");
            Db = new ContentDb(database.ToContentData());
        }
    }
}
