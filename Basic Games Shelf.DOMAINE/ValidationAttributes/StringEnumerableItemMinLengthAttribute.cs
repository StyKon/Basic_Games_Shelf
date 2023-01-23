using System.ComponentModel.DataAnnotations;

namespace Basic_Games_Shelf.DOMAINE.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class StringEnumerableItemMinLengthAttribute : ValidationAttribute
    {
        public StringEnumerableItemMinLengthAttribute(int minLength)
        {
            MinLength = minLength;
        }

        public int MinLength { get; }

        public override bool IsValid(object value)
        {
            if (!(value is IEnumerable<string> items)) return false;
            foreach (string item in items)
            {
                if (item.Length < MinLength) return false;
            }
            return true;
        }
    }
}
