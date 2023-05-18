namespace kwd.CoreUtil.Tests.String.samples;

public static class DemoRefactor
{
    //Sample method before factoring with DataString
    public static string EchoPathSegment(string data)
    {
        // check data is trimmed.
        //check length, chars etc.

        return data.ToUpper();
    }

    //Sample method after factoring with DataString
    // replace the param with DataString.
    public static string Echo(PathSegment data) => 
        data.ToString().ToUpper();
}
