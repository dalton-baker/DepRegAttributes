namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

[RegisterTransient(Tag = "Two")]
public class TaggedTwo
{
}



public enum RegisterTag
{
    Test
}

[RegisterTransient(Tag = RegisterTag.Test)]
public class EnumTagClass
{
}


[RegisterTransient(Tag = 1)]
public class IntTagClass
{
}

[RegisterTransient(Tag = ConstTag)]
public class ConstTagClass
{
    public const string ConstTag = "Const Tag";

}
