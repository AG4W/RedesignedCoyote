///Axel Gustafson
///examples:
///Event.Subscribe(GlobalEvent.OnSomethingHappenedExample, (object[] args) => SomeMethod());
///Event.Raise(GlobalEvent.OnSomethingHappenedExample, 34, "someStringArg");
using System;
using System.Collections.Generic;

namespace ag4w.Events
{
    public static class Event
    {
        static List<Action<object[]>>[] _events;
        static bool _hasInitialized = false;

        static void Initialize()
        {
            _events = new List<Action<object[]>>[Enum.GetNames(typeof(GlobalEvent)).Length];

            for (int i = 0; i < _events.Length; i++)
                _events[i] = new List<Action<object[]>>();

            _hasInitialized = true;
        }

        //args should not use object[], unncessesary boxing
        /// <summary>
        /// Subscribes to the specified event.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        public static void Subscribe(GlobalEvent g, Action<object[]> a)
        {
            if (!_hasInitialized)
                Initialize();

            _events[(int)g].Add(a);
        }
        /// <summary>
        /// Raises the specified event.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="args"></param>
        public static void Raise(GlobalEvent g, params object[] args)
        {
            for (int i = 0; i < _events[(int)g].Count; i++)
                _events[(int)g][i]?.Invoke(args);
        }
    }
    public enum GlobalEvent
    {
        OnSomethingHappenedExample,
        OnSomethingElseHappenedExample
    }
}