using System;
using System.Collections.Generic;

namespace TheFoundLinq.Classes
{
    /// <summary>
    /// A very funky comparer
    /// </summary>
    public class FuncComparer<T, TCompared> : IEqualityComparer<T> where TCompared : IEquatable<TCompared>
    {
        private Func<T, TCompared> _comparingFunction;

        public FuncComparer(Func<T, TCompared> comparingFunction)
        {
            _comparingFunction = comparingFunction;
        }

        public bool Equals(T x, T y)
        {
            var valX = _comparingFunction(x);
            var valY = _comparingFunction(y);

            if (valX == null || valY == null)
            {
                return valX == null && valY == null;
            }
            else
            {
                return valX.Equals(valY);
            }
        }

        public int GetHashCode(T obj)
        {
            var valX = _comparingFunction(obj);
            if (valX == null)
            {
                return 0;
            }

            var hashCode = valX.GetHashCode();
            return hashCode;
        }
    }
}
