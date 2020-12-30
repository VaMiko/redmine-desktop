namespace RedmineHelper.States.Abstractions
{
    /// <summary>
    /// Состояние ViewModel
    /// </summary>
    public abstract class State
    {
        protected State() => RegisterDependencies();
        protected abstract void RegisterDependencies();
    }
}
