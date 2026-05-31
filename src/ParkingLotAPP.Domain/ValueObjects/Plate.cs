using System.Text.RegularExpressions;

namespace ParkingLotAPP.Domain.ValueObjects
{
    public sealed class Plate : IEquatable<Plate>
    {
        public string Number { get; }

        private static readonly Regex _oldPlateRegex = new("^[A-Z]{3}-?[0-9]{4}", RegexOptions.Compiled);
        private static readonly Regex _newPlateRegex = new("^[A-Z]{3}-?[0-9][0-9/A-Z][0-9]{2}", RegexOptions.Compiled);

        public Plate(string number)
        {
            Number = number.ToUpper();
        }

        public static Plate Create(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException("The number plate should not be null or empty.");
            number = number.Trim().ToUpper();

            if (!_oldPlateRegex.IsMatch(number) && !_newPlateRegex.IsMatch(number))
                throw new ArgumentNullException("Invalid number plate.");

            return new Plate(number);
        }

        public bool Equals(Plate? other)
        {
            if (other is null) return false;
            return Number == other.Number;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Plate);
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }
    }
}
