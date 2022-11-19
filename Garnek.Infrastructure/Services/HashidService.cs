using Garnek.Application.Services;
using HashidsNet;

namespace Garnek.Infrastructure.Services;

public class HashidService : IHashidsService
{
    private readonly IHashids _hashid;

    public HashidService(IHashids hashid)
	{
        _hashid = hashid;
    }

    public Guid DecodeGuid(string hashid)
    {
        var hexString = _hashid.DecodeHex(hashid);
        var bytes = StringHexToByteArray(hexString);
        var guid = new Guid(bytes);
        return guid;
    }

    public string EncodeGuid(Guid id)
    {
        var guidBytes = id.ToByteArray();
        var hexString = Convert.ToHexString(guidBytes);
        var hashId = _hashid.EncodeHex(hexString);
        return hashId;
    }

    private static byte[] StringHexToByteArray(String hex)
    {
        var numberChars = hex.Length;
        var bytes = new byte[numberChars / 2];
        for (var i = 0; i < numberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        return bytes;
    }
}

