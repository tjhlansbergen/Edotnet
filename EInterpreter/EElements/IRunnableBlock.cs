﻿namespace EInterpreter.EElements;

public interface IRunnableBlock
{
    List<EElement> Elements { get; }
    string Name { get; }
}