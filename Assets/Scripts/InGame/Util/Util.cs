using System;

public class Util
{
    public static T GetEnumByString<T>(string column) where T : struct, Enum
    {
        if(Enum.TryParse<T>(column.ToUpper(), out T result))
            return result;
        return default(T);
    }
}
