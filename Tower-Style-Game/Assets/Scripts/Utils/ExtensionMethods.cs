using System;
using System.Collections.Generic;

public static class ExtensionMethods {

    public static float Map(this float value, float fromSource, float toSource, float fromTarget, float toTarget) {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public static bool AmIAbleToBuyIt(int myCurrencyAmount, float price) {
        if (myCurrencyAmount - price < 0) {
            return false;
        } else {
            return true;
        }
    }
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) {
        HashSet<TKey> seenKeys = new HashSet<TKey>();
        foreach (TSource element in source) {
            if (seenKeys.Add(keySelector(element))) {
                yield return element;
            }
        }
    }

}

