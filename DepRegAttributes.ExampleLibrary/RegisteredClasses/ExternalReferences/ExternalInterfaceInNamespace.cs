﻿using DepRegAttributes.ExternalExampleLibrary.SubNamespace;

namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.ExternalReferences;

[RegisterTransient<IExternalInterfaceInNamespace>]
public class ExternalInterfaceInNamespace: IExternalInterfaceInNamespace
{

}