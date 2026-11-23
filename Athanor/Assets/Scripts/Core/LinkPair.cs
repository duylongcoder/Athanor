using System;

namespace Athanor.Core
{
    /// <summary>Named pair for bench link coordinates — used as an alternative to value tuples so Unity serialization is reliable.</summary>
    [Serializable]
    public struct LinkPair : IEquatable<LinkPair>
    {
        public int a;
        public int b;

        public LinkPair(int a, int b) { this.a = a; this.b = b; }

        public bool Equals(LinkPair other) => a == other.a && b == other.b;

        public override bool Equals(object obj) => obj is LinkPair other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(a, b);

        public static bool operator ==(LinkPair left, LinkPair right) => left.Equals(right);
        public static bool operator !=(LinkPair left, LinkPair right) => !(left == right);

        /// <summary>Supports tuple deconstruction so `foreach (var (a, b) in links)` works.</summary>
        public void Deconstruct(out int a, out int b) { a = this.a; b = this.b; }
    }
}
