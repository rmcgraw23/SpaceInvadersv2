using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SpaceInvaders.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Converts to an ObservableCollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>the observable collection</returns>

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }
    }
}
