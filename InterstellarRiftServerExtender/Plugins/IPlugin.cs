using System;

namespace IRSE.Plugins
{
    public interface IPlugin
    {
        #region Properties

        Guid Id
        { get; }

        string Name
        { get; }

        string Version
        { get; }

        #endregion Properties

        #region Methods

        void Init(String ModDirectory);

        void Shutdown();

        #endregion Methods
    }
}