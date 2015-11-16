using System;
 
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(new Program().M());
    }
 
    bool M()
    {	
        C myVar = new C(true);
        return myVar == true ? true : false;
 
        // Removing " == true" changes the output of the program from "False" to "True"
        // Removing the ternary operator causes a build failure.
        // Removing both causes a build failure.
		
        // If myVar is a nullable bool, the "== true" cannot be removed, but the ternary
        // can be removed..
    }
}
 
class C
{
    private bool field = false;
 
    public C(bool param)
    {
        this.field = param;
    }
 
    // Evil. This essentially negates c, making the "== true" check mandatory.
    public static C operator ==(C c, bool other)
    {
        return new C(c.field != other);
    }
 
    public static C operator !=(C c, bool other)
    {
        return new C(c.field == other);
    }

    // Allows the ternary operator to work. Note the lack of a conversion from C to bool.
    public static bool operator true(C c)
    {
        return c.field;
    }
 
    public static bool operator false(C c)
    {
        return !c.field;
    }
}