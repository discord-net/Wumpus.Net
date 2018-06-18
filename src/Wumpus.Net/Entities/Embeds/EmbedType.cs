using Voltaic.Serialization;

namespace Wumpus.Entities
{
    [ModelStringEnum]
    public enum EmbedType
    {
        [ModelEnumValue("rich")] Rich,
        [ModelEnumValue("link")] Link,
        [ModelEnumValue("video")] Video,
        [ModelEnumValue("image")] Image,
        [ModelEnumValue("gifv")] Gifv,
        [ModelEnumValue("article")] Article,
        [ModelEnumValue("tweet")] Tweet
    }
}
