using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class AuditLogChange
    {
        [ModelProperty("new_value")]
        //[ModelSelector("key", ModelSelectorGroups.AuditLogChange)]
        public object NewValue { get; set; }
        [ModelProperty("old_value")]
        //[ModelSelector("key", ModelSelectorGroups.AuditLogChange)]
        public object OldValue { get; set; }
        [ModelProperty("key")]
        public AuditLogChangeKey Key { get; set; }
    }
}
