using System;

namespace Snooker.WebScraper.Addresses;

public class Address
{
    public Address()
    {
    }

    public Address(string addressLine)
    {
        ConvertAddress(addressLine);
    }

    public string? City { get; set; }

    public string? Number { get; set; }

    public string? PostalCode { get; set; }

    public string? Street { get; set; }

    private void ConvertAddress(string addressLine)
    {
        string[] parts = addressLine.Split(',');

        if (parts.Length != 2)
            throw new ArgumentException("Invalid address format.");

        // Extract street and number
        string streetAndNumber = parts[0].Trim();
        string[] streetAndNumberParts = streetAndNumber.Split(' ');
        if (streetAndNumberParts.Length < 2)
            throw new ArgumentException("Invalid address format.");

        Number = streetAndNumberParts[^1]; // last element
        Street = string.Join(" ", streetAndNumberParts[0..^1]); // all but the last element

        // Extract postal code and city
        string postalCodeAndCity = parts[1].Trim();
        string[] postalCodeAndCityParts = postalCodeAndCity.Split(' ');
        if (postalCodeAndCityParts.Length < 2)
            throw new ArgumentException("Invalid address format.");

        PostalCode = postalCodeAndCityParts[0];
        City = string.Join(" ", postalCodeAndCityParts[1..]);
    }
}