﻿namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.KeyedServices;


public interface IKeyedWithType
{
}

[RegisterTransient(types: typeof(IKeyedWithType), Key = typeof(TypeKey))]
public class KeyedWithType : IKeyedWithType
{
}

public class TypeKey { }