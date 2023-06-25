using Volo.Abp;

namespace Snooker.Addresses;

public class Address
{
    private string? _city;
    private string? _number;
    private string? _postalCode;
    private string? _street;

    public string? City
    {
        get => _city;
        set
        {
            Check.Length(value, nameof(City), AddressConsts.CityMaxLength);
            _city = value;
        }
    }

    public string? Number
    {
        get => _number;
        set
        {
            Check.Length(value, nameof(Number), AddressConsts.NumberMaxLength);
            _number = value;
        }
    }

    public string? PostalCode
    {
        get => _postalCode;
        set
        {
            Check.Length(value, nameof(PostalCode), AddressConsts.PostalCodeMaxLength);
            _postalCode = value;
        }
    }

    public string? Street
    {
        get => _street;
        set
        {
            Check.Length(value, nameof(Street), AddressConsts.StreetMaxLength);
            _street = value;
        }
    }
}