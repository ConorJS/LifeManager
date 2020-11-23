using System.Diagnostics.CodeAnalysis;

namespace LifeManager.Server.Domain {
    
    // Class properties are serialized; they need to be accessible.
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class DummyData {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}