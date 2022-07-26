namespace WebGalery.Domain
{
    public static class ParamAssert
    {
        public static void IsTrue(bool assertion, string paramName)
        {
            IsTrue(assertion, paramName, $"The parameter {paramName} must be given");
        }
        public static void IsTrue(bool assertion, string paramName, string message)
        {
            if (!assertion)
                throw new ArgumentException(message, paramName);
        }

        public static T NotNull<T>(T obj, string paramName)
        {
            IsTrue(obj != null, paramName);
            return obj;
        }

        public static string NotEmtpy(string str, string paramName)
        {
            IsTrue(!string.IsNullOrEmpty(str), paramName);
            return str;
        }

        public static IReadOnlySet<T> AsReadonlySet<T>(this ISet<T> set, string paramName)
        {
            if (set is IReadOnlySet<T> readonlySet)
                return readonlySet;
            throw new ArgumentException($"The parameter {paramName} cannot be transfered to readonly set, original class name = {set.GetType().FullName}", paramName);
        }
    }
}
