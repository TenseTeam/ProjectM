﻿namespace VUDK.Patterns.Initialization.Interfaces
{
    public interface IInit
    {
        /// <summary>
        /// Initialize object.
        /// </summary>
        public void Init();

        /// <summary>
        /// Check object correct initialization.
        /// </summary>
        public bool Check();
    }

    public interface IInit<T>
    {
        /// <summary>
        /// Initialize object with its arguments.
        /// </summary>
        public void Init(T arg);

        /// <summary>
        /// Check object correct initialization.
        /// </summary>
        public bool Check();
    }
}