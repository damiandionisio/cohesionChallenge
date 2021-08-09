using System;

namespace Cohesion.Core
{
    public sealed class CommandResult
    {
        public static CommandResult None => new CommandResult();
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as CommandResult;

            return other.Id == Id;
        }
    }
}
