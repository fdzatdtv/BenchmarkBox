using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BenchmarkBox.Polymorph;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BenchmarkBox
{
    [SimpleJob(RuntimeMoniker.Mono)]
    public class PolymorphismBenchmark
    {
        private List<AValue> _abstractValues;
        private List<IntValue> _intValues;
        private List<VariantValue> _variantValues;

        [Params(1000)]
        public int N;
        private List<string> _sourceValues;
        
        [GlobalSetup]
        public void GlobalSetup()
        {
            _sourceValues = Enumerable.Range(1, N)
                .Select(n => n.ToString())
                .ToList();
            InsertAbstract();
            InsertConcrete();
            InsertVariant();
        }
        
        // [Benchmark(Baseline = true)]
        public void InsertAbstract()
        {
            _abstractValues = new List<AValue>(N);
            for (int i = 0; i < N; i++)
                _abstractValues.Add(new IntValue(_sourceValues[i]));
        }

        // [Benchmark]
        public void InsertConcrete()
        {
            _intValues = new List<IntValue>(N);
            for (int i = 0; i < N; i++)
                _intValues.Add(new IntValue(_sourceValues[i]));
        }

        // [Benchmark]
        public void InsertVariant()
        {
            _variantValues = new List<VariantValue>(N);
            for (int i = 0; i < N; i++)
                _variantValues.Add(new VariantValue(_sourceValues[i], SourceType.Int));
        }

        [Benchmark(Baseline = true)]
        public void ConvertAbstractAsLast()
        {
            foreach (AValue aValue in _abstractValues)
            {
                if (aValue is BoolValue boolVal)
                    _ = boolVal.GetValueOrDefault();
                else if (aValue is DateValue dateVal)
                    _ = dateVal.GetValueOrDefault();
                else if (aValue is DateTimeValue datetimeVal)
                    _ = datetimeVal.GetValueOrDefault();
                else if (aValue is LongValue longVal)
                    _ = longVal.GetValueOrDefault();
                else if (aValue is ULongValue ulongVal)
                    _ = ulongVal.GetValueOrDefault();
                else if (aValue is DoubleValue doubleVal)
                    _ = doubleVal.GetValueOrDefault();
                else if (aValue is ShortValue shortVal)
                    _ = shortVal.GetValueOrDefault();
                else if (aValue is UShortValue ushortVal)
                    _ = ushortVal.GetValueOrDefault();
                else if (aValue is TimeSpanValue timespanVal)
                    _ = timespanVal.GetValueOrDefault();
                else if (aValue is DecimalValue decimalVal)
                    _ = decimalVal.GetValueOrDefault();
                else if (aValue is ByteValue byteVal)
                    _ = byteVal.GetValueOrDefault();
                else if (aValue is SByteValue sbyteVal)
                    _ = sbyteVal.GetValueOrDefault();
                else if (aValue is StringValue stringVal)
                    _ = stringVal.GetValueOrDefault();
                else if (aValue is CharValue charVal)
                    _ = charVal.GetValueOrDefault();
                else if (aValue is FloatValue floatVal)
                    _ = floatVal.GetValueOrDefault();
                else if (aValue is DayOfWeekValue dowVal)
                    _ = dowVal.GetValueOrDefault();
                else if (aValue is UIntValue uintVal)
                    _ = uintVal.GetValueOrDefault();
                else if (aValue is IntValue intVal)
                    _ = intVal.GetValueOrDefault();
            }
        }
        
        [Benchmark]
        public void ConvertAbstractAsFirst()
        {
            foreach (AValue aValue in _abstractValues)
            {
                if (aValue is IntValue intVal)
                    _ = intVal.GetValueOrDefault();
                else if (aValue is BoolValue boolVal)
                    _ = boolVal.GetValueOrDefault();
                else if (aValue is DateValue dateVal)
                    _ = dateVal.GetValueOrDefault();
                else if (aValue is DateTimeValue datetimeVal)
                    _ = datetimeVal.GetValueOrDefault();
                else if (aValue is LongValue longVal)
                    _ = longVal.GetValueOrDefault();
                else if (aValue is ULongValue ulongVal)
                    _ = ulongVal.GetValueOrDefault();
                else if (aValue is DoubleValue doubleVal)
                    _ = doubleVal.GetValueOrDefault();
                else if (aValue is ShortValue shortVal)
                    _ = shortVal.GetValueOrDefault();
                else if (aValue is UShortValue ushortVal)
                    _ = ushortVal.GetValueOrDefault();
                else if (aValue is TimeSpanValue timespanVal)
                    _ = timespanVal.GetValueOrDefault();
                else if (aValue is DecimalValue decimalVal)
                    _ = decimalVal.GetValueOrDefault();
                else if (aValue is ByteValue byteVal)
                    _ = byteVal.GetValueOrDefault();
                else if (aValue is SByteValue sbyteVal)
                    _ = sbyteVal.GetValueOrDefault();
                else if (aValue is StringValue stringVal)
                    _ = stringVal.GetValueOrDefault();
                else if (aValue is CharValue charVal)
                    _ = charVal.GetValueOrDefault();
                else if (aValue is FloatValue floatVal)
                    _ = floatVal.GetValueOrDefault();
                else if (aValue is DayOfWeekValue dowVal)
                    _ = dowVal.GetValueOrDefault();
                else if (aValue is UIntValue uintVal)
                    _ = uintVal.GetValueOrDefault();
            }
        }
        
        [Benchmark]
        public void ConvertConcrete()
        {
            foreach (IntValue intVal in _intValues)
            {
                _ = intVal.GetValueOrDefault();
            }
        }
        
        [Benchmark]
        public void ConvertVariantAsLast()
        {
            foreach (VariantValue variantVal in _variantValues)
            {
                switch (variantVal.SrcType)
                {
                    case SourceType.String:
                        _ = variantVal.SourceValue;
                        break;
                    case SourceType.Bool:
                        _ = variantVal.GetBoolOrDefault();
                        break;
                    case SourceType.UInt:
                        _ = variantVal.GetUIntOrDefault();
                        break;
                    case SourceType.Long:
                        _ = variantVal.GetLongOrDefault();
                        break;
                    case SourceType.ULong:
                        _ = variantVal.GetULongOrDefault();
                        break;
                    case SourceType.Double:
                        _ = variantVal.GetDoubleOrDefault();
                        break;
                    case SourceType.Float:
                        _ = variantVal.GetFloatOrDefault();
                        break;
                    case SourceType.Char:
                        _ = variantVal.GetCharOrDefault();
                        break;
                    case SourceType.Short:
                        _ = variantVal.GetShortOrDefault(); 
                        break;
                    case SourceType.UShort:
                        _ = variantVal.GetUShortOrDefault();
                        break;
                    case SourceType.Byte:
                        _ = variantVal.GetByteOrDefault();
                        break;
                    case SourceType.SByte:
                        _ = variantVal.GetSByteOrDefault();
                        break;
                    case SourceType.TimeSpan:
                        _ = variantVal.GetTimeSpanOrDefault();
                        break;
                    case SourceType.Date:
                        _ = variantVal.GetDateOrDefault();
                        break;
                    case SourceType.DateTime:
                        _ = variantVal.GetDateTimeOrDefault();
                        break;
                    case SourceType.Decimal:
                        _ = variantVal.GetDecimalOrDefault();
                        break;
                    case SourceType.DayOfWeek:
                        _ = variantVal.GetDayOfWeekOrDefault();
                        break;
                    case SourceType.Int:
                        _ = variantVal.GetIntOrDefault();
                        break;
                    default:
                        ThrowArgumentOutOfRangeException(variantVal.SrcType);
                        break;
                }
            }
        }

        [Benchmark]
        public void ConvertVariantAsFirst()
        {
            foreach (VariantValue variantVal in _variantValues)
            {
                switch (variantVal.SrcType)
                {
                    case SourceType.Int:
                        _ = variantVal.GetIntOrDefault();
                        break;
                    case SourceType.String:
                        _ = variantVal.SourceValue;
                        break;
                    case SourceType.Bool:
                        _ = variantVal.GetBoolOrDefault();
                        break;
                    case SourceType.UInt:
                        _ = variantVal.GetUIntOrDefault();
                        break;
                    case SourceType.Long:
                        _ = variantVal.GetLongOrDefault();
                        break;
                    case SourceType.ULong:
                        _ = variantVal.GetULongOrDefault();
                        break;
                    case SourceType.Double:
                        _ = variantVal.GetDoubleOrDefault();
                        break;
                    case SourceType.Float:
                        _ = variantVal.GetFloatOrDefault();
                        break;
                    case SourceType.Char:
                        _ = variantVal.GetCharOrDefault();
                        break;
                    case SourceType.Short:
                        _ = variantVal.GetShortOrDefault(); 
                        break;
                    case SourceType.UShort:
                        _ = variantVal.GetUShortOrDefault();
                        break;
                    case SourceType.Byte:
                        _ = variantVal.GetByteOrDefault();
                        break;
                    case SourceType.SByte:
                        _ = variantVal.GetSByteOrDefault();
                        break;
                    case SourceType.TimeSpan:
                        _ = variantVal.GetTimeSpanOrDefault();
                        break;
                    case SourceType.Date:
                        _ = variantVal.GetDateOrDefault();
                        break;
                    case SourceType.DateTime:
                        _ = variantVal.GetDateTimeOrDefault();
                        break;
                    case SourceType.Decimal:
                        _ = variantVal.GetDecimalOrDefault();
                        break;
                    case SourceType.DayOfWeek:
                        _ = variantVal.GetDayOfWeekOrDefault();
                        break;
                    default:
                        ThrowArgumentOutOfRangeException(variantVal.SrcType);
                        break;
                }
            }
        }

        private static void ThrowArgumentOutOfRangeException(SourceType sourceType)
        {
            throw new ArgumentOutOfRangeException(nameof(sourceType), $"{sourceType}");
        }
    }
}

