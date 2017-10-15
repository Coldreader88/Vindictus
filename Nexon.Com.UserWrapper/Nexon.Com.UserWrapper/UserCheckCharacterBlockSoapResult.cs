using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserCheckCharacterBlockSoapResult : SoapResultBase
	{
		public bool CharacterBlock { get; internal set; }
	}
}
