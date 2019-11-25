using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroExpansionNS
{
    public class MacroExpansionClass
    {
        public static IEnumerable<T> MacroExpansion<T>(IEnumerable<T> sequence, IEnumerable<T> newValues, T value){
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            if (newValues == null)
                throw new ArgumentNullException(nameof(newValues));
            return UnsafeMacroExpansion(sequence,newValues,value);
        }

        private static IEnumerable<T> UnsafeMacroExpansion<T>(IEnumerable<T> sequence, IEnumerable<T> newValues, T value){
            foreach (var e in sequence)
                if (Equals(e, value))
                    foreach (var v in newValues)
                        yield return v;
                else
                    yield return e;
        }
    }
}
