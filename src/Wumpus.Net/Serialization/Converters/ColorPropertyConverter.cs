using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Serialization.Json.Converters
{
    internal class ColorPropertyConverter : ValueConverter<Color>
    {
        private readonly UInt32PropertyConverter _innerConverter = new UInt32PropertyConverter();

        public override Color Read(Serializer serializer, ModelMap modelMap, PropertyMap propMap, object model, ref JsonReader reader, bool isTopLevel)
        {
            uint value = _innerConverter.Read(serializer, modelMap, propMap, model, ref reader, isTopLevel);
            return new Color(value);
        }
        public override void Write(Serializer serializer, ModelMap modelMap, PropertyMap propMap, object model, ref JsonWriter writer, Color value, string key)
        {
            _innerConverter.Write(serializer, modelMap, propMap, model, ref writer, value.RawValue, key);
        }
    }
}
