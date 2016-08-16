

using System;

namespace ShareNet.Infrastructure.PetaPoco.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlBehaviorAttribute : Attribute

    {
        // Methods
        public SqlBehaviorAttribute(SqlBehaviorFlags behavior)
        {
            this.Behavior = behavior;
        }

        // Properties
        public SqlBehaviorFlags Behavior { get; private set; }

    }
}
