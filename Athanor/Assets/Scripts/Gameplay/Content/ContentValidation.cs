namespace Athanor.Gameplay
{
    /// <summary>Runs ContentDb's validation on a database and returns the first problem, or null when the content is valid.</summary>
    public static class ContentValidation
    {
        public static string Check(ContentDatabase db)
        {
            try { new ContentDb(db.ToContentData()); return null; }
            catch (System.Exception e) { UnityEngine.Debug.LogWarning("[content] " + e.Message); return e.Message; }
        }
    }
}
