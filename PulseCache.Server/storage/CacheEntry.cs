using System;
namespace PulseCache.Server.Storage;

public readonly record struct CacheEntry
(
  byte[] Value,
  DateTime? Expiration
);
