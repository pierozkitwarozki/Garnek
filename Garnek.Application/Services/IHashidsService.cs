using System;
namespace Garnek.Application.Services;

public interface IHashidsService
{
	public string EncodeGuid(Guid id);
	public Guid DecodeGuid(string hashid);
}

