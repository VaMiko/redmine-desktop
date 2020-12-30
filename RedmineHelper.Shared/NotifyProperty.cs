namespace RedmineHelper.Shared
{
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;
    public class NotifyProperty<T> : INotifyPropertyChanged
    {
        private T _backingField;
        public NotifyProperty(T defaultValue)
        {
            _backingField = defaultValue;
            OnPropertyChanged();
        }

        public T Value
        {
            get => _backingField;
            set
            {
                _backingField = value;

                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Добавляет зависимости свойства
        /// </summary>
        /// <param name="dep">Зависимость для нотификации</param>
        /// <param name="action">Действие для обновления зависимости</param>
        public void AddDependencies(PropertyChangedEventHandler action)
        {
            if(Value is INotifyCollectionChanged collection)
            {
                collection.CollectionChanged += (sender, args) => action(sender, null);
                return;
            }
            PropertyChanged += action;
        }
    }
}
