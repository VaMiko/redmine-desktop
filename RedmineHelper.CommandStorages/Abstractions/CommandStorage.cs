using RedmineHelper.States.Abstractions;

namespace RedmineHelper.CommandStorages.Abstractions
{
    using System.Collections.Generic;
    using RedmineHelper.Shared.Abstractions;

    /// <summary>
    /// Хранилище комманд
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TState">Состояние</typeparam>
    public abstract class CommandStorage<TState>
        where TState : State

    {
    private readonly IDictionary<string, IAsyncCommand> _storage;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="state"></param>
    protected CommandStorage(TState state)
    {
        State = state;
        _storage = new Dictionary<string, IAsyncCommand>();
        InitCommands();
    }

    /// <summary>
    /// Состояние
    /// </summary>
    protected TState State { get; }

    /// <summary>
    /// Добавить команду в хранилище
    /// </summary>
    /// <param name="commandName">Имя команды</param>
    /// <param name="command">команда</param>
    protected void AddCommand(string commandName, IAsyncCommand command) => _storage.Add(commandName, command);

    /// <summary>
    /// Получить команду
    /// </summary>
    /// <param name="commandName"></param>
    public object this[string commandName] => _storage[commandName];

    protected abstract void InitCommands();
    }
}