namespace BenchmarkBox.Polymorph
{
    public abstract class AValue
    {
        public string SourceValue { get; }

        public AValue(string sourceValue)
        {
            SourceValue = sourceValue;
        }
    }
    
    public abstract class AValueOf<T> : AValue
    {
        public AValueOf(string sourceValue) : base(sourceValue)
        {
        }
        public abstract T GetValueOrDefault();
    }

    public class IntValue : AValueOf<int>
    {
        public IntValue(string sourceValue) : base(sourceValue)
        {
        }
        
        public override int GetValueOrDefault()
        {
            if (SourceValue != null
                && int.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }

    public class DateValue : AValueOf<DateTime>
    {
        public DateValue(string sourceValue) : base(sourceValue)
        {}
        
        public override DateTime GetValueOrDefault()
        {
            if (SourceValue != null
                && DateTime.TryParseExact(SourceValue, "d", null,
                    DateTimeStyles.None, out var value))
                return value;
            return default;
        }
    }
    
    public class DateTimeValue : AValueOf<DateTime>
    {
        public DateTimeValue(string sourceValue) : base(sourceValue)
        {}
        
        public override DateTime GetValueOrDefault()
        {
            if (SourceValue != null
                && DateTime.TryParse(SourceValue, null,
                    DateTimeStyles.None, out var value))
                return value;
            return default;
        }
    }
    
    public class BoolValue : AValueOf<bool>
    {
        public BoolValue(string sourceValue) : base(sourceValue)
        {}
        
        public override bool GetValueOrDefault()
        {
            if (SourceValue != null
                && bool.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class LongValue : AValueOf<long>
    {
        public LongValue(string sourceValue) : base(sourceValue)
        {}
        
        public override long GetValueOrDefault()
        {
            if (SourceValue != null
                && long.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class ULongValue : AValueOf<ulong>
    {
        public ULongValue(string sourceValue) : base(sourceValue)
        {}
        
        public override ulong GetValueOrDefault()
        {
            if (SourceValue != null
                && ulong.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class UIntValue : AValueOf<uint>
    {
        public UIntValue(string sourceValue) : base(sourceValue)
        {}
        
        public override uint GetValueOrDefault()
        {
            if (SourceValue != null
                && uint.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class ShortValue : AValueOf<short>
    {
        public ShortValue(string sourceValue) : base(sourceValue)
        {}
        
        public override short GetValueOrDefault()
        {
            if (SourceValue != null
                && short.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class UShortValue : AValueOf<ushort>
    {
        public UShortValue(string sourceValue) : base(sourceValue)
        {}
        
        public override ushort GetValueOrDefault()
        {
            if (SourceValue != null
                && ushort.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class TimeSpanValue : AValueOf<TimeSpan>
    {
        public TimeSpanValue(string sourceValue) : base(sourceValue)
        {}
        
        public override TimeSpan GetValueOrDefault()
        {
            if (SourceValue != null
                && TimeSpan.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class DoubleValue : AValueOf<double>
    {
        public DoubleValue(string sourceValue) : base(sourceValue)
        {}
        
        public override double GetValueOrDefault()
        {
            if (SourceValue != null
                && double.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class DecimalValue : AValueOf<decimal>
    {
        public DecimalValue(string sourceValue) : base(sourceValue)
        {}
        
        public override decimal GetValueOrDefault()
        {
            if (SourceValue != null
                && decimal.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class ByteValue : AValueOf<byte>
    {
        public ByteValue(string sourceValue) : base(sourceValue)
        {}
        
        public override byte GetValueOrDefault()
        {
            if (SourceValue != null
                && byte.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class StringValue : AValueOf<string>
    {
        public StringValue(string sourceValue) : base(sourceValue)
        {}
        
        public override string GetValueOrDefault()
        {
            return SourceValue;
        }
    }
    
    public class SByteValue : AValueOf<sbyte>
    {
        public SByteValue(string sourceValue) : base(sourceValue)
        {}
        
        public override sbyte GetValueOrDefault()
        {
            if (SourceValue != null
                && sbyte.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class FloatValue : AValueOf<float>
    {
        public FloatValue(string sourceValue) : base(sourceValue)
        {}
        
        public override float GetValueOrDefault()
        {
            if (SourceValue != null
                && float.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class CharValue : AValueOf<char>
    {
        public CharValue(string sourceValue) : base(sourceValue)
        {}
        
        public override char GetValueOrDefault()
        {
            if (SourceValue != null
                && char.TryParse(SourceValue, out var value))
                return value;
            return default;
        }
    }
    
    public class DayOfWeekValue : AValueOf<DayOfWeek>
    {
        public DayOfWeekValue(string sourceValue) : base(sourceValue)
        {}
        
        public override DayOfWeek GetValueOrDefault()
        {
            if (SourceValue != null
                && Enum.TryParse(SourceValue, out DayOfWeek value))
                return value;
            return default;
        }
    }

    public sealed class VariantValue : AValue
    {
        public VariantValue(string sourceValue, SourceType sourceType)
            : base(sourceValue)
        {
            SrcType = sourceType;
        }
        
        public SourceType SrcType { get; }
    }

    public enum SourceType
    {
        String = 0,
        Bool,
        UInt,
        Long,
        ULong,
        Double,
        Float,
        Char,
        Short,
        UShort,
        Byte,
        SByte,
        TimeSpan,
        Date,
        DateTime,
        Decimal,
        DayOfWeek,
        Int,
    }

    public static class VariantValueConverter
    {
        public static bool GetBoolOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && bool.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static int GetIntOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && int.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static DateTime GetDateOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && DateTime.TryParseExact(variantValue.SourceValue, "d", null,
                    DateTimeStyles.None, out var value))
                return value;
            return default;
        }
        
        public static DateTime GetDateTimeOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && DateTime.TryParse(variantValue.SourceValue, null,
                    DateTimeStyles.None, out var value))
                return value;
            return default;
        }
        
        public static uint GetUIntOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && uint.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static long GetLongOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && long.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static ulong GetULongOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && ulong.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static double GetDoubleOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && double.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static byte GetByteOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && byte.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static sbyte GetSByteOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && sbyte.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static char GetCharOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && char.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static TimeSpan GetTimeSpanOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && TimeSpan.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static float GetFloatOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && float.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static short GetShortOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && short.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static ushort GetUShortOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && ushort.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static decimal GetDecimalOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && decimal.TryParse(variantValue.SourceValue, out var value))
                return value;
            return default;
        }
        
        public static DayOfWeek GetDayOfWeekOrDefault(this VariantValue variantValue)
        {
            if (variantValue.SourceValue != null
                && Enum.TryParse(variantValue.SourceValue, out DayOfWeek value))
                return value;
            return default;
        }
        
    }
}