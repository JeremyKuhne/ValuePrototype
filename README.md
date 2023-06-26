# Value Prototype
Wrapping arbitrary values without boxing of common types.

[![Build](https://github.com/JeremyKuhne/ValuePrototype/actions/workflows/dotnet.yml/badge.svg)](https://github.com/JeremyKuhne/ValuePrototype/actions/workflows/dotnet.yml)

### Goals

- Take any value, including null
- Fit into 64bits of data and an object field (128bits total on x64)
- Do not box any primitive types (`int`, `float`, etc.) [Except for `Decimal`]
- Optimize performance for primitive type storage/retrieval
- Expose `Type` of wrapped type (or `null` for null)
- Allow retrieving as any type that is `AssignableFrom`
- Support nullable intrinsics without boxing (`int?`, `float?`, etc.) [Except for `Decimal`]
- Allow intrinsic to/from nullable intrinsic (except no value to intrinsic)

#### Additional special support

- Support `ArraySegment<T>` without boxing
- Support UTC `DateTime` and `DateTimeOffset` without boxing

[As proposed in .NET](https://github.com/dotnet/runtime/issues/28882)
