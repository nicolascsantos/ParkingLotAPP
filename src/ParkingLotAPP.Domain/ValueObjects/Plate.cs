using System.Text.RegularExpressions;

namespace ParkingLotAPP.Domain.ValueObjects
{
    public sealed class Plate : IEquatable<Plate>
    {
        public string Number { get; set; }

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
            return Equals(other as Plate);
        }
    }
}
