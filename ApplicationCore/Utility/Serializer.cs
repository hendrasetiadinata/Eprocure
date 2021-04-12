using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace ApplicationCore.Utility
{
    public class Serializer
    {
        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, DefaultSetting());
        }

        public static string Serialize(object data, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(data, settings ?? DefaultSetting());
        }

        public static T DeserializeToObj<T>(string data) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(data, DefaultSetting());
        }

        public static T DeserializeToObj<T>(string data, JsonSerializerSettings settings) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(data, settings ?? DefaultSetting());
        }

        public static List<T> DeserializeToListObj<T>(string data) where T : new()
        {
            return JsonConvert.DeserializeObject<List<T>>(data, DefaultSetting());
        }

        public static List<T> DeserializeToListObj<T>(string data, JsonSerializerSettings settings) where T : new()
        {
            return JsonConvert.DeserializeObject<List<T>>(data, settings ?? DefaultSetting());
        }

        private static JsonSerializerSettings DefaultSetting() => new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }

    public class IgnoreEmptyEnumerableResolver : DefaultContractResolver
    {
        public static readonly IgnoreEmptyEnumerableResolver Instance = new IgnoreEmptyEnumerableResolver();

        protected override JsonProperty CreateProperty(MemberInfo member,
            MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType != typeof(string) &&
                typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = instance =>
                {
                    IEnumerable enumerable = null;
                    // this value could be in a public field or public property
                    switch (member.MemberType)
                    {
                        case MemberTypes.Property:
                            enumerable = instance
                                .GetType()
                                .GetProperty(member.Name)
                                ?.GetValue(instance, null) as IEnumerable;
                            break;
                        case MemberTypes.Field:
                            enumerable = instance
                                .GetType()
                                .GetField(member.Name)
                                .GetValue(instance) as IEnumerable;
                            break;
                    }

                    return enumerable == null ||
                           enumerable.GetEnumerator().MoveNext();
                    // if the list is null, we defer the decision to NullValueHandling
                };
            }

            return property;
        }
    }
}
