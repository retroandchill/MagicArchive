// // @file Blittable.cs
// //
// // @copyright Copyright (c) 2026 Retro & Chill. All rights reserved.
// // Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace MagicArchive.Test.Models;

[Archivable]
public partial struct BlittableStruct
{
    public int MyProperty1;
    public float MyProperty2;
}

[Archivable]
public partial struct BlittableStruct2
{
    public int MyProperty1 { get; set; }
    public float MyProperty2 { get; set; }
}

[Archivable]
public partial struct BlittableStruct3
{
    public int MyProperty1
    {
        get;
        set => field = Math.Clamp(value, 0, 100);
    }
    public float MyProperty2 { get; set; }
}

[Archivable]
public partial struct BlittableStruct4
{
    public int MyProperty1 { get; set; }

    [ArchiveInclude]
    private float MyProperty2 { get; set; }
}

[Archivable]
public readonly partial record struct BlittableStruct5(uint Id);

public enum TokenType : uint
{
    Assembly = 0x00000000,
    AssemblyRef = 0x01000000,
    TypeRef = 0x02000000,
    TypeDef = 0x03000000,
    Variable = 0x04000000,
    Function = 0x05000000,
    Parameter = 0x06000000,

    String = 0x70000000,
}

[Archivable]
public readonly partial record struct MetadataToken
{
    [ArchiveInclude]
    private readonly uint _value;
    private const uint IdMask = 0x00ffffff;
    private const uint TypeMask = 0xff000000;

    [ArchiveIgnore]
    public uint Id
    {
        get => _value & IdMask;
        init
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, IdMask);
            _value = (uint)Type | value;
        }
    }

    [ArchiveIgnore]
    public TokenType Type
    {
        get => (TokenType)(_value & TypeMask);
        init => _value = (uint)value | Id;
    }

    public static readonly MetadataToken Invalid = new(0U);

    public MetadataToken(uint value)
    {
        _value = value;
    }

    public MetadataToken(TokenType type)
    {
        _value = (uint)type;
    }

    public MetadataToken(TokenType type, uint id)
    {
        _value = (uint)type | id;
    }

    public uint ToUInt32() => _value;

    public bool Equals(MetadataToken other)
    {
        return _value == other._value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{Type}:0x{Id:X4}]";
    }
}

[Archivable]
public partial struct NonBlittableStruct
{
    public int MyProperty1 { get; set; }
    public float MyProperty2 { get; set; }

    public double Sum
    {
        get => MyProperty1 + MyProperty2;
        set => throw new NotImplementedException();
    }
}

[Archivable]
public partial struct NonBlittableStruct2
{
    public int MyProperty1 { get; set; }
    public double MyProperty2 { get; set; }
}

[Archivable]
public partial struct NonBlittableStruct3
{
    public int MyProperty1 { get; set; }

    [ArchiveIgnore]
    public float MyProperty2 { get; set; }
}

[Archivable]
public partial struct NonBlittableStruct4
{
    public int MyProperty1 { get; set; }

    private float MyProperty2 { get; set; }
}

[Archivable]
[StructLayout(LayoutKind.Explicit)]
public partial struct NonBlittableStruct5
{
    [field: FieldOffset(0)]
    public int MyProperty1 { get; set; }

    [field: FieldOffset(8)]
    public float MyProperty2 { get; set; }
}

[Archivable(SerializeLayout.Explicit)]
public partial struct NonBlittableStruct6
{
    [ArchiveOrder(1)]
    public int MyProperty1 { get; set; }

    [ArchiveOrder(0)]
    public float MyProperty2 { get; set; }
}
