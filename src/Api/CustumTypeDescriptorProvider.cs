using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Api
{
    public class CustumTypeDescriptorProvider : TypeDescriptionProvider
    {
        public override ICustomTypeDescriptor GetTypeDescriptor(System.Type objectType, object instance)
        {
            if (objectType.Name == "List`1" && objectType.GetGenericArguments()[0].Name == "String")
                return new ListOfStringDescriptor();
            return base.GetTypeDescriptor(objectType, instance);
        }
    }

    public class ListOfStringDescriptor : CustomTypeDescriptor
    {
        public override TypeConverter GetConverter()
        {
            return new ListOfStringConverter();
        }
    }

    public class ListOfStringConverter : ArrayConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string s = value as string;

            if (!string.IsNullOrEmpty(s))
            {
                return ((string)value).Split(',').ToList();
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}