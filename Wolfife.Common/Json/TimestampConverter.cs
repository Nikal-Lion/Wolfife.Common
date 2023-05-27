using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Wolfife.Common.Json
{
    public class TimestampConverter : DateTimeConverterBase
    {
        public TimestampConverter() : base()
        {

        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double javaScriptTicks = value switch
            {

                DateTime time => ConvertDateTimeInt(time),
                DateTimeOffset offset => ConvertDateTimeInt(offset),
                _ => 0,

            };
            writer.WriteValue(javaScriptTicks);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ConvertIntDateTime(double.Parse(reader.Value.ToString()));
        }

        private static DateTime ConvertIntDateTime(double milliseconds)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(milliseconds);
        }

        /// <summary>
        /// 日期转换为时间戳（时间戳单位毫秒）
        /// </summary>
        /// <returns></returns>
        private static double ConvertDateTimeInt(DateTime dateTime)
        {
            if (dateTime.Year == 1)
            {
                return 0;
            }
            return ConvertDateTimeInt(new DateTimeOffset(dateTime));
        }
        /// <summary>
        /// 日期转换为时间戳（时间戳单位毫秒）
        /// </summary>
        /// <returns></returns>
        private static double ConvertDateTimeInt(DateTimeOffset offset) => offset.ToUnixTimeMilliseconds();

    }
}