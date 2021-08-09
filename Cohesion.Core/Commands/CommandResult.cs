using System;

namespace Cohesion.Core
{
    public sealed class CommandResult
    {
        public static readonly CommandResult None;
        public Guid Id { get; set; }
    }
}
