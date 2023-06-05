using System;
using JetBrains.Annotations;

namespace Custom.DependencyInjection
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute
    {
    }
}