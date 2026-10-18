# MagicArchive

MagicArchive is a high-performance binary serialization library for .NET 10, based off of the [MemoryPack](https://github.com/Cysharp/MemoryPack) library, but with some modifications in order to better support portability, such as endianness support and avoiding serializing non-blittable types as raw bytes.