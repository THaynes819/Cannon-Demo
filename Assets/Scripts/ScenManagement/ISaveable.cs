using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SceneManagement
{
    public interface ISaveable
    {
        /// <summary>
        /// Called when saving to capture the state of the component.
        /// </summary>
        /// <returns>
        /// Return a `System.Serializable` object that represents the state of the
        /// component.
        /// </returns>
        object CaptureState();

        /// <summary>
        /// Called when restoring the state of a scene.
        /// </summary>
        /// <param name="state">
        /// The same `System.Serializable` object that was returned by
        /// CaptureState when saving.
        /// </param>
        void RestoreState(object state);
    }
}